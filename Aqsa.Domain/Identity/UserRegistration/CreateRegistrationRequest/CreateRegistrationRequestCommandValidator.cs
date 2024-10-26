// using FluentValidation;
//
// namespace Aqsa.Domain.Identity.UserRegistration.CreateRegistrationRequest
// {
//     public class CreateRegistrationRequestCommandValidator : AbstractValidator<CreateRegistrationRequestCommand>
//     {
//         public CreateRegistrationRequestCommandValidator()
//         {
//             RuleFor(c => c.Email)
//                 .NotEmpty()
//                 .EmailAddress()
//                 .MaximumLength(100);
//
//             RuleFor(c => c.Name)
//                 .NotEmpty()
//                 .MaximumLength(100);
//
//             RuleFor(c => c.Password)
//                 .NotEmpty()
//                 .MaximumLength(40);
//         }
//     }
// }
