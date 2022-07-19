﻿namespace ElevateOTT.ClientPortal.Features.AppSettings.Commands.UpdateSettings
{
    public class IdentitySettingsResponse
    {
        #region Public Properties

        public Guid UserSettingsId { get; set; }
        public Guid PasswordSettingsId { get; set; }
        public Guid LockoutSettingsId { get; set; }
        public Guid SignInSettingsId { get; set; }
        public string SuccessMessage { get; set; }

        #endregion Public Properties
    }
}