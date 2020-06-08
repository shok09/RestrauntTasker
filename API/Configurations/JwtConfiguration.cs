using API.Services.JwtAuth.IssuerOptions;
using API.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using API.Services.JwtAuth;
using API.Services.JwtAuth.Interfaces;

namespace API.Configurations
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenValidator, TokenValidator>();

            var authSettings = configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);
            
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            var jwtAppSettingsOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(opt =>
            {
                opt.Issuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                opt.Audience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)];
                opt.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
        
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
           
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(config =>
            {
                config.ClaimsIssuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                config.TokenValidationParameters = tokenValidationParameters;
                config.SaveToken = true;

            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequirePerformerRole", policy => policy.RequireRole("Cook").RequireAuthenticatedUser());
                opt.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Chef").RequireAuthenticatedUser());
            });

            return services;
        }
    }
}
