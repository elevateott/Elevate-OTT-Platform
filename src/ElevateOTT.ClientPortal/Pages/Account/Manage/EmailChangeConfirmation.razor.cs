namespace ElevateOTT.ClientPortal.Pages.Account.Manage;

public partial class EmailChangeConfirmation : ComponentBase
{
    #region Public Properties

    [Parameter] public bool DisplayConfirmAccountLink { get; set; }
    [Parameter] public string EmailConfirmationUrl { get; set; }

    #endregion Public Properties
}