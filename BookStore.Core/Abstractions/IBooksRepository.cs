﻿using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<Guid> Create(Book book);
        //Task<Guid> Delete(Guid id, string tittle, string description, decimal price);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> Get();
        Task<Guid> Update(Guid id, string tittle, string description, decimal price);
        Task DeleteAll();
    }
}