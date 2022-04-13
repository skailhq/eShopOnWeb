using Skail.Platform.Runtime.Schedulers.Abstractions;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public interface IEmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, IDelayable
{
    
}
