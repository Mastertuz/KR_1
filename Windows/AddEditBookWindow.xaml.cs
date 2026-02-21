using KR_1.Data;
using KR_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace KR_1.Windows
{
    public partial class AddEditBookWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();
        private Book? _book;

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
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.Id == bookId);

            if (_book != null)
            {
                TitleTextBox.Text = _book.Title;
                YearTextBox.Text = _book.PublishYear.ToString();
                IsbnTextBox.Text = _book.ISBN;
                QuantityTextBox.Text = _book.QuantityInStock.ToString();
                AuthorComboBox.SelectedValue = _book.AuthorId;
                GenreComboBox.SelectedValue = _book.GenreId;
            }
        }

        private void LoadAuthorsAndGenres()
        {
            var authors = _context.Authors
                .Select(a => new { a.Id, FullName = a.LastName + " " + a.FirstName })
                .ToList();
            AuthorComboBox.ItemsSource = authors;

            var genres = _context.Genres.ToList();
            GenreComboBox.ItemsSource = genres;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                !int.TryParse(YearTextBox.Text, out int year) ||
                string.IsNullOrWhiteSpace(IsbnTextBox.Text) ||
                !int.TryParse(QuantityTextBox.Text, out int quantity) ||
                AuthorComboBox.SelectedValue == null ||
                GenreComboBox.SelectedValue == null)
            {
                MessageBox.Show("Заполните все поля корректно.");
                return;
            }

            if (_book == null)
            {
                var book = new Book
                {
                    Title = TitleTextBox.Text.Trim(),
                    PublishYear = year,
                    ISBN = IsbnTextBox.Text.Trim(),
                    QuantityInStock = quantity,
                    AuthorId = (int)AuthorComboBox.SelectedValue,
                    GenreId = (int)GenreComboBox.SelectedValue
                };
                _context.Books.Add(book);
            }
            else
            {
                _book.Title = TitleTextBox.Text.Trim();
                _book.PublishYear = year;
                _book.ISBN = IsbnTextBox.Text.Trim();
                _book.QuantityInStock = quantity;
                _book.AuthorId = (int)AuthorComboBox.SelectedValue;
                _book.GenreId = (int)GenreComboBox.SelectedValue;
                _context.Entry(_book).State = EntityState.Modified;
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
    }
}