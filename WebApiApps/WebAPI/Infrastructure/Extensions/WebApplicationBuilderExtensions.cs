using Application.DAL.Contexts;
using Application.DAL.Repositories;
using BookStore.Domain.Extensions;
using Infrastructure.DAL.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        private static List<Type> GetDbSetTypes() =>
       typeof(ApplicationDbContext).GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Where(x => x.PropertyType.InheritsOrImplements(typeof(DbSet<>)))
           .Select(x => x.PropertyType.GenericTypeArguments.First())
           .ToList();

        public static WebApplicationBuilder AddAppInfrastructure(this WebApplicationBuilder builder)
        {
            builder
                .AddDbContexts()
                .AddRepositoreies()
                .AddAppServices();

            return builder;
        }

        public static WebApplicationBuilder AddDbContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("BookStoreDatabase"));
            return builder;
        }

        public static WebApplicationBuilder AddRepositoreies(this WebApplicationBuilder builder)
        {
            // Add generic repositories
            var repoType = typeof(IRepositoryBase<>);
            var targetType = typeof(RepositoryBase<>);

            GetDbSetTypes().ForEach(x =>
            {
                var specificInterface = repoType.MakeGenericType(x);
                var specificImplementation = targetType.MakeGenericType(x);
                builder.Services.AddScoped(specificInterface, specificImplementation);
            });

            // Add specific repositories
            builder.Services
                .AddScoped<IBookStoreRepository, BookStoreRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
        {
            // Add Base Entity services
            var repoType = typeof(IRepositoryBase<>);
            var serviceType = typeof(IEntityServiceBase<>);
            var targetType = typeof(EntityServiceBase<,>);

            GetDbSetTypes().ForEach(x =>
            {
                var specificInterface = serviceType.MakeGenericType(x);
                var specificRepoInterface = repoType.MakeGenericType(x);
                var specificImplementation = targetType.MakeGenericType(x, specificRepoInterface);
                builder.Services.AddScoped(specificInterface, specificImplementation);
            });

            //Add specific Entity services
            builder.Services.AddScoped<IBookService, BookService>();
            return builder;
        }
    }
}
