using Microsoft.eShopWeb.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Skail.Platform.Runtime;
using Skail.Platform.Runtime.Core;

[assembly:VisibleToSkailPlatform]

var trigger = Environment.GetEnvironmentVariable("TRIGGER");

var builder = Host.CreateDefaultBuilder(args);
if (trigger.IsNullOrEmpty() || trigger != "true") {
    builder.UseSkailServiceProviderFactory();
}

builder.ConfigureServices( (hostContext, services) =>
{
    services.AddAccountValidationServices(hostContext.Configuration);
});

var host = builder.Build();
if (trigger.IsNullOrEmpty() || trigger != "true")
{
    await Platform.Initialize().RunAsync();
}
else
{
    var _accountValidation = (IAccountValidation?) host.Services.GetService(typeof(IAccountValidation));
    if (_accountValidation!= null) await _accountValidation.ValidateAccount("teste@teste.com", 1);
}
