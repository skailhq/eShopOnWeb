using Skail.Platform.Runtime.Configuration.Attributes;
using Skail.Platform.Runtime.Schedulers.Abstractions;

namespace Microsoft.eShopWeb.Infrastructure.Services;

[ServiceInterface]
public interface IEmailSender : IDelayable
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
