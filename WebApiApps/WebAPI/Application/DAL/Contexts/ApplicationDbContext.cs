using Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Application.DAL.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
    }
}
