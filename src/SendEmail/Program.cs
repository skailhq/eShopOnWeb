using Microsoft.eShopWeb.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skail.Platform.Runtime;
using Skail.Platform.Runtime.Core;

[assembly:VisibleToSkailPlatform]

var builder = Host.CreateDefaultBuilder(args);
builder.UseSkailServiceProviderFactory();
builder.ConfigureServices( (hostContext, services) =>
{
    // used by Identity 
    services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
    services.AddTransient<IEmailSender, EmailSender>();
    
    services.AddScoped<IGenerateDateTimeCommand, GenerateDateTimeCommand>();
});
builder.Build();
await Platform.Initialize().RunAsync();
