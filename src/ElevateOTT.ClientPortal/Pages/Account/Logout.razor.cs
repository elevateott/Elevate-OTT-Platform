﻿namespace ElevateOTT.ClientPortal.Pages.Account;

public partial class Logout : ComponentBase
{
    #region Private Properties

    [Inject] private IAuthenticationService AuthenticationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        await AuthenticationService.Logout();
        NavigationManager.NavigateTo("/account/login");
    }

    #endregion Protected Methods
}