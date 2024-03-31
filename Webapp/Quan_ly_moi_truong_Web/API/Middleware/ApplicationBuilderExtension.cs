using Application.Common.Interfaces.Persistence.Notifiy;

namespace API.Middleware
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder)
            where T : ISubscribeTableDependency
        {
            Console.WriteLine("middleware sql dependency");
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            Console.WriteLine("middleware sql dependency, service is null? " + (service == null));
            service?.SubscribeTableDependency();
        }
    }
}