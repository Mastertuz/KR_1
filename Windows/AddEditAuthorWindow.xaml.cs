using KR_1.Data;
using KR_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace KR_1.Windows
{
    public partial class AddEditAuthorWindow : Window
    {
        private readonly LibraryContext _context = new LibraryContext();
        private Author? _author;

        public AddEditAuthorWindow()
        {
            InitializeComponent();
        }

        public AddEditAuthorWindow(int authorId)
        {
            InitializeComponent();
            _author = _context.Authors.Find(authorId);
            if (_author != null)
            {
                FirstNameBox.Text = _author.FirstName;
                LastNameBox.Text = _author.LastName;
                BirthDatePicker.SelectedDate = _author.BirthDate;
                CountryBox.Text = _author.Country;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                BirthDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Имя, фамилия и дата рождения обязательны.");
                return;
            }

            if (_author == null)
            {
                _author = new Author
                {
                    FirstName = FirstNameBox.Text.Trim(),
                    LastName = LastNameBox.Text.Trim(),
                    BirthDate = BirthDatePicker.SelectedDate.Value,
                    Country = CountryBox.Text.Trim()
                };
                _context.Authors.Add(_author);
            }
            else
            {
                _author.FirstName = FirstNameBox.Text.Trim();
                _author.LastName = LastNameBox.Text.Trim();
                _author.BirthDate = BirthDatePicker.SelectedDate.Value;
                _author.Country = CountryBox.Text.Trim();
                _context.Entry(_author).State = EntityState.Modified;
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