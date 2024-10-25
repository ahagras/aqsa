using FluentValidation;

namespace Aqsa.Domain.Identity.UserRegistration.VerifyRegistrationRequest
{
    internal class VerifyRegistrationRequestCommandValidator : AbstractValidator<VerifyRegistrationRequestCommand>
    {
        public VerifyRegistrationRequestCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(c => c.ReferenceId)
                .NotEmpty();

            RuleFor(c => c.OTP)
                .NotEmpty()
                .Length(4);

        }
    }
}
