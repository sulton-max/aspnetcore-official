using Application.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void AddAppServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
        }
    }
}
