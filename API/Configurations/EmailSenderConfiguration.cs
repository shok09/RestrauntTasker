using API.Models.Settings;
using API.Services.AuthEmailSender;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configurations
{
    internal static class EmailSenderConfiguration
    {
        public static IServiceCollection AddEmailSenderConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            var mailSettings = configuration.GetSection(nameof(AuthMessageSenderOptions));
            services.Configure<AuthMessageSenderOptions>(mailSettings);

            return services;
        }
    }
}
