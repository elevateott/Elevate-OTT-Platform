﻿namespace ElevateOTT.Domain.Entities.Settings.IdentitySettings;

public class SignInSettings : ISettingsSchema, IMayHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }

    //public bool RequireConfirmedEmail { get; set; }
    //public bool RequireConfirmedPhoneNumber { get; set; }
    public bool RequireConfirmedAccount { get; set; }

    public Guid? TenantId { get; set; }

    #endregion Public Properties
}