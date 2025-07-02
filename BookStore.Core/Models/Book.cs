namespace BookStore.Core.Models
{

    /// <summary>
    /// Класс с бизнес логикой
    /// </summary>
    public class Book
    {
        //макс длина заголовка
        public const int MAX_TITLE_LENGHT = 100;
        
        //Guid
        public Guid Id { get; }
       
        //Заголовок
        public string Title { get; } = string.Empty;
        
        //Описание
        public string Description { get; } = string.Empty;
        
        //Цена
        public decimal Price { get; }

        //Конструктор
        private Book(Guid id, string title, string description, decimal price)
        { 
            Id = id;
            Title = title;
            Description = description;
            Price = price;

        }


        public static (Book book, string Error) Create(Guid id, string title, string description, decimal price)
        { 
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > 100)
            {
                error = "title is null or more 100";
            }

            Book book = new Book(id, title, description, price);
            
            return (book, error);
        }
    }
}
