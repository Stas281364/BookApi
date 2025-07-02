using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Core.Models;
using BookStore.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

//Репозиторий (или Repository Pattern) — это абстракция над доступом к данным, которая отделяет бизнес-логику
//от логики работы с базой данных.

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {

        private readonly BookStoreDbContext _Context;

        //конструктор
        public BooksRepository(BookStoreDbContext context)
        {
            _Context = context;
        }

        //Получение книг
        public async Task<List<Book>> Get()
        {
            var bookEntities = await _Context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities.Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).book).ToList();

            return books;
        }

        //Создание книги
        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity { Id = book.Id, Title = book.Title, Description = book.Description, Price = book.Price };
            await _Context.Books.AddAsync(bookEntity);
            await _Context.SaveChangesAsync();

            return bookEntity.Id;
        }

        //Обновление книги 
        public async Task<Guid> Update(Guid id, string tittle, string description, decimal price)
        {
            await _Context.Books.Where(b => b.Id == id).ExecuteUpdateAsync(s => s
            .SetProperty(b => b.Title, tittle)
            .SetProperty(b => b.Description, description)
            .SetProperty(b => b.Price, price)
            );

            return id;
        }

        //Удаление книги
        public async Task<Guid> Delete(Guid id)
        {
            await _Context.Books.Where(b => b.Id == id).ExecuteDeleteAsync();

            return id;
        }

        public async Task DeleteAll()
        { 
            await _Context.Books.ExecuteDeleteAsync();
        }

    }
}
