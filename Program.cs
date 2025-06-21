using BookStore.Application.Services;
using BookStore.DataAccess;
using BookStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();            // Нужно для Swagger
builder.Services.AddSwaggerGen();                      // Добавляет генератор Swagger

builder.Services.AddDbContext<BookStoreDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BookStoreDbContext)));
    }
);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

var app = builder.Build();

// Включаем Swagger в dev-среде
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                                  // Генерация JSON-файла OpenAPI
    app.UseSwaggerUI();                                // Интерфейс Swagger UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api", () => "Hello im API!");

app.Run();
