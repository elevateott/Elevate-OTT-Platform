﻿namespace BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    #region Public Constructors

    public ConfirmEmailCommandValidator()
    {
        RuleFor(v => v.UserId).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.User_Id_is_required);

        RuleFor(v => v.Code).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Code_is_required);
    }

    #endregion Public Constructors
}