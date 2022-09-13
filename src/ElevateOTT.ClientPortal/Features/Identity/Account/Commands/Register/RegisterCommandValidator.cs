namespace ElevateOTT.ClientPortal.Features.Identity.Account.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    #region Public Constructors

    // TODO add validation for other fields
    // Check if email address already in use

    public RegisterCommandValidator()
    {
        RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Email_is_required)
            .EmailAddress().WithMessage(v => string.Format(BackendResources.Resource.Email_is_invalid, v.Email))
            .MaximumLength(60).WithMessage(BackendResources.Resource.Email_must_not_exceed_60_characters)
            .MinimumLength(6).WithMessage(BackendResources.Resource.Email_must_be_at_least_6_chars);

        RuleFor(v => v.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Password_is_required)
            .MaximumLength(20).WithMessage(BackendResources.Resource.Passwords_must_be_at_least_length_characters)
            .MinimumLength(6).WithMessage(BackendResources.Resource.Password_must_be_at_least_6_characters);

        RuleFor(v => v.ConfirmPassword).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Confirm_password_is_required)
            .Equal(v => v.Password).WithMessage(BackendResources.Resource.The_password_and_confirmation_password_do_not_match);
    }

    #endregion Public Constructors
}
