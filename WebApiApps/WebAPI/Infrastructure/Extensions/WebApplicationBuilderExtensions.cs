using Application.DAL.Contexts;
using Application.DAL.Repositories;
using Domain.Models.Entity;
using Infrastructure.DAL.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
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
            builder.Services
                .AddScoped<IRepositoryBase<Book>, RepositoryBase<Book>>()
                .AddScoped<IBookStoreRepository, BookStoreRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookService, BookService>();
            return builder;
        }
    }
}
