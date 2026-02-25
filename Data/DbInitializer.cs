using KR_1.Models;
using System;
using System.Linq;

namespace KR_1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            // Создаём БД, если её нет
            context.Database.EnsureCreated();

            if (context.Books.Any())
                return;

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
                new Book { Title = "Война и мир", PublishYear = 1869, ISBN = "9785170826504", QuantityInStock = 5,
                    Authors = new[] { authors[0] }, Genres = new[] { genres[0] } },
                new Book { Title = "Анна Каренина", PublishYear = 1877, ISBN = "9785040887602", QuantityInStock = 3,
                    Authors = new[] { authors[0] }, Genres = new[] { genres[0] } },
                new Book { Title = "Преступление и наказание", PublishYear = 1866, ISBN = "9785170989612", QuantityInStock = 7,
                    Authors = new[] { authors[1] }, Genres = new[] { genres[0] } },
                new Book { Title = "Идиот", PublishYear = 1869, ISBN = "9785171131142", QuantityInStock = 2,
                    Authors = new[] { authors[1] }, Genres = new[] { genres[0] } },
                new Book { Title = "Братья Карамазовы", PublishYear = 1880, ISBN = "9785041015329", QuantityInStock = 4,
                    Authors = new[] { authors[1] }, Genres = new[] { genres[0] } },
                new Book { Title = "Вишнёвый сад", PublishYear = 1904, ISBN = "9785171193195", QuantityInStock = 6,
                    Authors = new[] { authors[2] }, Genres = new[] { genres[2] } },
                new Book { Title = "Чайка", PublishYear = 1896, ISBN = "9785171193126", QuantityInStock = 3,
                    Authors = new[] { authors[2] }, Genres = new[] { genres[2] } },
                new Book { Title = "Евгений Онегин", PublishYear = 1833, ISBN = "9785170989629", QuantityInStock = 8,
                    Authors = new[] { authors[3] }, Genres = new[] { genres[1] } },
                new Book { Title = "Капитанская дочка", PublishYear = 1836, ISBN = "9785171082994", QuantityInStock = 5,
                    Authors = new[] { authors[3] }, Genres = new[] { genres[5] } },
                new Book { Title = "Мёртвые души", PublishYear = 1842, ISBN = "9785171175443", QuantityInStock = 4,
                    Authors = new[] { authors[4] }, Genres = new[] { genres[0] } },
                new Book { Title = "Ревизор", PublishYear = 1836, ISBN = "9785171175436", QuantityInStock = 7,
                    Authors = new[] { authors[4] }, Genres = new[] { genres[2] } },
                new Book { Title = "Отцы и дети", PublishYear = 1862, ISBN = "9785171085322", QuantityInStock = 5,
                    Authors = new[] { authors[5] }, Genres = new[] { genres[0] } },
                new Book { Title = "Мастер и Маргарита", PublishYear = 1967, ISBN = "9785171047399", QuantityInStock = 9,
                    Authors = new[] { authors[6] }, Genres = new[] { genres[3] } },
                new Book { Title = "Собачье сердце", PublishYear = 1925, ISBN = "9785171047382", QuantityInStock = 6,
                    Authors = new[] { authors[6] }, Genres = new[] { genres[3] } },
                new Book { Title = "Записки охотника", PublishYear = 1852, ISBN = "9785171085339", QuantityInStock = 4,
                    Authors = new[] { authors[5] }, Genres = new[] { genres[5] } }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
