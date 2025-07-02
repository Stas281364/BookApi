using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Core.Models;
using BookStore.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


//в Конфигурациях настраиваем наши класс для БД
namespace BookStore.DataAccess.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            //Настройка первичного ключа
            builder.HasKey(x => x.Id);

            //Настройка Title - имеет макс длину и обязятелен
            builder.Property(b => b.Title)
               .HasMaxLength(Book.MAX_TITLE_LENGHT)
               .IsRequired();

            //Настройка Description - обязятелен
            builder.Property(b => b.Description).IsRequired();

            //Настройка Price - обязятелен
            builder.Property(b => b.Price).IsRequired();
        }
    }
}
