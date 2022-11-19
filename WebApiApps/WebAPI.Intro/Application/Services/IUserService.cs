using Domain.Entity;

namespace Application.Services
{
    public interface IUserService
    {
        IEnumerable<User> ListUsers(int pageSize, int pageIndex);

        User? GetById(int userId);
    }
}
