using API.Common.Errors;
using API.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });

            services.AddControllers();
            services.AddHttpClient();
            services.AddDistributedMemoryCache();

            services.AddSession(cfg =>
            {
                cfg.Cookie.Name = "tokenv2";
                cfg.IdleTimeout = new TimeSpan(0, 60, 0);
            });

            services.AddSingleton<ProblemDetailsFactory, WebProblemDetailFactory>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:3000", "http://localhost:5500", 
                                     "https://vesinhdanang.xyz", "http://vesinhdanang.xyz", 
                                     "https://localhost:7024", "https://organic-space-lamp-5jvpr577qxjf77w4-3000.app.github.dev/login")
                        //.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });

            services.AddMappings();
            services.AddControllers();
            //services.AddProblemDetails();
            return services;
        }
    }
}
