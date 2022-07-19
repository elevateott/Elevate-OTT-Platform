namespace ElevateOTT.ClientPortal.Pages.Account;

public partial class RegisterConfirmation : ComponentBase
{
    #region Public Properties

    [Parameter] public bool DisplayConfirmAccountLink { get; set; }
    [Parameter] public string EmailConfirmationUrl { get; set; }

    #endregion Public Properties
}