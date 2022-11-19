using Domain.Models.Common;

namespace Domain.Models.Entity
{
    /// <summary>
    /// Provides Book resource entity model
    /// </summary>
    public partial class Book : IEntity, ICloneable<Book>
    {
        #region Constructors

        public Book()
        { }

        public Book(string name)
        {
            Name = name;
        }

        #endregion

        public Book Clone(bool shallowCopy = true)
        {
            return shallowCopy
                ? (Book)this.MemberwiseClone()
                : new Book(Name);
        }
    }
}
