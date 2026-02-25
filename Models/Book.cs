using System.Collections.Generic;
using System.Linq;

namespace KR_1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PublishYear { get; set; }
        public string ISBN { get; set; } = null!;
        public int QuantityInStock { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public string AuthorsDisplay => string.Join(", ", Authors.Select(a => a.FullName));
        public string GenresDisplay => string.Join(", ", Genres.Select(g => g.Name));
    }
}
