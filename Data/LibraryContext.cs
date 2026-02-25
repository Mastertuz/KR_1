using KR_1.Models;
using Microsoft.EntityFrameworkCore;

namespace KR_1.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(b => b.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(b => b.PublishYear).IsRequired();
                entity.Property(b => b.QuantityInStock).IsRequired();

                entity.HasIndex(b => b.ISBN).IsUnique();

                entity.HasMany(b => b.Authors)
                    .WithMany(a => a.Books)
                    .UsingEntity(j => j.ToTable("BookAuthors"));

                entity.HasMany(b => b.Genres)
                    .WithMany(g => g.Books)
                    .UsingEntity(j => j.ToTable("BookGenres"));
            });

            // Author
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.BirthDate).IsRequired();

                entity.Property(a => a.Country)
                    .HasMaxLength(100);
            });

            // Genre
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(g => g.Description)
                    .HasMaxLength(500);
            });
        }
    }
}
