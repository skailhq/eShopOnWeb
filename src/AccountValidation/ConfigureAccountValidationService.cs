using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public static class ConfigureAccountValidationServices
{
    public static IServiceCollection AddAccountValidationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var skail_func = Environment.GetEnvironmentVariable("SKAIL_FUNC");
        if (skail_func != null)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
       
        services.AddTransient<IAccountValidation, AccountValidation>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, AccountValidation>();
        services.AddSendemailServices(configuration);
        
        return services;
    }
}
