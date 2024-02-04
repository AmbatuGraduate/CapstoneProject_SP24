
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ErrorOr;
using Application.Authentication.Common;
using FluentValidation;
using System.Reflection;
using Application.Common.Behaviours;

namespace Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehaviours<,>)
                );

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
