using Application.Services;
using Domain.Entity;
using System.Linq;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "Bob"
            },
            new User
            {
                Id = 2,
                Username = "John"
            },
            new User
            {
                Id = 3,
                Username = "Tom"
            },
            new User
            {
                Id = 4,
                Username = "Max"
            }
        };

        public User? GetById(int userId)
        {
            return Users.FirstOrDefault(user => user.Id == userId);
        }

        public IEnumerable<User> ListUsers(int pageSize, int pageIndex)
        {
            return Users.AsEnumerable();
        }
    }
}
