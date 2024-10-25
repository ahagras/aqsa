using Aqsa.Domain.Common.Services.Cache;
using Aqsa.Domain.Entity;
using MediatR;

namespace Aqsa.Domain.Identity.UserRegistration.VerifyRegistrationRequest;
internal class VerifyRegistrationRequestCommandHandler(IRedisCacheService redisCacheService, IJADbContext jADbContext)
    : IRequestHandler<VerifyRegistrationRequestCommand, string>
{
    public async Task<string> Handle(VerifyRegistrationRequestCommand request, CancellationToken cancellationToken)
    {
        var cachedkey = $"reg:{request.Email}";

        var reg = await redisCacheService.GetStringAsync(cachedkey);

        if (string.IsNullOrEmpty(reg))
            throw new Exception();

        var decodedData = reg.Split("/#4744#/");

        var transactionId = decodedData[1];
        var otp = decodedData[2];

        if (transactionId != request.ReferenceId)
            throw new Exception();

        if (otp != request.OTP)
            throw new Exception();

        var user = new User("", "", "", "");

        await jADbContext.Users.AddAsync(user, cancellationToken);

        await jADbContext.SaveChangesAsync(cancellationToken);

        await redisCacheService.RemoveKeyAsync(cachedkey);

        return transactionId;
    }
}
