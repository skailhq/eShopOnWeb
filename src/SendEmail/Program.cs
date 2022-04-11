using Microsoft.eShopWeb.Web.Configuration;
using Microsoft.Extensions.Hosting;
using Skail.Platform.Runtime;

var builder = Host.CreateDefaultBuilder(args);
builder.UseSkailServiceProviderFactory();
builder.ConfigureServices( (hostContext, services) =>
{
    services.AddCoreServices(hostContext.Configuration);
});
var funcHost = builder.Build();
await Platform.Initialize().RunAsync();
