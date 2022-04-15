using Microsoft.eShopWeb.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Skail.Platform.Runtime;
using Skail.Platform.Runtime.Core;

[assembly:VisibleToSkailPlatform]

var builder = Host.CreateDefaultBuilder(args);
builder.UseSkailServiceProviderFactory();
builder.ConfigureServices( (hostContext, services) =>
{
    services.AddSendemailServices(hostContext.Configuration);
});
builder.Build();
await Platform.Initialize().RunAsync();
