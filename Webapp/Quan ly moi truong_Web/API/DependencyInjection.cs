

using API.Common.Errors;
using API.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {

            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, WebProblemDetailFactory>();
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            services.AddMappings();
            return services;
        }

    }
}
