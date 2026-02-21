using KR_1.Models;
using System;
using System.Linq;

namespace KR_1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            // Удаляем старую БД и создаём новую
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Авторы
            var authors = new[]
            {
                new Author { FirstName = "Лев", LastName = "Толстой", BirthDate = new DateTime(1828,9,9), Country = "Россия" },
                new Author { FirstName = "Фёдор", LastName = "Достоевский", BirthDate = new DateTime(1821,11,11), Country = "Россия" },
                new Author { FirstName = "Антон", LastName = "Чехов", BirthDate = new DateTime(1860,1,29), Country = "Россия" },
                new Author { FirstName = "Александр", LastName = "Пушкин", BirthDate = new DateTime(1799,6,6), Country = "Россия" },
                new Author { FirstName = "Николай", LastName = "Гоголь", BirthDate = new DateTime(1809,4,1), Country = "Россия" },
                new Author { FirstName = "Иван", LastName = "Тургенев", BirthDate = new DateTime(1818,11,9), Country = "Россия" },
                new Author { FirstName = "Михаил", LastName = "Булгаков", BirthDate = new DateTime(1891,5,15), Country = "Россия" }
            };
            context.Authors.AddRange(authors);
            context.SaveChanges();

            // Жанры
            var genres = new[]
            {
                new Genre { Name = "Роман", Description = "Художественный роман" },
                new Genre { Name = "Поэзия", Description = "Стихотворные произведения" },
                new Genre { Name = "Драма", Description = "Пьесы" },
                new Genre { Name = "Фантастика", Description = "Научная фантастика и магический реализм" },
                new Genre { Name = "Детектив", Description = "Криминальные истории" },
                new Genre { Name = "Повесть", Description = "Средняя проза" }
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            // 15 реальных книг
            var books = new Book[]
            {
                new Book { Title = "Война и мир", PublishYear = 1869, ISBN = "978-5-17-082650-4", QuantityInStock = 5,
                    AuthorId = authors[0].Id, GenreId = genres[0].Id },
                new Book { Title = "Анна Каренина", PublishYear = 1877, ISBN = "978-5-04-088760-2", QuantityInStock = 3,
                    AuthorId = authors[0].Id, GenreId = genres[0].Id },
                new Book { Title = "Преступление и наказание", PublishYear = 1866, ISBN = "978-5-17-098961-2", QuantityInStock = 7,
                    AuthorId = authors[1].Id, GenreId = genres[0].Id },
                new Book { Title = "Идиот", PublishYear = 1869, ISBN = "978-5-17-113114-2", QuantityInStock = 2,
                    AuthorId = authors[1].Id, GenreId = genres[0].Id },
                new Book { Title = "Братья Карамазовы", PublishYear = 1880, ISBN = "978-5-04-101532-9", QuantityInStock = 4,
                    AuthorId = authors[1].Id, GenreId = genres[0].Id },
                new Book { Title = "Вишнёвый сад", PublishYear = 1904, ISBN = "978-5-17-119319-5", QuantityInStock = 6,
                    AuthorId = authors[2].Id, GenreId = genres[2].Id },
                new Book { Title = "Чайка", PublishYear = 1896, ISBN = "978-5-17-119312-6", QuantityInStock = 3,
                    AuthorId = authors[2].Id, GenreId = genres[2].Id },
                new Book { Title = "Евгений Онегин", PublishYear = 1833, ISBN = "978-5-17-098962-9", QuantityInStock = 8,
                    AuthorId = authors[3].Id, GenreId = genres[1].Id },
                new Book { Title = "Капитанская дочка", PublishYear = 1836, ISBN = "978-5-17-108299-4", QuantityInStock = 5,
                    AuthorId = authors[3].Id, GenreId = genres[5].Id },
                new Book { Title = "Мёртвые души", PublishYear = 1842, ISBN = "978-5-17-117544-3", QuantityInStock = 4,
                    AuthorId = authors[4].Id, GenreId = genres[0].Id },
                new Book { Title = "Ревизор", PublishYear = 1836, ISBN = "978-5-17-117543-6", QuantityInStock = 7,
                    AuthorId = authors[4].Id, GenreId = genres[2].Id },
                new Book { Title = "Отцы и дети", PublishYear = 1862, ISBN = "978-5-17-108532-2", QuantityInStock = 5,
                    AuthorId = authors[5].Id, GenreId = genres[0].Id },
                new Book { Title = "Мастер и Маргарита", PublishYear = 1967, ISBN = "978-5-17-104739-9", QuantityInStock = 9,
                    AuthorId = authors[6].Id, GenreId = genres[3].Id },
                new Book { Title = "Собачье сердце", PublishYear = 1925, ISBN = "978-5-17-104738-2", QuantityInStock = 6,
                    AuthorId = authors[6].Id, GenreId = genres[3].Id },
                new Book { Title = "Записки охотника", PublishYear = 1852, ISBN = "978-5-17-108533-9", QuantityInStock = 4,
                    AuthorId = authors[5].Id, GenreId = genres[5].Id }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}