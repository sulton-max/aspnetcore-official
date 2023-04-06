using Application.DAL.Contexts;
using Application.DAL.Repositories;
using Domain.Models.Entity;

namespace Infrastructure.DAL.Repositories
{
    public class BookStoreRepository : RepositoryBase<Book>, IBookStoreRepository
    {
        public BookStoreRepository
        (
            ApplicationDbContext dbContext,
            IRepositoryBase<Book> bookRepository
        ) : base(dbContext)
        {
        }
    }
}
