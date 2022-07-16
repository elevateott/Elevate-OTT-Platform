﻿namespace BinaryPlate.Application.UseCases.Identity;

public class AccountUseCase : IAccountUseCase
{
    #region Private Fields

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationUserManager _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<RegisterCommand> _logger;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly INotificationService _notificationService;
    private readonly IConfigReaderService _configReaderService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAppSettingsUseCase _appSettingsUseCase;
    private readonly IApplicationDbContext _dbContext;
    private readonly ITenantResolver _tenantResolver;
    private readonly IDemoUserPasswordSetterService _demoUserPasswordSetterService;

    #endregion Private Fields

    #region Public Constructors

    public AccountUseCase(ApplicationUserManager userManager,
                          SignInManager<ApplicationUser> signInManager,
                          RoleManager<ApplicationRole> roleManager,
                          ILogger<RegisterCommand> logger,
                          ITokenGeneratorService tokenGeneratorService,
                          INotificationService notificationService,
                          IConfigReaderService configReaderService,
                          IHttpContextAccessor httpContextAccessor,
                          IAppSettingsUseCase appSettingsUseCase,
                          IApplicationDbContext dbContext,
                          ITenantResolver tenantResolver,
                          IDemoUserPasswordSetterService demoUserPasswordSetterService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;
        _tokenGeneratorService = tokenGeneratorService;
        _notificationService = notificationService;
        _configReaderService = configReaderService;
        _httpContextAccessor = httpContextAccessor;
        _appSettingsUseCase = appSettingsUseCase;
        _dbContext = dbContext;
        _tenantResolver = tenantResolver;
        _demoUserPasswordSetterService = demoUserPasswordSetterService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<LoginResponse>> Login(LoginCommand request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user.IsSuspended)
                return Envelope<LoginResponse>.Result.ServerError($"{Resource.You_have_successfully_created_a_new_account} {Resource.Your_account_is_deactivated_Please_contact_your_administrator}", rollbackDisabled: true);

            Envelope<string> accessToken = await GenerateTokens(user);

            var authResponse = new AuthResponse { AccessToken = accessToken.Payload, RefreshToken = user.RefreshToken };

            var loginResponse = new LoginResponse
            {
                AuthResponse = authResponse,
                RequiresTwoFactor = false,
            };

            return Envelope<LoginResponse>.Result.Ok(loginResponse);
        }

        if (result.RequiresTwoFactor)
            return Envelope<LoginResponse>.Result.Ok(new LoginResponse { RequiresTwoFactor = true });

        if (result.IsLockedOut)
            return Envelope<LoginResponse>.Result.AddErrors(result.ToApplicationResult(), ResponseType.Unauthorized, rollbackDisabled: true);
        else
        {
            return Envelope<LoginResponse>.Result.AddErrors(result.ToApplicationResult(), ResponseType.Unauthorized, rollbackDisabled: true);
        }
    }

    public async Task<Envelope<LoginWith2FaResponse>> LoginWith2Fa(LoginWith2FaCommand request)
    {
        //var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        var user = await _userManager.FindByEmailAsync(request.UserName);

        if (user == null)
            return Envelope<LoginWith2FaResponse>.Result.NotFound(Resource.Unable_to_load_user);

        var authenticatorCode = request.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

        var isValidTwoFactorToken = await _userManager.VerifyTwoFactorTokenAsync(user, new IdentityOptions().Tokens.AuthenticatorTokenProvider, authenticatorCode);

        //var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode,
        //    loginWith2FaCommand.RememberMe, loginWith2FaCommand.RememberMachine);

        if (isValidTwoFactorToken)
        {
            _logger.LogInformation(Resource.User_with_Id_UserId_logged_in_with_2Fa, user.Id);

            var accessToken = await GenerateTokens(user);

            var authResponse = new AuthResponse
            {
                AccessToken = accessToken.Payload,
                RefreshToken = user.RefreshToken,
            };
            var loginWith2FaResponse = new LoginWith2FaResponse { AuthResponse = authResponse };

            return Envelope<LoginWith2FaResponse>.Result.Ok(loginWith2FaResponse);
        }
        _logger.LogWarning($"Invalid authenticator code entered for user with ID '{user.UserName}'");

        return Envelope<LoginWith2FaResponse>.Result.BadRequest(Resource.Invalid_authenticator_code_entered);
    }

    public async Task<Envelope<LoginWithRecoveryCodeResponse>> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request)
    {
        var user = await _userManager.FindByEmailAsync(request.UserName);

        if (user == null)
            return Envelope<LoginWithRecoveryCodeResponse>.Result.NotFound(Resource.Unable_to_load_user);

        var recoveryCode = request.RecoveryCode.Replace(" ", string.Empty);

        var result = await _userManager.RedeemTwoFactorRecoveryCodeAsync(user, recoveryCode);

        if (result.Succeeded)
        {
            _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);

            var accessToken = await GenerateTokens(user);
            var authResponse = new AuthResponse
            {
                AccessToken = accessToken.Payload,
                RefreshToken = user.RefreshToken,
            };
            var loginWithRecoveryCodeResponse = new LoginWithRecoveryCodeResponse { AuthResponse = authResponse };
            return Envelope<LoginWithRecoveryCodeResponse>.Result.Ok(loginWithRecoveryCodeResponse);
        }
        _logger.LogWarning($"Invalid recovery code entered for user with ID '{user.Id}'");
        return Envelope<LoginWithRecoveryCodeResponse>.Result.BadRequest(Resource.Invalid_recovery_code_entered);
    }

    public async Task<Envelope<RegisterResponse>> Register(RegisterCommand request)
    {
        var user = request.MapToEntity();

        AssignUserToDefaultRoles(user);

        await SetInitialActivation(user);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Envelope<RegisterResponse>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError, rollbackDisabled: true);

        if (_tenantResolver.TenantMode == TenantMode.SingleTenant)
        {
            var registerAsSuperAdminIfNotExistResult = await RegisterAsSuperAdminIfNotExist(user);

            if (registerAsSuperAdminIfNotExistResult.IsError)
                return Envelope<RegisterResponse>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError, rollbackDisabled: true);
        }

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
        {
            var callbackUrl = await _userManager.SendActivationEmailAsync(user, _configReaderService, _notificationService, _httpContextAccessor);

            var payload = new RegisterResponse
            {
                RequireConfirmedAccount = true,
                DisplayConfirmAccountLink = true,
                Email = user.Email,
                EmailConfirmationUrl = HttpUtility.UrlEncode(callbackUrl),
                AuthResponse = null,
                SuccessMessage = Resource.Verification_email_has_been_sent
            };
            return Envelope<RegisterResponse>.Result.Ok(payload);
        }
        else
        {
            var loginCommand = new LoginCommand
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = false
            };

            var loginResponse = await Login(loginCommand);

            if (loginResponse.IsError)
            {
                return loginResponse.ModelStateErrors.Any()
                    ? Envelope<RegisterResponse>.Result.AddErrors(loginResponse.ModelStateErrors, ResponseType.ServerError, rollbackDisabled: true)
                    : Envelope<RegisterResponse>.Result.ServerError(loginResponse.Message, rollbackDisabled: true);
            }
            var payload = new RegisterResponse
            {
                RequireConfirmedAccount = false,
                DisplayConfirmAccountLink = false,
                Email = user.Email,
                EmailConfirmationUrl = null,
                AuthResponse = new AuthResponse
                {
                    AccessToken = loginResponse.Payload.AuthResponse.AccessToken,
                    RefreshToken = loginResponse.Payload.AuthResponse.RefreshToken
                },
                SuccessMessage = Resource.You_have_successfully_created_a_new_account
            };

            _logger.LogInformation("User created a new account with password.");

            return Envelope<RegisterResponse>.Result.Ok(payload);
        }
    }

    public async Task<Envelope<string>> ConfirmEmail(string userId, string code)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            // Don't reveal that the user does not exist
            return Envelope<string>.Result.Ok(Resource.Username_has_been_confirmed_successfully);

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

        var result = await _userManager.ConfirmEmailAsync(user, code);

        return !result.Succeeded
            ? Envelope<string>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError, rollbackDisabled: true)
            : Envelope<string>.Result.Ok(Resource.Username_has_been_confirmed_successfully);
    }

    public async Task<Envelope<ResendEmailConfirmationResponse>> ResendEmailConfirmation(ResendEmailConfirmationCommand request)
    {
        ResendEmailConfirmationResponse payload;

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            // Don't reveal that the user does not exist
            payload = new ResendEmailConfirmationResponse
            {
                RequireConfirmedAccount = true,
                DisplayConfirmAccountLink = true,
                SuccessMessage = Resource.Verification_email_has_been_sent
            };
            return Envelope<ResendEmailConfirmationResponse>.Result.Ok(payload);
        }

        var callbackUrl = await _userManager.SendActivationEmailAsync(user, _configReaderService, _notificationService, _httpContextAccessor);

        payload = new ResendEmailConfirmationResponse
        {
            RequireConfirmedAccount = true,
            DisplayConfirmAccountLink = true,
            EmailConfirmationUrl = HttpUtility.UrlEncode(callbackUrl),
            SuccessMessage = Resource.Verification_email_has_been_sent
        };

        return Envelope<ResendEmailConfirmationResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<ForgetPasswordResponse>> ForgotPassword(ForgetPasswordCommand request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            // Don't reveal that the user does not exist or is not confirmed
            return Envelope<ForgetPasswordResponse>.Result.Ok(new ForgetPasswordResponse
            {
                Code = null,
                SuccessMessage = Resource.Password_reset_link_was_sent
            });
        // For more information on how to enable account confirmation and password reset please
        // visit https://go.microsoft.com/fwlink/?LinkID=532713

        var code = await _userManager.SendResetPasswordAsync(user, _configReaderService, _notificationService, _tenantResolver.TenantMode);

        var payload = new ForgetPasswordResponse
        {
            Code = code,
            SuccessMessage = Resource.Password_reset_link_was_sent,
        };

        return Envelope<ForgetPasswordResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<string>> ResetPassword(ResetPasswordCommand request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null) // Don't reveal that the user does not exist
            return Envelope<string>.Result.Ok(Resource.Your_password_has_been_reset);

        var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code)), request.Password);

        await _demoUserPasswordSetterService.ResetDefaultPassword(request.Password, user);

        return !result.Succeeded
            ? Envelope<string>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError, rollbackDisabled: true)
            : Envelope<string>.Result.Ok(Resource.Your_password_has_been_reset);
    }

    public async Task<Envelope<AuthResponse>> RefreshToken(RefreshTokenCommand request)//Extract to another class
    {
        if (request is null)
            return Envelope<AuthResponse>.Result.BadRequest(Resource.Invalid_client_request);

        var principal = _tokenGeneratorService.GetPrincipalFromExpiredToken(request.AccessToken);

        if (principal.Identity is null)
            return Envelope<AuthResponse>.Result.BadRequest(Resource.Invalid_client_request);

        var username = principal.Identity.Name;

        var user = await _userManager.FindByEmailAsync(username);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenTimeSpan <= DateTime.UtcNow)//::REFRESH::
            return Envelope<AuthResponse>.Result.BadRequest(Resource.Invalid_client_request);

        var tokenSettingsResponse = await _appSettingsUseCase.GetTokenSettings();

        var refreshTokenTimeSpan = tokenSettingsResponse.Payload.TokenSettings.RefreshTokenTimeSpan;

        if (refreshTokenTimeSpan == null)
            return Envelope<AuthResponse>.Result.BadRequest(Resource.Refresh_token_timespan_cannot_be_null);

        user.RefreshTokenTimeSpan = DateTime.UtcNow.AddMinutes(refreshTokenTimeSpan.Value);

        var accessToken = await _tokenGeneratorService.GenerateAccessTokenAsync(user);

        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        await _userManager.UpdateAsync(user);

        var payload = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = user.RefreshToken,
        };

        return Envelope<AuthResponse>.Result.Ok(payload);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<Envelope<ApplicationUser>> RegisterAsSuperAdminIfNotExist(ApplicationUser user)
    {
        if (_userManager.Users.Any(u => u.Id != user.Id)) return Envelope<ApplicationUser>.Result.Ok();

        var adminRole = new ApplicationRole { Name = "Admin" };

        if (!_roleManager.Roles.Any())
        {
            foreach (var permission in _dbContext.ApplicationPermissions)
                adminRole.RoleClaims.Add(new ApplicationRoleClaim
                {
                    ClaimType = "permissions",
                    ClaimValue = permission.Name
                });

            var result = await _roleManager.CreateAsync(adminRole);

            if (!result.Succeeded)
                return Envelope<ApplicationUser>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.BadRequest, rollbackDisabled: true);
        }

        user.IsSuspended = false;

        user.JobTitle = "Administrator";

        await _userManager.AddToRoleAsync(user, adminRole.Name);

        return Envelope<ApplicationUser>.Result.Ok(user);
    }

    private async Task SetInitialActivation(ApplicationUser user)
    {
        var identitySettings = await _appSettingsUseCase.GetIdentitySettings();

        user.IsSuspended = !identitySettings.Payload.UserSettings.NewUsersActiveByDefault;
    }

    private void AssignUserToDefaultRoles(ApplicationUser user)
    {
        var defaultRoleIds = _roleManager.Roles.Where(r => r.IsDefault).Select(r => r.Id);

        foreach (var defaultRoleId in defaultRoleIds)
            user.UserRoles.Add(new ApplicationUserRole { RoleId = defaultRoleId });
    }

    private async Task<Envelope<string>> GenerateTokens(ApplicationUser user)
    {
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        var tokenSettingsResponse = await _appSettingsUseCase.GetTokenSettings();

        var refreshTokenTimeSpan = tokenSettingsResponse.Payload.TokenSettings.RefreshTokenTimeSpan;

        if (refreshTokenTimeSpan == null)
            return Envelope<string>.Result.BadRequest(Resource.Refresh_token_timespan_cannot_be_null);

        user.RefreshTokenTimeSpan = DateTime.UtcNow.AddMinutes(refreshTokenTimeSpan.Value);

        await _userManager.UpdateAsync(user);

        var accessToken = await _tokenGeneratorService.GenerateAccessTokenAsync(user);

        return Envelope<string>.Result.Ok(accessToken);
    }

    #endregion Private Methods
}