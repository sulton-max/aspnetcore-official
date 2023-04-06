using Domain.Models.Entity;
using Domain.Models.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Services
{
    /// <summary>
    /// Provides methods to execute CRUD operatiosn on a Book resource
    /// </summary>
    public interface IBookService : IEntityServiceBase<Book>
    {
        Task<JsonPatchResult<Book>> UpdatePartialAsync(int id, JsonPatchDocument<Book> bookPatchDoc, ModelStateDictionary? modelState = null, bool saveChanges = true);
    }
}
