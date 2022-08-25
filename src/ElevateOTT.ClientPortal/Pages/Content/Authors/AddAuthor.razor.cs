using System.ComponentModel;
using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.CreateAuthor;

namespace ElevateOTT.ClientPortal.Pages.Content.Authors;

public partial class AddAuthor : ComponentBase
{
    #region Private Properties
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IBreadcrumbService? BreadcrumbService { get; set; }
    [Inject] private IAuthorsClient? AuthorsClient { get; set; }

    private string? _imageSrc;

    //
    // TODO these values should come from config
    //
    private string _recommendedResolution = "700x700";
    private string _slugExampleName = "carey-bryers";
    private int _maxNameChars = 60;
    private int _maxSeoTitleChars = 60;
    private int _maxSeoDescriptionChars = 170;
    private int _maxSlugChars = 60;


    private string SlugPlaceholder => _slugExampleName;

    // TODO getters and setters ??????
    private StreamContent? _imageContent { get; set; }
    private ServerSideValidator? _serverSideValidator { get; set; }
    private EditContextServerSideValidator? _editContextServerSideValidator { get; set; }
    private CreateAuthorCommand _createAuthorCommand { get; } = new ();

    #endregion Private Properties

    #region Protected Methods
    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService?.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Authors, "/content/authors"),
            new(Resource.Add_Author, "#", true)
        });

        _createAuthorCommand.PropertyChanged += NameChangedHandler;
    }
    #endregion Protected Methods

    #region Private Methods
    private void NameChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        _createAuthorCommand.Slug = _createAuthorCommand.Name.FormatSlug();
        StateHasChanged();
    }
    private void GetBase64StringImageUrl(string imageSrc)
    {
        _imageSrc = imageSrc;
        StateHasChanged();
    }

    private void ImageSelected(StreamContent content)
    {
        _imageContent = content;
        _createAuthorCommand.IsImageAdded = true;
        StateHasChanged();
    }

    private void ImageUnSelected()
    {
        _imageContent = null;
        _createAuthorCommand.IsImageAdded = false;
        StateHasChanged();
    }

    private bool HasUploadedImage()
    {
        return !string.IsNullOrWhiteSpace(_imageSrc);
    }
    private void UpdateRteValue(string value)
    {
        _createAuthorCommand.Bio = value;
    }

    private async Task SubmitForm()
    {
        // TODO guard clauses

        Console.WriteLine("SubmitForm");

        var userFormData = new MultipartFormDataContent
        {
            { new StringContent(_createAuthorCommand.Name ?? string.Empty), "Title" },
            { new StringContent(_createAuthorCommand.Bio ?? string.Empty), "Bio" },
            { new StringContent(_createAuthorCommand.ImageUrl ?? string.Empty), "ImageUrl" },
            { new StringContent(_createAuthorCommand.SeoTitle ?? string.Empty), "SeoTitle" },
            { new StringContent(_createAuthorCommand.SeoDescription ?? string.Empty), "SeoDescription" },
            { new StringContent(_createAuthorCommand.Slug ?? string.Empty), "Slug" },
            { new StringContent(_createAuthorCommand.IsImageAdded.ToString()), "IsImageAdded" },
        };

        if (_imageContent != null)
            userFormData.Add(_imageContent, "ImageFile", _imageContent.Headers.GetValues("FileName").LastOrDefault());

        Console.WriteLine("pre update call");

        var httpResponse = await AuthorsClient.CreateAuthor(userFormData);

        Console.WriteLine("post update call");

        Console.WriteLine("httpResponse: " + httpResponse);
        Console.WriteLine("httpResponse.Success: " + httpResponse.Success);




        if (httpResponse.Success)
        {
            var successResult = httpResponse.Response as SuccessResult<string>;
            Snackbar?.Add(successResult?.Result, Severity.Success);
            NavigationManager?.NavigateTo("content/authors");
        }
        else
        {
            var exceptionResult = httpResponse.Response as ExceptionResult;
            _editContextServerSideValidator?.Validate(exceptionResult);
            _serverSideValidator?.Validate(exceptionResult);
        }
    }
    #endregion Private Methods
}
