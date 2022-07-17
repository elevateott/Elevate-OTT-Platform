﻿namespace ElevateOTT.BlazorPlate.Interfaces.Consumers;

public interface IAccountsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> Register(RegisterCommand request);

    Task<HttpResponseWrapper<object>> Login(LoginCommand request);

    Task<HttpResponseWrapper<object>> ForgetPassword(ForgetPasswordCommand request);

    Task<HttpResponseWrapper<object>> ResetPassword(ResetPasswordCommand request);

    Task<HttpResponseWrapper<object>> ConfirmEmail(ConfirmEmailCommand request);

    Task<HttpResponseWrapper<object>> ResendEmailConfirmation(ResendEmailConfirmationCommand request);

    Task<HttpResponseWrapper<object>> LoginWith2Fa(LoginWith2FaCommand request);

    Task<HttpResponseWrapper<object>> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request);

    Task<HttpResponseWrapper<object>> RefreshToken(RefreshTokenCommand request);

    #endregion Public Methods
}