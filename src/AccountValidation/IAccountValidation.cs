using Skail.Platform.Runtime.Schedulers.Abstractions;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public interface IAccountValidation : ISchedulable, Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
{
    public Task ValidateAccount(string email, int count);
}
