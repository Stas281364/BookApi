using BookStore.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess
{
    public class BookStoreDbContext : DbContext
    {
        //Конструктор для регистрации DbContext в Depencity Injection
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) 
            : base(options) 
        {
            
        }

        //Образование коллекций для взаимодействия
        public DbSet<BookEntity> Books {  get; set; }
    }
}
