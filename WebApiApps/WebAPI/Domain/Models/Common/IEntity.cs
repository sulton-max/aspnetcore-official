namespace Domain.Models.Common
{
    /// <summary>
    /// Defines common entity model properties
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Represents entity model Primary Key
        /// </summary>
        int Id { get; set; }
    }
}
