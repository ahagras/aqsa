using System.ComponentModel.DataAnnotations;
using Aqsa.Domain.Common;
using Aqsa.Domain.Common.Services.Cache;
using Aqsa.Domain.Common.Services.EmailSender;
using Aqsa.Domain.Services.Cache.CachedObjects;
using MediatR;
using Newtonsoft.Json;

namespace Aqsa.Domain.Identity.UserRegistration.CreateRegistrationRequest;
internal class CreateRegistrationRequestCommandHandler(
    IRedisCacheService redisCacheService,
    IEmailSenderService emailSenderService)
    : IRequestHandler<CreateRegistrationRequestCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRegistrationRequestCommand request, CancellationToken cancellationToken)
    {

        CreateRegistrationRequestCommandValidator validator = new CreateRegistrationRequestCommandValidator();
        var isValid = await validator.ValidateAsync(request, cancellationToken);
        
        var transactionId = EncryptionHelper.EncryptAndEncode(request.Email.ToLower());

        var transactionIdExists = await redisCacheService.KeyExistsAsync($"reg:{transactionId}");

        if (transactionIdExists)
            return Result<string>.Success("verify_registration"); ;

        var random = new Random();

        var otp = random.Next(1111, 9999).ToString();

        var userRegistrationModel = new UserRegistrationCachedObject
        {
            Email = request.Email,
            Name = request.Name,
            Otp = otp,
            Password = request.Password,
        };

        var userRegistrationData = JsonConvert.SerializeObject(userRegistrationModel);

        await redisCacheService.SetStringAsync($"reg:{transactionId}", userRegistrationData, TimeSpan.FromMinutes(30));

        await emailSenderService.SendEmailAsync(userRegistrationModel.Email, userRegistrationModel.Name, "Verify your Email", Email_Template_OTP(otp, "", 30));

        return Result<string>.Success("verify_registration");
    }

    private string Email_Template_OTP(string otp, string link, int expiryMinute) => $"<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>OTP Verification</title> <style> body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }} .container {{ width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }} .header {{ background-color: #007bff; color: #ffffff; padding: 5px; text-align: center; border-radius: 8px 8px 0 0; }} .content {{ padding: 20px; text-align: center; }} .otp {{ font-size: 24px; font-weight: bold; color: #007bff; }} .button {{ display: inline-block; padding: 10px 20px; margin-top: 20px; font-size: 16px; color: #ffffff; background-color: #007bff; text-decoration: none; border-radius: 5px; }} </style></head><body> <div class=\"container\"> <div class=\"header\"> <h2>OTP Verification</h2> </div> <div class=\"content\"> <p>Your One-Time Password (OTP) is:</p> <p class=\"otp\">{otp}</p> <p>Please use this code to complete your verification. This code is valid for {expiryMinute} minutes.</p> <a href=\"{link}\" class=\"button\">Verify Now</a> </div> </div></body></html>";

}
