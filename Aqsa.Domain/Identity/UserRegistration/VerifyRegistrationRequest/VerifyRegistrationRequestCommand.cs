using MediatR;

namespace Aqsa.Domain.Identity.UserRegistration.VerifyRegistrationRequest
{
    public record VerifyRegistrationRequestCommand : IRequest<string>
    {

        public string ReferenceId { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string OTP { get; init; } = default!;
    }
}
