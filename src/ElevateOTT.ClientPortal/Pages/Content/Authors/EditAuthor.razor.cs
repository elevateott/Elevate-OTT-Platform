using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;

namespace ElevateOTT.ClientPortal.Pages.Content.Authors;

public partial class EditAuthor : ComponentBase
{
    #region Public Properties
    public EditAuthor()
    {
        AddedAuthorReferencesList = new List<ReferenceItemForAdd>();
        ModifiedAuthorReferencesList = new List<ReferenceItemForEdit>();
        RemovedAuthorReferencesList = new List<string>();
    }

    [Parameter] public string AuthorId { get; set; } = string.Empty;

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

    private ServerSideValidator? ServerSideValidator { get; set; }
    private EditContextServerSideValidator? EditContextServerSideValidator { get; set; }
    private AuthorForEdit AuthorForEditVm { get; set; } = new();
    private UpdateAuthorCommand? UpdateAuthorCommand { get; set; }
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

        var httpResponseWrapper = await AuthorsClient.GetAuthor(new GetAuthorForEditQuery
        {
            Id = Guid.Parse(AuthorId),
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<AuthorForEdit>;
            AuthorForEditVm = successResult?.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }
    #endregion Protected Methods

    #region Private Methods
    private void UpdateRteValue(string value)
    {
        AuthorForEditVm.Bio = value;
    }
    private async Task SubmitForm()
    {
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
            UpdateAuthorCommand = new UpdateAuthorCommand
            {
                Id = AuthorForEditVm.Id,
                Name = AuthorForEditVm.Name,
                Bio = AuthorForEditVm.Bio,
                ImageUrl = AuthorForEditVm.ImageUrl,
                SeoTitle = AuthorForEditVm.SeoTitle,
                SeoDescription = AuthorForEditVm.SeoDescription,
                Slug = AuthorForEditVm.Slug
            };
            var httpResponse = await AuthorsClient.UpdateAuthor(UpdateAuthorCommand);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("content/authors");
            }
            else
            {
                var exceptionResult = httpResponse.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }
    #endregion Private Methods
}
