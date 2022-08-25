﻿using ElevateOTT.ClientPortal.Features.Content.Categories.Commands.UpdateCategory;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategoryForEdit;
using System.ComponentModel;

namespace ElevateOTT.ClientPortal.Pages.Content.Categories;

public partial class EditCategory : ComponentBase
{
    #region Public Properties

    [Parameter] public Guid CategoryId { get; set; }

    #endregion Public Properties

    #region Private Properties
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IBreadcrumbService? BreadcrumbService { get; set; }
    [Inject] private ICategoriesClient? CategoriesClient { get; set; }

    private string? _imageSrc;

    //
    // TODO these values should come from config
    //
    private string _recommendedResolution = "700x700";
    private string _slugExampleName = "strength-training";
    private int _maxNameChars = 60;
    private int _maxSeoTitleChars = 60;
    private int _maxSeoDescriptionChars = 170;
    private int _maxSlugChars = 60;

    private string SlugPlaceholder => _slugExampleName;

    // TODO getters and setters ??????
    private StreamContent? _imageContent { get; set; }
    private ServerSideValidator? _serverSideValidator { get; set; }
    private EditContextServerSideValidator? _editContextServerSideValidator { get; set; }
    private CategoryForEdit _categoryForEditVm { get; set; } = new();
    private UpdateCategoryCommand? _updateCategoryCommand { get; set; }

    #endregion Private Properties

    #region Protected Methods
    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService?.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Categories, "/content/categories"),
            new("Edit Category", "#", true)
        });

        var httpResponseWrapper = await CategoriesClient.GetCategory(CategoryId);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CategoryForEdit>;
            _categoryForEditVm = successResult?.Result;
            _categoryForEditVm.PropertyChanged += NameChangedHandler;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            _serverSideValidator.Validate(exceptionResult);
        }
    }
    #endregion Protected Methods

    #region Private Methods
    private void NameChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        _categoryForEditVm.Slug = _categoryForEditVm.Title.FormatSlug();
        StateHasChanged();
    }

    private void GetBase64StringImageUrl(string imageSrc)
    {
        _imageSrc = imageSrc;
        Console.WriteLine(_imageSrc);
        StateHasChanged();
    }

    private void ImageSelected(StreamContent content)
    {
        _imageContent = content;
        _categoryForEditVm.IsImageAdded = true;
        StateHasChanged();
    }

    private void ImageUnSelected()
    {
        _imageContent = null;
        _categoryForEditVm.IsImageAdded = false;
        StateHasChanged();
    }

    private bool HasUploadedImage()
    {
        return !string.IsNullOrWhiteSpace(_imageSrc);
    }

    private void UpdateRteValue(string value)
    {
        _categoryForEditVm.Description = value;
    }

    private async Task SubmitForm()
    {
        // TODO guard clauses

        Console.WriteLine("SubmitForm");

        _updateCategoryCommand = new UpdateCategoryCommand
        {
            Id = _categoryForEditVm.Id,
            Title = _categoryForEditVm.Title,
            Description = _categoryForEditVm.Description,
            Position = _categoryForEditVm.Position,
            ImageUrl = _categoryForEditVm.ImageUrl,
            SeoTitle = _categoryForEditVm.SeoTitle,
            SeoDescription = _categoryForEditVm.SeoDescription,
            Slug = _categoryForEditVm.Slug.FormatSlug(),
            IsImageAdded = _categoryForEditVm.IsImageAdded
        };


        var userFormData = new MultipartFormDataContent
        {
            { new StringContent(_updateCategoryCommand.Title ?? string.Empty), "Title" },
            { new StringContent(_updateCategoryCommand.Description ?? string.Empty), "Description" },
            { new StringContent(_updateCategoryCommand.Position.ToString()), "Position" },
            { new StringContent(_updateCategoryCommand.ImageUrl ?? string.Empty), "ImageUrl" },
            { new StringContent(_updateCategoryCommand.SeoTitle ?? string.Empty), "SeoTitle" },
            { new StringContent(_updateCategoryCommand.SeoDescription ?? string.Empty), "SeoDescription" },
            { new StringContent(_updateCategoryCommand.Slug ?? string.Empty), "Slug" },
            { new StringContent(_updateCategoryCommand.IsImageAdded.ToString()), "IsImageAdded" },
        };

        if (_imageContent != null)
            userFormData.Add(_imageContent, "ImageFile", _imageContent.Headers.GetValues("FileName").LastOrDefault());

        Console.WriteLine("pre update call");

        var httpResponse = await CategoriesClient.UpdateCategory(userFormData);

        Console.WriteLine("post update call");

        Console.WriteLine("httpResponse: " + httpResponse);
        Console.WriteLine("httpResponse.Success: " + httpResponse.Success);

        if (httpResponse.Success)
        {
            var successResult = httpResponse.Response as SuccessResult<string>;
            Snackbar?.Add(successResult?.Result, Severity.Success);
            NavigationManager?.NavigateTo("content/categories");
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
