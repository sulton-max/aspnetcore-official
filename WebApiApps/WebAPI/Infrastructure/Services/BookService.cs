using Application.DAL.Repositories;
using Application.Services;
using Domain.Models.Entity;
using Domain.Models.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;

namespace Infrastructure.Services;

public class BookService : EntityServiceBase<Book, IRepositoryBase<Book>>, IBookService
{
    public BookService(IRepositoryBase<Book> entityRepository) : base(entityRepository)
    {
    }


    private Task InvokeLongRunningOperation()
    {
        return Task.Run(() => Thread.Sleep(1000));
    }

    public override Task<IEnumerable<Book>> GetAsync(int pageSize, int pageToken)
    {
        return Task.Run(() => EntityRepository.Get(x => true).Skip(pageSize * (pageToken - 1)).Take(pageSize).ToList().AsEnumerable());

        //var query = EntityRepository.Get(x => true).Skip(pageSize * (pageToken - 1)).Take(pageSize);
        //foreach (var book in query)
        //{
        //    await InvokeLongRunningOperation();
        //    yield return book;
        //}
    }

    public override async Task<Book?> GetByIdAsync(int id)
    {
        return await Task.Run(() => EntityRepository.Get(x => x.Id == id).FirstOrDefault());
    }

    public override async Task<Book> CreateAsync(Book book, bool saveChanges = true)
    {
        return await Task.Run(() =>
        {
            EntityRepository.Create(book);
            if (saveChanges)
                EntityRepository.SaveChanges();

            return book;
        });
    }

    public override async Task<bool> UpdateAsync(int id, Book book, bool saveChanges = true)
    {
        return await Task.Run(async () =>
        {
            var foundBook = await GetByIdAsync(id) ?? throw new InvalidOperationException();

            foundBook.Name = book.Name;

            EntityRepository.Update(foundBook);
            return saveChanges
                ? EntityRepository.SaveChanges()
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
            EntityRepository.Detach(foundBook);

            if (modelState is not null)
                bookPatchDoc.ApplyTo(book);
            else
                bookPatchDoc.ApplyTo(book, modelState!);

            EntityRepository.Update(book);
            if (saveChanges)
                if (!EntityRepository.SaveChanges())
                    throw new InvalidOperationException();

            return new JsonPatchResult<Book>(foundBook, book);
        });
    }

    public override async Task<bool> DeleteByIdAsync(int id, bool saveChanges = true)
    {
        return await Task.Run(async () =>
        {
            var book = await GetByIdAsync(id) ?? throw new InvalidOperationException();
            EntityRepository.Delete(book);
            return saveChanges
                ? EntityRepository.SaveChanges()
                : true;
        });
    }
}
