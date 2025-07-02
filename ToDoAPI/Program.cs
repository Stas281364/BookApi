using BookStore.Application.Services;
using BookStore.DataAccess;
using BookStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ����������� ��������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();            // ����� ��� Swagger
builder.Services.AddSwaggerGen();                      // ��������� ��������� Swagger

builder.Services.AddDbContext<BookStoreDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BookStoreDbContext)));
    }
);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

var app = builder.Build();

// �������� Swagger � dev-�����
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                                  // ��������� JSON-����� OpenAPI
    app.UseSwaggerUI();                                // ��������� Swagger UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api", () => "Hello im API!");

app.Run();
