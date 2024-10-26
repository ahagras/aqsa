using FluentValidation;
using MediatR;

namespace Aqsa.Domain.Common.Identity.UserRegistration;

public class CreateRegistrationRequest : IRequest<Result<string>>
{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public class Handler : IRequestHandler<CreateRegistrationRequest, Result<string>>
        {
                public Task<Result<string>> Handle(CreateRegistrationRequest request, CancellationToken cancellationToken)
                {
                        throw new NotImplementedException();
                }
        }
        
        public class Validator:AbstractValidator<CreateRegistrationRequest>
        {
                public Validator()
                {
                        RuleFor(c => c.Email)
                                .NotEmpty()
                                .EmailAddress()
                                .MaximumLength(100);

                        RuleFor(c => c.Name)
                                .NotEmpty()
                                .MaximumLength(100);

                        RuleFor(c => c.Password)
                                .NotEmpty()
                                .MaximumLength(40);
                } 
        }
}

