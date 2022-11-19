using Microsoft.EntityFrameworkCore;
using WebAPI.Intro.Models;

namespace WebAPI.Intro.DAL.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
    }
}
