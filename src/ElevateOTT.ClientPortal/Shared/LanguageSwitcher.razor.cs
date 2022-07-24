namespace ElevateOTT.ClientPortal.Shared;

public partial class LanguageSwitcher
{
    #region Private Fields

    private CultureInfo[] _cultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("es-ES"),
    };

    private string _buttonText = CultureInfo.CurrentUICulture.DisplayName;

    #endregion Private Fields

    #region Private Properties

    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private CultureInfo Culture { get; set; }

    #endregion Private Properties

    #region Private Methods

    private async Task SetCultureInfo(CultureInfo cultureInfo)
    {
        Console.WriteLine(cultureInfo.TextInfo.IsRightToLeft);
        Culture = cultureInfo;
        await LocalStorage.SetItemAsync("Culture", cultureInfo.Name);
        await LocalStorage.SetItemAsync("IsRightToLeft", cultureInfo.TextInfo.IsRightToLeft);
        NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
    }

    #endregion Private Methods
}
