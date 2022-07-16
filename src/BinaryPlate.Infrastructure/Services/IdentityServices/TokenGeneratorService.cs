namespace BinaryPlate.Infrastructure.Services.IdentityServices;

public class TokenGeneratorService : ITokenGeneratorService
{
    #region Private Fields

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfigReaderService _configReaderService;
    private readonly IAppSettingsUseCase _appSettingsUseCase;
    private readonly IUserUseCase _userUseCase;

    #endregion Private Fields

    #region Public Constructors

    public TokenGeneratorService(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IConfigReaderService configReaderService,
                                 IAppSettingsUseCase appSettingsUseCase,
                                 IUserUseCase userUseCase)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configReaderService = configReaderService;
        _appSettingsUseCase = appSettingsUseCase;
        _userUseCase = userUseCase;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new("AvatarUri", user.AvatarUri ?? string.Empty),
            new("FullName", string.IsNullOrWhiteSpace(user.FullName) ? user.UserName : user.FullName),
            new("JobTitle", user.JobTitle ?? string.Empty),
            new("refreshAt", ((DateTimeOffset)user.RefreshTokenTimeSpan).ToUnixTimeSeconds().ToString()),
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        var roleClaims = userRoles.Select(r => new Claim(ClaimTypes.Role, r));

        claims.AddRange(roleClaims);

        var selectedNonExcludedPermissions = await _userUseCase.GetUserPermissionsWithoutExcluded(user);

        var allUserPermissions = selectedNonExcludedPermissions.Select(nep => new Claim("permissions", nep.Name));

        claims.AddRange(allUserPermissions);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configReaderService.GetJwtOptions().SecurityKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var tokenSettingsResponse = await _appSettingsUseCase.GetTokenSettings();

        var accessTokenTimeSpan = tokenSettingsResponse.Payload.TokenSettings.AccessTokenTimeSpan;

        if (accessTokenTimeSpan == null)
            throw new Exception(Resource.Access_token_timespan_cannot_be_null);

        var expiry = DateTime.UtcNow.AddMinutes(accessTokenTimeSpan.Value);

        var token = new JwtSecurityToken(issuer: _configReaderService.GetJwtOptions().Issuer,
                                         audience: _configReaderService.GetJwtOptions().Issuer,
                                         claims: claims,
                                         expires: expiry,
                                         signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new SecurityTokenException(Resource.Invalid_token);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configReaderService.GetJwtOptions().SecurityKey)),
            ValidateLifetime = false,
            ValidIssuer = _configReaderService.GetJwtOptions().Issuer,
            ValidAudience = _configReaderService.GetJwtOptions().Issuer,
            //ValidAudience = _appOptionsService.GetJwtOptions().Audience,
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken?.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase) != true)
            throw new SecurityTokenException(Resource.Invalid_token);

        return principal;
    }

    #endregion Public Methods
}