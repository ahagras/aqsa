namespace Aqsa.Domain.Services.Cache.CachedObjects;

internal class UserRegistrationCachedObject
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string Password { get; init; }
    public required string Otp { get; init; }
}
