namespace Domain.Models.Common
{
    public interface ICloneable<TModel> where TModel : class
    {
        /// <summary>
        /// Creates a clone of this object
        /// </summary>
        /// <param name="shallowCopy">Defines either shallow copy or deep copy</param>
        /// <returns>Either deep or shallow copied model</returns>
        TModel Clone(bool shallowCopy = true);
    }
}
