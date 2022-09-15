﻿namespace ElevateOTT.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;

public class AppSignInOptions
{
    #region Public Fields

    public const string Section = "AppSignInOptions";

    #endregion Public Fields

    #region Public Properties

    //public bool RequireConfirmedPhoneNumber { get; set; }
    //public bool RequireConfirmedEmail { get; set; }
    public bool RequireConfirmedAccount { get; set; }

    #endregion Public Properties

    #region Public Methods

    public SignInSettings MapToEntity()
    {
        return new()
        {
            //RequireConfirmedEmail = RequireConfirmedEmail,
            //RequireConfirmedPhoneNumber = RequireConfirmedPhoneNumber,

            // TODO set this to true after email activation workflow set up
            RequireConfirmedAccount = false // RequireConfirmedAccount
        };
    }

    #endregion Public Methods
}
