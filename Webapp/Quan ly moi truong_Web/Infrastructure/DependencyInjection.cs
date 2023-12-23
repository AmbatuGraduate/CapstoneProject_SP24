using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration
            )
        {
            services.AddAuth(configuration);
            services.AddPersistance();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITreeRepository, TreeRepository>();
            return services;
        }

        public static IServiceCollection AddPersistance(
            this IServiceCollection services)
        {
            services.AddDbContext<WebDbContext>(opts =>
            {
                opts.UseSqlServer("Server=tcp:sampleazuredemo.database.windows.net,1433;Initial Catalog=DummyDB;Persist Security Info=False;User ID=vu;Password=2646347.com;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            return services;
        }


        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration
            )
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            // Add IOptions where we can request the jwt setting
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });


            return services;
        }

    }
}
