﻿namespace BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.Register;

public class RegisterCommand
{
    #region Public Properties

    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ReturnUrl { get; set; }

    #endregion Public Properties
}