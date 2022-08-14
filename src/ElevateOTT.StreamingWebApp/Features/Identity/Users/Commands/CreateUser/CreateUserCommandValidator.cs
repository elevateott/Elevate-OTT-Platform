﻿using Resource = ElevateOTT.BackendResources.Resource;

namespace ElevateOTT.StreamingWebApp.Features.Identity.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    #region Public Constructors

    public CreateUserCommandValidator()
    {
        //RuleFor(v => v.AvatarUri).Cascade(CascadeMode.Stop)
        //    .NotEmpty().WithMessage(BackendResources.Resource.Profile_picture_is_required)
        //    .When(v => v.IsAvatarAdded == false);

        RuleFor(v => v.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.First_name_is_required);

        //RuleFor(v => v.NumberOfAttachments).Cascade(CascadeMode.Stop)
        //    .LessThanOrEqualTo(3).WithMessage(BackendResources.Resource.Maximum_allowed_number_of_attachments_must_not_exceed_3)
        //    .GreaterThanOrEqualTo(1).WithMessage(BackendResources.Resource.Minimum_allowed_number_of_attachments_must_be_at_least_1);

        RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Username_is_required)
            .EmailAddress().WithMessage(v => string.Format(BackendResources.Resource.Username_is_invalid, v.Email))
            .MaximumLength(100).WithMessage(BackendResources.Resource.Username_must_not_exceed_200_characters)
            .MinimumLength(6).WithMessage(BackendResources.Resource.Username_must_be_at_least_6_characters);

        RuleFor(v => v.Password).Cascade(CascadeMode.Stop)
                                .NotEmpty().WithMessage(BackendResources.Resource.Password_is_required)
                                .MaximumLength(100).WithMessage(BackendResources.Resource.Password_must_not_exceed_200_characters)
                                .MinimumLength(6).WithMessage(BackendResources.Resource.Password_must_be_at_least_6_characters)
                                .When(v => v.SetRandomPassword == false && string.IsNullOrWhiteSpace(v.Password));

        RuleFor(v => v.ConfirmPassword).Cascade(CascadeMode.Stop)
                                       .NotEmpty().WithMessage(BackendResources.Resource.Confirm_password_is_required)
                                       .Equal(v => v.Password).WithMessage(Resource.The_password_and_confirmation_password_do_not_match)
                                       .When(v => v.SetRandomPassword == false && string.IsNullOrWhiteSpace(v.Password));
    }

    #endregion Public Constructors
}