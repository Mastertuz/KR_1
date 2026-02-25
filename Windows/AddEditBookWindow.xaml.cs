using KR_1.Data;
using KR_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KR_1.Windows
{
    public partial class AddEditBookWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();
        private Book? _book;
        private List<Author> _authors = new();
        private List<Genre> _genres = new();

        public AddEditBookWindow()
        {
            InitializeComponent();
            LoadAuthorsAndGenres();
        }

        public AddEditBookWindow(int bookId)
        {
            InitializeComponent();
            LoadAuthorsAndGenres();
            _book = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == bookId);

            if (_book != null)
            {
                TitleTextBox.Text = _book.Title;
                YearTextBox.Text = _book.PublishYear.ToString();
                IsbnTextBox.Text = _book.ISBN;
                QuantityTextBox.Text = _book.QuantityInStock.ToString();

                foreach (var author in _authors.Where(a => _book.Authors.Any(ba => ba.Id == a.Id)))
                    AuthorsListBox.SelectedItems.Add(author);

                foreach (var genre in _genres.Where(g => _book.Genres.Any(bg => bg.Id == g.Id)))
                    GenresListBox.SelectedItems.Add(genre);
            }
        }

        private void LoadAuthorsAndGenres()
        {
            _authors = _context.Authors.ToList();
            AuthorsListBox.ItemsSource = _authors;

            _genres = _context.Genres.ToList();
            GenresListBox.ItemsSource = _genres;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var isbn = IsbnTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                !int.TryParse(YearTextBox.Text, out int year) ||
                string.IsNullOrWhiteSpace(isbn) ||
                !isbn.All(char.IsDigit) ||
                !int.TryParse(QuantityTextBox.Text, out int quantity) ||
                AuthorsListBox.SelectedItems.Count == 0 ||
                GenresListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Заполните все поля корректно.");
                return;
            }

            var selectedAuthors = AuthorsListBox.SelectedItems.Cast<Author>().ToList();
            var selectedGenres = GenresListBox.SelectedItems.Cast<Genre>().ToList();

            if (_book == null)
            {
                var book = new Book
                {
                    Title = TitleTextBox.Text.Trim(),
                    PublishYear = year,
                    ISBN = isbn,
                    QuantityInStock = quantity,
                    Authors = selectedAuthors,
                    Genres = selectedGenres
                };
                _context.Books.Add(book);
            }
            else
            {
                _book.Title = TitleTextBox.Text.Trim();
                _book.PublishYear = year;
                _book.ISBN = isbn;
                _book.QuantityInStock = quantity;

                _book.Authors.Clear();
                foreach (var author in selectedAuthors)
                    _book.Authors.Add(author);

                _book.Genres.Clear();
                foreach (var genre in selectedGenres)
                    _book.Genres.Add(genre);
            }

            _context.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void IsbnTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void IsbnTextBox_OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(typeof(string)))
            {
                e.CancelCommand();
                return;
            }

            var text = e.DataObject.GetData(typeof(string)) as string ?? string.Empty;
            if (!text.All(char.IsDigit))
                e.CancelCommand();
        }
    }
}
