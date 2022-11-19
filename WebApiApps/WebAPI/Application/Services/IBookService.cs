using Application.DAL.Repositories;
using Domain.Models.Entity;
using Domain.Models.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Services
{
    /// <summary>
    /// Provides methods to execute CRUD operatiosn on a Book resource
    /// </summary>
    public interface IBookService
    {
        IBookStoreRepository BookRepository { get; }

        /// <summary>
        /// Gets all books within page size for specified page token
        /// </summary>
        /// <param name="pageSize">Size of the page for pagination</param>
        /// <param name="pageToken">Current page token to paginate</param>
        /// <returns>Array of books</returns>
        IAsyncEnumerable<Book> GetAsync(int pageSize, int pageToken);

        /// <summary>
        /// Gets a specific Book resource
        /// </summary>
        /// <param name="id">Id of the book resource</param>
        /// <returns>A Book if found, else null</returns>
        Task<Book?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a Book resource
        /// </summary>
        /// <param name="book">Book resource to create</param>
        /// <param name="saveChanges">Defines wheter method should immediately send updates</param>
        /// <returns>Created Book resource with updated properties</returns>
        Task<bool> CreateAsync(Book book, bool saveChanges = true);

        /// <summary>
        /// Updates a Book resource
        /// </summary>
        /// <param name="id">Id of the Book resource being updated</param>
        /// <param name="book">Book resource with new property values</param>
        /// <param name="saveChanges">Defines wheter method should immediately send updates</param>
        /// <returns>True if update succeeded, else false</returns>
        Task<bool> UpdateAsync(int id, Book book, bool saveChanges = true);

        /// <summary>
        /// Updates a Book resource partially
        /// </summary>
        /// <param name="id">Id of the Book resource being updated</param>
        /// <param name="bookPatchDoc">JSON patch document to partially update resource</param>
        /// <param name="modelState">Model state dictionary to write JSON patch errors</param>
        /// <param name="saveChanges">Defines wheter method should immediately send updates</param>
        /// <returns>True if update succeeded, else false</returns>
        Task<JsonPatchResult<Book>> UpdatePartialAsync
        (
            int id,
            JsonPatchDocument<Book> bookPatchDoc,
            ModelStateDictionary? modelState = null,
            bool saveChanges = true
        );

        /// <summary>
        /// Deletes a specific Book resource
        /// </summary>
        /// <param name="id">Id of the Book resource being deleted</param>
        /// <param name="saveChanges">Defines wheter method should immediately send updates</param>
        /// <returns>True if delete succeeded, else false</returns>
        Task<bool> DeleteAsync(int id, bool saveChanges = true);
    }
}
