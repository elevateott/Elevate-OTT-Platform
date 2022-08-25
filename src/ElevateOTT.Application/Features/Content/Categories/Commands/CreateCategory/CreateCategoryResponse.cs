﻿namespace ElevateOTT.Application.Features.Content.Categories.Commands.CreateCategory;

public class CreateCategoryResponse
{
    #region Public Properties

    public Guid Id { get; set; }
    public string SuccessMessage { get; set; } = string.Empty;

    #endregion Public Properties
}
