using KR_1.Data;
using KR_1.Models;
using System.Linq;
using System.Windows;

namespace KR_1.Windows
{
    public partial class ManageGenresWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();

        public ManageGenresWindow()
        {
            InitializeComponent();
            LoadGenres();
        }

        private void LoadGenres()
        {
            GenresGrid.ItemsSource = _context.Genres.ToList();
        }

        private void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditGenreWindow();
            if (window.ShowDialog() == true)
                LoadGenres();
        }

        private void EditGenre_Click(object sender, RoutedEventArgs e)
        {
            if (GenresGrid.SelectedItem is Genre selected)
            {
                var window = new AddEditGenreWindow(selected.Id);
                if (window.ShowDialog() == true)
                    LoadGenres();
            }
            else
                MessageBox.Show("Выберите жанр.");
        }

        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            if (GenresGrid.SelectedItem is Genre selected)
            {
                if (MessageBox.Show($"Удалить жанр '{selected.Name}'?",
                    "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Genres.Remove(selected);
                    _context.SaveChanges();
                    LoadGenres();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}