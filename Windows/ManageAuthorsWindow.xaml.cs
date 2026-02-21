using KR_1.Data;
using KR_1.Models;
using System.Linq;
using System.Windows;

namespace KR_1.Windows
{
    public partial class ManageAuthorsWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();

        public ManageAuthorsWindow()
        {
            InitializeComponent();
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            AuthorsGrid.ItemsSource = _context.Authors.ToList();
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditAuthorWindow();
            if (window.ShowDialog() == true)
                LoadAuthors();
        }

        private void EditAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsGrid.SelectedItem is Author selected)
            {
                var window = new AddEditAuthorWindow(selected.Id);
                if (window.ShowDialog() == true)
                    LoadAuthors();
            }
            else
                MessageBox.Show("Выберите автора.");
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsGrid.SelectedItem is Author selected)
            {
                if (MessageBox.Show($"Удалить автора {selected.LastName} {selected.FirstName}?",
                    "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Authors.Remove(selected);
                    _context.SaveChanges();
                    LoadAuthors();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}