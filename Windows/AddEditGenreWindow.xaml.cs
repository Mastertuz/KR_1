using KR_1.Data;
using KR_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace KR_1.Windows
{
    public partial class AddEditGenreWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();
        private Genre? _genre;

        public AddEditGenreWindow()
        {
            InitializeComponent();
        }

        public AddEditGenreWindow(int genreId)
        {
            InitializeComponent();
            _genre = _context.Genres.Find(genreId);
            if (_genre != null)
            {
                NameBox.Text = _genre.Name;
                DescriptionBox.Text = _genre.Description;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Название жанра обязательно.");
                return;
            }

            if (_genre == null)
            {
                _genre = new Genre
                {
                    Name = NameBox.Text.Trim(),
                    Description = DescriptionBox.Text.Trim()
                };
                _context.Genres.Add(_genre);
            }
            else
            {
                _genre.Name = NameBox.Text.Trim();
                _genre.Description = DescriptionBox.Text.Trim();
                _context.Entry(_genre).State = EntityState.Modified;
            }

            _context.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}