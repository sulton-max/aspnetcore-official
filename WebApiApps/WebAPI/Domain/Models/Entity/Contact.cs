using Domain.Models.Common;

namespace Domain.Models.Entity
{
    /// <summary>
    /// Represents contact informatino
    /// </summary>
    public class Contact : IEntity
    {
        public int Id { get; set; }

        public string Mobile { get; set; }
    }
}
