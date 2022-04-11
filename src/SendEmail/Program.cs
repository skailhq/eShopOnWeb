using Microsoft.eShopWeb.Web.Configuration;
using Microsoft.Extensions.Hosting;
using Skail.Platform.Runtime;
using Skail.Platform.Runtime.Core.DependencyInjection;

var builder = Host.CreateDefaultBuilder(args);
builder.UseServiceProviderFactory(PlatformServiceProviderFactory.Current);
builder.ConfigureServices( (hostContext, services) =>
{
    services.AddCoreServices(hostContext.Configuration);
});
var funcHost = builder.Build();
await Platform.Initialize(funcHost).RunAsync();
