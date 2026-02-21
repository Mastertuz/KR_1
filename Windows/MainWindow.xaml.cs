using KR_1.Data;
using KR_1.Models;
using KR_1.Windows;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KR_1.Windows
{
    public partial class MainWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DbInitializer.Initialize(_context);
            LoadAuthors();
            LoadGenres();
            LoadBooks();
        }

        private void LoadAuthors()
        {
            var authors = _context.Authors.ToList();
            authors.Insert(0, new Author { Id = 0, FirstName = "Все", LastName = "авторы" });
            AuthorFilterComboBox.ItemsSource = authors;
            AuthorFilterComboBox.SelectedIndex = 0;
        }

        private void LoadGenres()
        {
            var genres = _context.Genres.ToList();
            genres.Insert(0, new Genre { Id = 0, Name = "Все жанры" });
            GenreFilterComboBox.ItemsSource = genres;
            GenreFilterComboBox.SelectedIndex = 0;
        }

        private void LoadBooks()
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (AuthorFilterComboBox.SelectedValue is int authorId && authorId > 0)
                query = query.Where(b => b.AuthorId == authorId);

            if (GenreFilterComboBox.SelectedValue is int genreId && genreId > 0)
                query = query.Where(b => b.GenreId == genreId);

            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
                query = query.Where(b => b.Title.Contains(SearchTextBox.Text));

            var books = query.ToList();
            BooksDataGrid.ItemsSource = books;
            TotalQuantityLabel.Content = books.Sum(b => b.QuantityInStock).ToString();
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e) => LoadBooks();
        private void Search_Click(object sender, RoutedEventArgs e) => LoadBooks();

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            AuthorFilterComboBox.SelectedIndex = 0;
            GenreFilterComboBox.SelectedIndex = 0;
            SearchTextBox.Clear();
            LoadBooks();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditBookWindow();
            if (window.ShowDialog() == true)
                LoadBooks();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                var window = new AddEditBookWindow(selectedBook.Id);
                if (window.ShowDialog() == true)
                    LoadBooks();
            }
            else
                MessageBox.Show("Выберите книгу для редактирования.");
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                if (MessageBox.Show($"Удалить книгу '{selectedBook.Title}'?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Books.Remove(selectedBook);
                    _context.SaveChanges();
                    LoadBooks();
                }
            }
            else
                MessageBox.Show("Выберите книгу для удаления.");
        }

        private void ManageAuthors_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManageAuthorsWindow();
            window.ShowDialog();
            LoadAuthors();
            LoadBooks();
        }

        private void ManageGenres_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManageGenresWindow();
            window.ShowDialog();
            LoadGenres();
            LoadBooks();
        }
    }
}