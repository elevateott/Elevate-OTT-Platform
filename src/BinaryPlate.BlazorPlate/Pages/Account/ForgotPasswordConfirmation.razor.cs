﻿namespace BinaryPlate.BlazorPlate.Pages.Account;

public partial class ForgotPasswordConfirmation
{
    #region Public Properties

    [Parameter] public string Code { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }

    #endregion Private Properties

    #region Private Methods

    private void ResetPassword()
    {
        NavigationManager.NavigateTo($"account/resetPassword?code={Code}");
    }

    #endregion Private Methods
}