using Domain.Common;

namespace Domain.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string? Username { get; set; }
    }
}
