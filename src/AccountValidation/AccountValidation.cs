using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using Skail.Platform.Runtime.Schedulers;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class AccountValidation : IAccountValidation
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AccountValidation> _logger;
    private readonly IEmailSender _emailSender;
    
    public AccountValidation(UserManager<ApplicationUser> userManager, 
        IEmailSender emailSender,
        ILogger<AccountValidation> logger)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _logger = logger;
    }
    
    public Task ValidateAccount(string email, int count)
    {
        var waitSeconds = Environment.GetEnvironmentVariable("WAIT_SECONDS");

        var resultParse = Int32.TryParse(waitSeconds, out var seconds);
        if (!resultParse)
        {
            seconds = 60 * 60 * 24; // Default for unit is day, used for demonstration 
        }
        
        var user = _userManager.FindByEmailAsync(email).Result;
        if (user == null)
        {
            return Task.CompletedTask;
        }
        
        _logger.LogInformation("result.EmailConfirmed: {}; password: {}", user.EmailConfirmed, user.PasswordHash);
        if (!user.EmailConfirmed)
        {
            var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var webSrvAddr = Environment.GetEnvironmentVariable("WEB_SRV_ADDR");
            if (webSrvAddr == null)
            {
                webSrvAddr = "https://localhost:5001";
            }
            var callbackUrl = webSrvAddr + "confirm-email/get?userId=" + user.Id + "&code=" + code;
            if (!user.PasswordHash.EndsWith("_blocked"))
            {
                if (count < 15)
                {
                    var message = "Not confirmed account will be blocked on " + (15 - count) + " days<br>. Confirm using this link: " + callbackUrl;
                    _emailSender.SendEmailAsync(email, "Email confirmation", message);
                    this.RunAt<IAccountValidation>(DateTime.Now.AddSeconds(seconds), true).ValidateAccount(email, count + 1);
                }
                else
                {
                    var message = "Not confirmed account will be deleted on 3 days<br>. Confirm using this link: " + callbackUrl;
                    _emailSender.SendEmailAsync(email, "Email confirmation", message);
                    user.PasswordHash += "_blocked";
                    var _ = _userManager.UpdateAsync(user).Result;
                    this.RunAt<IAccountValidation>(DateTime.Now.AddSeconds(seconds), true).ValidateAccount(email, 1);
                }
            }
            else
            {
                if (count < 3)
                {
                    var message = "Not confirmed account will be deleted on " + (3 - count) + " days<br>. Confirm using this link: " + callbackUrl;
                    _emailSender.SendEmailAsync(email, "Email confirmation", message);
                    this.RunAt<IAccountValidation>(DateTime.Now.AddSeconds(seconds), true).ValidateAccount(email, count + 1);
                } else
                {
                    var message = "Not confirmed account was deleted.";
                    _emailSender.SendEmailAsync(email, "Email confirmation", message);
                    var _ = _userManager.DeleteAsync(user).Result;
                }
            }
        }

        return Task.CompletedTask;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var waitSeconds = Environment.GetEnvironmentVariable("WAIT_SECONDS");

        var resultParse = Int32.TryParse(waitSeconds, out var seconds);
        if (!resultParse)
        {
            seconds = 60 * 60 * 24; // Default for unit is day, used for demonstration 
        }
        _emailSender.SendEmailAsync(email, subject, htmlMessage);
        this.RunAt<IAccountValidation>(DateTime.Now.AddSeconds(seconds), true).ValidateAccount(email, 1);
        return Task.CompletedTask;
    }
}
