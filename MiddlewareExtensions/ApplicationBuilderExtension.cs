using myapp.Interfaces;

namespace myapp.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder app,string connectionString) where T :ISubscribeTableDependency {
            var serviceProvider = app.ApplicationServices;
            var service=serviceProvider.GetService<T>();
            service?.SubscribeTableDependency(connectionString);
        }
    }
}
