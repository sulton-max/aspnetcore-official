using Application.DAL.Repositories;
using Domain.Models.Entity;
using Domain.Models.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        public IBookStoreRepository BookRepository { get; }

        public BookService(IBookStoreRepository booksRepository)
        {
            BookRepository = booksRepository;
        }

        private Task InvokeLongRunningOperation()
        {
            return Task.Run(() => Thread.Sleep(1000));
        }

        public async IAsyncEnumerable<Book> GetAsync(int pageSize, int pageToken)
        {
            var query = BookRepository.Get(x => true).Skip(pageSize * (pageToken - 1)).Take(pageSize);
            foreach (var book in query)
            {
                await InvokeLongRunningOperation();
                yield return book;
            }
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await Task.Run(() => BookRepository.Get(x => x.Id == id).FirstOrDefault());
        }

        public async Task<bool> CreateAsync(Book book, bool saveChanges = true)
        {
            return await Task.Run(() =>
            {
                BookRepository.Create(book);
                return saveChanges
                    ? BookRepository.SaveChanges()
                    : true;
            });
        }

        public async Task<bool> UpdateAsync(int id, Book book, bool saveChanges = true)
        {
            return await Task.Run(async () =>
            {
                var foundBook = await GetByIdAsync(id) ?? throw new InvalidOperationException();

                foundBook.Name = book.Name;

                BookRepository.Update(foundBook);
                return saveChanges
                    ? BookRepository.SaveChanges()
                    : true;
            });
        }

        public async Task<JsonPatchResult<Book>> UpdatePartialAsync
        (
            int id,
            JsonPatchDocument<Book> bookPatchDoc,
            ModelStateDictionary? modelState = null,
            bool saveChanges = true
        )
        {
            return await Task.Run(async () =>
            {
                var book = await GetByIdAsync(id) ?? throw new InvalidOperationException();
                var foundBook = book.Clone();
                BookRepository.Detach(foundBook);

                if (modelState is not null)
                    bookPatchDoc.ApplyTo(book);
                else
                    bookPatchDoc.ApplyTo(book, modelState!);

                BookRepository.Update(book);
                if (saveChanges)
                    if (!BookRepository.SaveChanges())
                        throw new InvalidOperationException();

                return new JsonPatchResult<Book>(foundBook, book);
            });
        }

        public async Task<bool> DeleteAsync(int id, bool saveChanges = true)
        {
            return await Task.Run(async () =>
            {
                var book = await GetByIdAsync(id) ?? throw new InvalidOperationException();
                BookRepository.Delete(book);
                return saveChanges
                    ? BookRepository.SaveChanges()
                    : true;
            });
        }
    }
}
