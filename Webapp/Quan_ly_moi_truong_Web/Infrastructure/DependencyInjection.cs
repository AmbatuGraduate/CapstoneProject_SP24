﻿using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.Common.Interfaces.Services;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Calendar;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;


namespace Infrastructure
{

    // Update At: 17/02/2024
    // Update By: Dang Nguyen Khanh Vu
    // Changes:
    // - Thêm binding value  giữa GoogleApiSettings và  các thông tin bên appseting.json (phải trùng tên với nhau)

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

            // add session
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSession();


            // Add repositories dependency injection
           services.AddScoped<ISessionService, SessionService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITreeRepository, TreeRepository>();
            services.AddScoped<IStreetRepository, StreetRepository>();
            services.AddScoped<ITreeTypeRepository, TreeTypeRepository>();
            services.AddScoped<ICultivarRepository, CultivarRepository>();
            services.AddScoped<Func<GoogleCredential, CalendarService>>(provider => (credential) =>
            {
                return new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "cay-xanh"
                });
            });

            services.AddScoped<Func<GoogleCredential, DirectoryService>>(provider => (credential) =>
            {
                return new DirectoryService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "cay-xanh"
                });
            });

            // calendar service registration
            services.AddScoped<ITreeCalendarService, TreeCalendarService>();

            return services;
        }

        // Add database context
        public static IServiceCollection AddPersistance(
            this IServiceCollection services)
        {
            services.AddDbContext<WebDbContext>(opts =>
            {
                /*                opts.UseSqlServer("Server=tcp:urban-sanitation.database.windows.net,1433;Initial Catalog=UrbanSanitationDB;Persist Security Info=False;User ID=adminServer;Password=Urbansanitation357;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                */
                opts.UseSqlServer("Server=20.2.70.147,1433;Initial Catalog=UrbanSanitationDB;Persist Security Info=False;User ID=ad;Password=Urban123;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;");
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

            var googleApiSettings = new GoogleApiSettings();
            configuration.Bind(GoogleApiSettings.SectionName, googleApiSettings);

            // Add IOptions where we can request the jwt setting
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton(Options.Create(googleApiSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(opts =>
                {
                    opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

                })
                .AddCookie()
                .AddJwtBearer(opts =>
                {
                    opts.RequireHttpsMetadata = false;
                    opts.SaveToken = true;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };

                    //Get token from cookie
                    opts.Events = new()
                    {
                        OnMessageReceived = context =>
                        {
                            var request = context.HttpContext.Request;
                            var cookies = request.Cookies;
                            if (cookies.TryGetValue("token_v2",
                                out var accessTokenValue))
                            {
                                context.Token = accessTokenValue;
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddGoogle(options =>
                {
                    options.ClientId = googleApiSettings.ClientId;
                    options.ClientSecret = googleApiSettings.ClientSecret;
                    options.Scope.Add("https://www.googleapis.com/auth/calendar");
                });

            return services;
        }
    }
}