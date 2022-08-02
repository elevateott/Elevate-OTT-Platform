namespace ElevateOTT.Application.Common.Extensions.Identity;

public class ApplicationUserManager : UserManager<ApplicationUser>
{
    #region Public Constructors

    public ApplicationUserManager(IUserStore<ApplicationUser> store,
                                  IOptions<IdentityOptions> optionsAccessor,
                                  IPasswordHasher<ApplicationUser> passwordHasher,
                                  IEnumerable<IUserValidator<ApplicationUser>> userValidators,
                                  IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
                                  ILookupNormalizer keyNormalizer,
                                  IdentityErrorDescriber errors,
                                  IServiceProvider services,
                                  ILogger<UserManager<ApplicationUser>> logger) : base(store,
                                                                                       optionsAccessor,
                                                                                       passwordHasher,
                                                                                       userValidators,
                                                                                       passwordValidators,
                                                                                       keyNormalizer,
                                                                                       errors,
                                                                                       services,
                                                                                       logger)
    {
    }

    #endregion Public Constructors

    #region Public Methods
    public void RemoveExcludedUserPermissions(ApplicationUser user, ApplicationUserRole removedUserRole)
    {
        var removedExcludedUserPermissions = from c in user.Claims
                                             join r in removedUserRole.Role.RoleClaims on c.ClaimValue equals r.ClaimValue
                                             where c.IsExcluded
                                             select c;

        user.Claims.RemoveAll(r => removedExcludedUserPermissions.Any(up => r.Id == up.Id));
    }

    public async Task RevokeOrGrantPermissionsAsync(ApplicationUser user, List<Guid> inheritedDbUserPermissions, IList<Guid> selectedPermissionIds, IApplicationDbContext dbContext)
    {
        var revokedUserPermissionsIds = inheritedDbUserPermissions.Except(selectedPermissionIds).ToList();

        var revokedUserPermissions = await dbContext.ApplicationPermissions.Where(ap => revokedUserPermissionsIds.Any(rup => ap.Id == rup)).ToListAsync();

        foreach (var revokedUserPermission in revokedUserPermissions)
        {
            user.Claims.Add(new ApplicationUserClaim()
            {
                ClaimType = "permissions",
                ClaimValue = revokedUserPermission.Name,
                IsExcluded = true,
            });
        }
    }

    public async Task AddOrRemoveDirectUserPermissionAsync(IList<Guid> assignedUserPermissionIds, ApplicationUser user, IApplicationDbContext dbContext)
    {
        if (assignedUserPermissionIds?.Any() == true)
        {
            var addedUserPermissions = assignedUserPermissionIds.Where(aup => user.Claims.All(uc => uc.ClaimValue != dbContext.ApplicationPermissions.FirstOrDefault(c => c.Id == aup)?.Name)).ToList();

            var removedUserPermissions = user.Claims.Where(uc => assignedUserPermissionIds.All(aup => aup != dbContext.ApplicationPermissions.FirstOrDefault(c => c.Name == uc.ClaimValue)?.Id)).ToList();

            foreach (var addedUserPermission in addedUserPermissions)
            {
                var permission = await dbContext.ApplicationPermissions.FirstOrDefaultAsync(c => c.Id == addedUserPermission);

                user.Claims.Add(new ApplicationUserClaim()
                {
                    UserId = user.Id,
                    ClaimType = "permissions",
                    ClaimValue = permission.Name,
                    IsExcluded = false,
                });
            }

            foreach (var removedUserPermission in removedUserPermissions)
                user.Claims.Remove(removedUserPermission);
        }
        else
        {
            user.Claims.Clear();
        }
    }

    public async Task<Envelope<string>> ResetPasswordAsync(string password, ApplicationUser user, string token)
    {
        var resetPasswordResult = await ResetPasswordAsync(user, token, password);

        return !resetPasswordResult.Succeeded
            ? Envelope<string>.Result.AddErrors(resetPasswordResult.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok();
    }

    public async Task<IdentityResult> DeleteWithFilesAsync(ApplicationUser user, string folderName, IStorageProvider storageProvider)
    {
        var result = await base.DeleteAsync(user);

        if (result.Succeeded)
            await DeleteFilesAsync(folderName, user.Email.ReplaceSpaceAndSpecialCharsWithDashes(), storageProvider);

        return result;
    }

    public async Task<IdentityResult> CreateWithFilesAsync(ApplicationUser user, string password, IFormFile avatar, IList<IFormFile> attachments, string folderName, IStorageProvider storageProvider)
    {
        var result = await base.CreateAsync(user, password);

        if (result.Succeeded)
        {
            try
            {
                await AddUserAvatarAsync(user, avatar, folderName, storageProvider);
                await AddUserAttachmentsAsync(user, attachments, "users", storageProvider);
                await base.UpdateAsync(user);// with attachments
            }
            catch (Exception ex)
            {
                await DeleteFilesAsync(folderName, user.Email.ReplaceSpaceAndSpecialCharsWithDashes(), storageProvider);
                throw new Exception($"{ex} - {Resource.User_has_been_added_without_files}");
            }
        }

        return result;
    }

    public async Task<IdentityResult> UpdateWithFilesAsync(ApplicationUser user, IFormFile avatar, string oldAvatarUri, IList<IFormFile> attachments, IList<Guid> attachmentIds, string fileNamePrefix, IStorageProvider storageProvider)
    {
        var result = await base.UpdateAsync(user);

        if (!result.Succeeded)
            return result;

        var storageService = await storageProvider.InvokeInstanceAsync();

        switch (storageService.GetFileState(avatar, oldAvatarUri))
        {
            case FileStatus.Unchanged:
                break;

            case FileStatus.Modified:
                await UpdateUserAvatarAsync(user, avatar, fileNamePrefix, oldAvatarUri, storageProvider);
                break;

            case FileStatus.Deleted:
                await storageService.DeleteFileIfExists(user.AvatarUri, "Users");
                user.AvatarUri = null;
                break;
        }

        await AddOrRemoveUserAttachmentsAsync(user, attachments, attachmentIds, fileNamePrefix, storageProvider);

        result = await base.UpdateAsync(user);

        return result;
    }

    public async Task<IdentityResult> UpdateWithFilesAsync(ApplicationUser user, IFormFile avatar, string oldAvatarUri, string fileNamePrefix, IStorageProvider storageProvider)
    {
        var result = await base.UpdateAsync(user);

        if (!result.Succeeded)
            return result;

        var storageService = await storageProvider.InvokeInstanceAsync();

        switch (storageService.GetFileState(avatar, oldAvatarUri))
        {
            case FileStatus.Unchanged:
                break;

            case FileStatus.Modified:
                await UpdateUserAvatarAsync(user, avatar, fileNamePrefix, oldAvatarUri, storageProvider);
                break;

            case FileStatus.Deleted:
                await storageService.DeleteFileIfExists(user.AvatarUri, "Users");
                user.AvatarUri = null;
                break;
        }

        result = await base.UpdateAsync(user);

        return result;
    }

    public async Task DeleteFilesAsync(string folderName, string subFolderName, IStorageProvider storageProvider)
    {
        var storageService = await storageProvider.InvokeInstanceAsync();

        await storageService.DeleteContainer(folderName, subFolderName);
    }

    public async Task UpdateUserAvatarAsync(ApplicationUser user, IFormFile avatar, string fileNamePrefix, string oldFileUri, IStorageProvider storageProvider)
    {
        var storageService = await storageProvider.InvokeInstanceAsync();

        var newUserAvatarUri = await storageService.EditFile(avatar, "users", fileNamePrefix, oldFileUri);

        user.AvatarUri = newUserAvatarUri;
    }

    public string GenerateRandomPassword(int passwordLength)
    {
        var rnd = new Random();
        return rnd.Next(passwordLength).ToString();
    }

    public void AddOrRemoveUserRoles(IList<string> assignedUserRoleIds, ApplicationUser dbUser)
    {
        if (assignedUserRoleIds != null)
        {
            var addedUserRoles = assignedUserRoleIds.Where(aur => dbUser.UserRoles.All(r => r.RoleId != aur)).ToList();

            var removedUserRoles = dbUser.UserRoles.Where(ur => assignedUserRoleIds.All(ar => ar != ur.RoleId)).ToList();

            foreach (var addedRole in addedUserRoles)
            {
                dbUser.UserRoles.Add(new ApplicationUserRole
                {
                    RoleId = addedRole,
                    UserId = dbUser.Id,
                });
            }

            foreach (var removedUserRole in removedUserRoles)
            {
                dbUser.UserRoles.Remove(removedUserRole);
                RemoveExcludedUserPermissions(dbUser, removedUserRole);
            }
        }
        else
        {
            foreach (var userRole in dbUser.UserRoles)
                RemoveExcludedUserPermissions(dbUser, userRole);
            dbUser.UserRoles.Clear();
        }
    }

    public void AssignRolesToUser(IList<string> assignedRoleIds, ApplicationUser user, IList<string> defaultRoleIds)
    {
        if (assignedRoleIds?.Any() == true)
            foreach (var roleId in assignedRoleIds)
                user.UserRoles.Add(new ApplicationUserRole { RoleId = roleId });

        foreach (var defaultRoleId in defaultRoleIds)
            if (user.UserRoles.All(ur => ur.RoleId != defaultRoleId))
                user.UserRoles.Add(new ApplicationUserRole { RoleId = defaultRoleId });
    }

    public async Task<string> SendActivationEmailAsync(ApplicationUser user, IConfigReaderService configReaderService, INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
    {
        var callbackUrl = string.Empty;

        var code = await GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var clientAppOptions = configReaderService.GetClientAppOptions();

        var tenantHost = httpContextAccessor.GetClientAppHost();

        var url = $"{tenantHost}/{clientAppOptions.ConfirmEmailUrl}";

        callbackUrl = string.Format(url, user.Id, code);

        await notificationService.SendEmailAsync(user.Email, Resource.Confirm_your_email, string.Format(Resource.Please_confirm_your_account_by_clicking_here, HtmlEncoder.Default.Encode(callbackUrl)));

        return callbackUrl;
    }

    public async Task<string> SendActivationEmailAsync(ApplicationUser user, string newEmail, IConfigReaderService configReaderService, INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
    {
        var callbackUrl = string.Empty;

        if (Options.SignIn.RequireConfirmedAccount)
        {
            var code = await GenerateChangeEmailTokenAsync(user, newEmail);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var clientAppOptions = configReaderService.GetClientAppOptions();

            var tenantHost = httpContextAccessor.GetClientAppHost();

            var url = $"{tenantHost}/{clientAppOptions.ConfirmEmailChangeUrl}";

            callbackUrl = string.Format(url, user.Id, newEmail, code);

            await notificationService.SendEmailAsync(newEmail, Resource.Confirm_your_email, string.Format(Resource.Please_confirm_your_account_by_clicking_here, HtmlEncoder.Default.Encode(callbackUrl)));
        }
        return callbackUrl;
    }

    public async Task<string> SendResetPasswordAsync(ApplicationUser user, IConfigReaderService configReaderService, INotificationService notificationService, TenantMode tenantMode)
    {
        var code = await GeneratePasswordResetTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var clientAppOptions = configReaderService.GetClientAppOptions();

        var url = tenantMode switch
        {
            TenantMode.MultiTenant => $"{string.Format(clientAppOptions.MultiTenantHostName, configReaderService.GetSubDomain())}/{clientAppOptions.ResetPasswordUrl}",
            TenantMode.SingleTenant => $"{clientAppOptions.SingleTenantHostName}/{clientAppOptions.ResetPasswordUrl}",
            _ => throw new ArgumentOutOfRangeException(nameof(tenantMode), tenantMode, null),
        };

        var callbackUrl = string.Format(url, code);

        await notificationService.SendEmailAsync(user.Email, Resource.Reset_your_password, string.Format(Resource.Please_reset_your_password_by_clicking_here, HtmlEncoder.Default.Encode(callbackUrl)));

        return code;
    }

    #endregion Public Methods

    #region Private Methods

    private async Task AddOrRemoveUserAttachmentsAsync(ApplicationUser user, IList<IFormFile> attachments, IList<Guid> attachmentIds, string fileNamePrefix, IStorageProvider storageProvider)
    {
        var storageService = await storageProvider.InvokeInstanceAsync();

        if (attachmentIds?.Any() == true)
        {
            var userAttachmentsToBeRemoved = user.UserAttachments.Where(ua => attachmentIds.All(ai => ai != ua.Id)).ToList();

            foreach (var userAttachment in userAttachmentsToBeRemoved)
            {
                user.UserAttachments.Remove(userAttachment);
                await storageService.DeleteFileIfExists(userAttachment.FileUri, "Users");
            }
        }

        if (attachments?.Any() == true)
        {
            var newUserAttachments = await storageService.UploadMultipleFiles(attachments, "users", fileNamePrefix);

            var userAttachmentsToBeAdded = newUserAttachments.Select(u => new ApplicationUserAttachment
            {
                FileName = u.FileName,
                FileUri = u.FileUri
            }).ToList();

            //check if file with the same name exists.
            var userAttachmentsUriWithoutDuplicates = userAttachmentsToBeAdded.Where(item =>
                !user.UserAttachments.Exists(ua => ua.FileUri == item.FileUri));

            user.UserAttachments.AddRange(userAttachmentsUriWithoutDuplicates);
        }
    }

    private async Task AddUserAttachmentsAsync(ApplicationUser user, IList<IFormFile> attachmentsFormFiles, string folderName, IStorageProvider storageProvider)
    {
        var storageService = await storageProvider.InvokeInstanceAsync();

        var userAttachmentsToBeAdded = await storageService.UploadMultipleFiles(attachmentsFormFiles, folderName, user.Email.ReplaceSpaceAndSpecialCharsWithDashes());

        user.UserAttachments = userAttachmentsToBeAdded.Select(uam => new ApplicationUserAttachment { FileName = uam.FileName, FileUri = uam.FileUri }).ToList();
    }

    private async Task AddUserAvatarAsync(ApplicationUser user, IFormFile avatarFormFile, string folderName, IStorageProvider storageProvider)
    {
        var storageService = await storageProvider.InvokeInstanceAsync();

        var userAvatarUri = await storageService.UploadFile(avatarFormFile, folderName, user.Email.ReplaceSpaceAndSpecialCharsWithDashes());

        user.AvatarUri = userAvatarUri;
    }

    #endregion Private Methods
}
