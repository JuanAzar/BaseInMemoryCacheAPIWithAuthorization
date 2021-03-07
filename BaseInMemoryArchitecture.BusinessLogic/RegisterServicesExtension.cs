using Microsoft.Extensions.DependencyInjection;

namespace BaseInMemoryArchitecture.BusinessLogic
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface()
                .WithScopedLifetime());

            return services;
        }
    }
}
