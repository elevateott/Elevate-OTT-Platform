﻿namespace BinaryPlate.BlazorPlate.Services;

public interface INavigationService
{
    #region Public Methods

    Task NavigateToUrlAsync(string url, bool openInNewTab);

    #endregion Public Methods
}