using Application.DAL.Contexts;
using Domain.Models.Common;
using System.Linq.Expressions;

namespace Application.DAL.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntity
    {
        ApplicationDbContext DbContext { get; }

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        bool SaveChanges();

        Task<bool> SaveChangesAsync();

        void Detach(TEntity entity);
    }
}
