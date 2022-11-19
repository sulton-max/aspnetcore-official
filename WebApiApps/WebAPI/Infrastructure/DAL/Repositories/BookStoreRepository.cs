using Application.DAL.Contexts;
using Application.DAL.Repositories;
using Domain.Models.Entity;

namespace Infrastructure.DAL.Repositories
{
    public class BookStoreRepository : RepositoryBase<Book>, IBookStoreRepository
    {
        public IRepositoryBase<Book> BookRepository { get; private set; }

        public BookStoreRepository
        (
            ApplicationDbContext dbContext,
            IRepositoryBase<Book> bookRepository
        ) : base(dbContext)
        {
            BookRepository = bookRepository;
        }
    }
}
