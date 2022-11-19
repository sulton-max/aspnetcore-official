using Domain.Models.Common;

namespace Domain.Models.Entity
{
    /// <summary>
    /// Represents Book resource entity model
    /// </summary>
    public partial class Book : IEntity, ICloneable<Book>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = null;
    }
}
