using Application.DAL.Contexts;
using Domain.Models.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DAL.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext == null)
                throw new InvalidOperationException();

            if (!dbContext.Books.Any())
                dbContext.Books.AddRange(new[]
                {
                new Book
                {
                    Name = "API Design patterns"
                },
                new Book
                {
                    Name = "Algorithms to Live by"
                },
                  new Book
                {
                    Name = "Mastermind"
                },
                new Book
                {
                    Name = "Ask Powerful Questions"
                },
                  new Book
                {
                    Name = "Head First Design Patterns"
                },
                new Book
                {
                    Name = "Cracking the Coding Interview"
                }
            });

            if (!dbContext.Contacts.Any())
                dbContext.Contacts.AddRange(new[]
                {

                    new Contact
                    {
                        Mobile = "998999663258"
                    }
                });

            dbContext.SaveChanges();
        }
    }
}
