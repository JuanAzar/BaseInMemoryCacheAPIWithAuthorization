using Microsoft.Extensions.DependencyInjection;
using BaseInMemoryArchitecture.Common.Contracts;
using BaseInMemoryArchitecture.Common.Implementations;

namespace BaseInMemoryArchitecture.Common
{
    public static class RegisterCommonExtension
    {
        public static IServiceCollection RegisterApplicationCommon(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IJsonManager<>), typeof(JsonManager<>));
        }
    }
}
