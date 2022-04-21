using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public static class ConfigureSendemailServices
{
    public static IServiceCollection AddSendemailServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // used by Identity, now into services.AddAccountValidationServices (project AccountValidation)
        // services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<IGenerateDateTimeCommand, GenerateDateTimeCommand>();

        return services;
    }
}

