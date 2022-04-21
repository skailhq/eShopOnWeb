using Skail.Platform.Runtime.Schedulers.Abstractions;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public interface IEmailSender : IDelayable
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
