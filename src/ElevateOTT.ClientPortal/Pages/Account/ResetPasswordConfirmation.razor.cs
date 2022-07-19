namespace ElevateOTT.ClientPortal.Pages.Account;

public partial class ResetPasswordConfirmation : ComponentBase
{
    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }

    #endregion Private Properties

    #region Private Methods

    private void RedirectToLogin()
    {
        NavigationManager.NavigateTo("account/login");
    }

    #endregion Private Methods
}