using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorsForAutoComplete;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.ClientPortal.Models.DTOs;

namespace ElevateOTT.ClientPortal.Pages.Content.Videos;

public partial class EditVideo : ComponentBase
{
    #region Public Properties

    [Parameter] public Guid VideoId { get; set; }

    #endregion Public Properties

    #region Private Properties
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IBreadcrumbService? BreadcrumbService { get; set; }
    [Inject] private IVideosClient? VideosClient { get; set; }    
    [Inject] private IAuthorsClient? AuthorsClient { get; set; }
    private string? _imageSrc;

    //
    // TODO these values should come from config
    //
    private EditContext _editContext;
    private string _recommendedResolution = "700x700";
    private string _slugExampleName = "carey-bryers";
    private int _maxNameChars = 60;
    private int _maxSeoTitleChars = 60;
    private int _maxSeoDescriptionChars = 170;
    private int _maxShortDescriptionChars = 140;
    private int _maxSlugChars = 60;
    private AuthorItemForAutoComplete _selectedAuthor = new();
    private  AuthorsForAutoCompleteResponse _authorsForAutoResponse = new ();
    private string SlugPlaceholder => _slugExampleName;

    // TODO getters and setters ??????
    private StreamContent? _imageContent { get; set; }
    private ServerSideValidator? _serverSideValidator { get; set; }
    private EditContextServerSideValidator? _editContextServerSideValidator { get; set; }
    private VideoForEdit _videoForEditVm { get; set; } = new();
    private UpdateVideoCommand? _updateVideoCommand { get; set; }
    

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService?.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Videos, "/content/videos"),
            new(Resource.Edit_Video, "#", true)
        });

        var httpResponseWrapper = await VideosClient.GetVideo(VideoId);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<VideoForEdit>;
            _videoForEditVm = successResult?.Result;
            if (_videoForEditVm?.Author is not null)
            {
                _selectedAuthor = new AuthorItemForAutoComplete
                {
                    Id = _videoForEditVm.Author.Id,
                    Name = _videoForEditVm.Author.Name ?? string.Empty,
                    ImageUrl = _videoForEditVm.Author.ImageUrl ?? string.Empty
                };
            }
            //_videoForEditVm.PropertyChanged += NameChangedHandler;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            _serverSideValidator.Validate(exceptionResult);
        }
    }
    
    #endregion Protected Methods

    #region Private Methods
    //private void NameChangedHandler(object? sender, PropertyChangedEventArgs e)
    //{
    //    _videoForEditVm.Slug = _videoForEditVm.Name.FormatSlug();
    //    StateHasChanged();
    //}

    private void GetBase64StringImageUrl(string imageSrc)
    {
        _imageSrc = imageSrc;
        StateHasChanged();
    }

    private void ImageSelected(StreamContent content)
    {
        _imageContent = content;
        //_videoForEditVm.IsImageAdded = true;
        StateHasChanged();
    }

    private void ImageUnSelected()
    {
        _imageContent = null;
        //_videoForEditVm.IsImageAdded = false;
        StateHasChanged();
    }

    private bool HasUploadedImage()
    {
        return !string.IsNullOrWhiteSpace(_imageSrc);
    }
    private void UpdateRteValue(string value)
    {
        //_videoForEditVm.Bio = value;
    }

    private async Task SubmitForm()
    {
        // TODO guard clauses

        // _editContext?.Validate();

        Console.WriteLine("SubmitForm");

        System.Console.WriteLine($"selected author id: {_selectedAuthor.Id} ");
        System.Console.WriteLine($"selected author name: {_selectedAuthor.Name} ");
        // TODO update AuthorId

        //_updateVideoCommand = new UpdateAuthorCommand
        //{
        //    Id = _videoForEditVm.Id,
        //    Name = _videoForEditVm.Name,
        //    Bio = _videoForEditVm.Bio,
        //    ImageUrl = _videoForEditVm.ImageUrl,
        //    SeoTitle = _videoForEditVm.SeoTitle,
        //    SeoDescription = _videoForEditVm.SeoDescription,
        //    Slug = _videoForEditVm.Slug.FormatSlug(),
        //    IsImageAdded = _videoForEditVm.IsImageAdded
        //};


        //var userFormData = new MultipartFormDataContent
        //    {
        //        { new StringContent(_updateVideoCommand.Id.ToString() ?? string.Empty), "id" },
        //        { new StringContent(_updateVideoCommand.Name ?? string.Empty), "Name" },
        //        { new StringContent(_updateVideoCommand.Bio ?? string.Empty), "Bio" },
        //        { new StringContent(_updateVideoCommand.ImageUrl ?? string.Empty), "ImageUrl" },
        //        { new StringContent(_updateVideoCommand.SeoTitle ?? string.Empty), "SeoTitle" },
        //        { new StringContent(_updateVideoCommand.SeoDescription ?? string.Empty), "SeoDescription" },
        //        { new StringContent(_updateVideoCommand.Slug ?? string.Empty), "Slug" },
        //        { new StringContent(_updateVideoCommand.IsImageAdded.ToString()), "IsImageAdded" },
        //    };

        //if (_imageContent != null)
        //    userFormData.Add(_imageContent, "ImageFile", _imageContent.Headers.GetValues("FileName").LastOrDefault());

        //Console.WriteLine("pre update call");

        //var httpResponse = await VideosClient.UpdateAuthor(userFormData);

        Console.WriteLine("post update call");

        //Console.WriteLine("httpResponse: " + httpResponse);
        //Console.WriteLine("httpResponse.Success: " + httpResponse.Success);


        //if (httpResponse.Success)
        //{
        //    var successResult = httpResponse.Response as SuccessResult<string>;
        //    Snackbar?.Add(successResult?.Result, Severity.Success);
        //    NavigationManager?.NavigateTo("content/videos");
        //}
        //else
        //{
        //    var exceptionResult = httpResponse.Response as ExceptionResult;
        //    _editContextServerSideValidator?.Validate(exceptionResult);
        //    _serverSideValidator?.Validate(exceptionResult);
        //}
    }

    private async Task<IEnumerable<AuthorItemForAutoComplete>> SearchAuthors(string? value)
    {
        System.Console.WriteLine("author auto complete value: " + value ?? "value is null");

        var responseWrapper = await AuthorsClient.GetAuthorsForAutoComplete(
            new GetAuthorsForAutoCompleteQuery{
            PageNumber = 50,
            SearchText = value ?? string.Empty
        });

         if (responseWrapper.Success)
        {
            var successResult = responseWrapper.Response as SuccessResult<AuthorsForAutoCompleteResponse>;
            if (successResult != null)
                _authorsForAutoResponse = successResult.Result;
        }
        else
        {
            var exceptionResult = responseWrapper.Response as ExceptionResult;
            _serverSideValidator.Validate(exceptionResult);
        }

        return _authorsForAutoResponse.Authors.Items;        
    }

    private IEnumerable<string> ValidateAuthor(string? value)
    {
        var author = _authorsForAutoResponse?.Authors?.Items.FirstOrDefault(a => a.Name.Equals(value));
        if (author is null)
        {
            yield return Resource.Author_by_that_name_not_found;
        }
    }
    #endregion Private Methods
}
