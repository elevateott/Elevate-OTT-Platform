﻿namespace BinaryPlate.BlazorPlate.Interfaces;

public interface IAuthenticationService
{
    #region Public Methods

    Task Login(AuthResponse authResponse);

    Task ReAuthenticate(AuthResponse authResponse);

    Task Logout();

    #endregion Public Methods
}