﻿namespace ElevateOTT.ClientPortal.Pages.Account;

public partial class LoginRedirect : ComponentBase
{
    #region Public Properties

    [Parameter] public string ReturnUrl { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IReturnUrlProvider ReturnUrlProvider { get; set; }

    #endregion Private Properties

    #region Private Methods

    private void RedirectToLogin()
    {
        ReturnUrlProvider.SetReturnUrl(ReturnUrl);
        NavigationManager.NavigateTo("account/login");
    }

    #endregion Private Methods
}