using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;

namespace ElevateOTT.ClientPortal.Pages.Content.Authors;

public partial class EditAuthor : ComponentBase
{
    #region Public Properties
    public EditAuthor()
    {
        //AddedAuthorReferencesList = new List<ReferenceItemForAdd>();
        //ModifiedAuthorReferencesList = new List<ReferenceItemForEdit>();
        //RemovedAuthorReferencesList = new List<string>();
    }

    [Parameter] public Guid AuthorId { get; set; }

    public List<ReferenceItemForAdd> AddedAuthorReferencesList { get; set; }
    public List<ReferenceItemForEdit> ModifiedAuthorReferencesList { get; set; }
    public List<string> RemovedAuthorReferencesList { get; set; }

    #endregion Public Properties

    #region Private Properties
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IBreadcrumbService? BreadcrumbService { get; set; }
    [Inject] private IAuthorsClient? AuthorsClient { get; set; }

    private string? _imageSrc;

    private string _recommendedResolution = "700x700";
    private StreamContent? _imageContent { get; set; }
    private ServerSideValidator? _serverSideValidator { get; set; }
    private EditContextServerSideValidator? _editContextServerSideValidator { get; set; }
    private AuthorForEdit _authorForEditVm { get; set; } = new();
    private UpdateAuthorCommand? _updateAuthorCommand { get; set; }
    #endregion Private Properties

    #region Protected Methods
    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Authors, "/content/authors"),
            new(Resource.Edit_Author, "#", true)
        });

        var httpResponseWrapper = await AuthorsClient.GetAuthor(AuthorId);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<AuthorForEdit>;
            _authorForEditVm = successResult?.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            _serverSideValidator.Validate(exceptionResult);
        }
    }
    #endregion Protected Methods

    #region Private Methods
    private void GetBase64StringImageUrl(string imageSrc)
    {
        _imageSrc = imageSrc;
    }

    private void ImageSelected(StreamContent content)
    {
        _imageContent = content;
        _authorForEditVm.IsImageAdded = true;
    }

    private void ImageUnSelected()
    {
        _imageContent = null;
        _authorForEditVm.IsImageAdded = false;
    }

    private bool HasImage()
    {
        return string.IsNullOrWhiteSpace(_authorForEditVm.ImageUrl) && string.IsNullOrWhiteSpace(_imageSrc);
    }
    private void UpdateRteValue(string value)
    {
        _authorForEditVm.Bio = value;
    }
    private async Task SubmitForm()
    {
        // TODO remove confirmation dialog 

        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Are_you_sure_you_want_to_save_author},
            {"ButtonText", Resource.Yes},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>("Confirm", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            _updateAuthorCommand = new UpdateAuthorCommand
            {
                Id = _authorForEditVm.Id,
                Name = _authorForEditVm.Name,
                Bio = _authorForEditVm.Bio,
                ImageUrl = _authorForEditVm.ImageUrl,
                SeoTitle = _authorForEditVm.SeoTitle,
                SeoDescription = _authorForEditVm.SeoDescription,
                Slug = _authorForEditVm.Slug,
                IsImageAdded = _authorForEditVm.IsImageAdded
            };



            var userFormData = new MultipartFormDataContent
            {
                { new StringContent(_updateAuthorCommand.Id.ToString() ?? string.Empty), "id" },
                { new StringContent(_updateAuthorCommand.Name ?? string.Empty), "Name" },
                { new StringContent(_updateAuthorCommand.Bio ?? string.Empty), "Bio" },
                { new StringContent(_updateAuthorCommand.ImageUrl ?? string.Empty), "ImageUrl" },
                { new StringContent(_updateAuthorCommand.SeoTitle ?? string.Empty), "SeoTitle" },
                { new StringContent(_updateAuthorCommand.SeoDescription ?? string.Empty), "SeoDescription" },
                { new StringContent(_updateAuthorCommand.Slug ?? string.Empty), "Slug" },
                { new StringContent(_updateAuthorCommand.IsImageAdded.ToString()), "IsImageAdded" },
            };

            if (_imageContent != null)
                userFormData.Add(_imageContent, "AuthorImage", _imageContent.Headers.GetValues("FileName").LastOrDefault());


            var httpResponse = await AuthorsClient.UpdateAuthor(_updateAuthorCommand);

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
    }
    #endregion Private Methods
}
