using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connString = "Data Source=library.db";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        // Загрузка книг (с поиском по названию или автору)
        private void LoadData(string search = "")
        {
            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "SELECT Id, Title, Author, Year, RenterName, " +
                               "CASE WHEN IsRented = 1 THEN 'Занята' ELSE 'Свободна' END AS Status " +
                               "FROM Books WHERE Title LIKE @q OR Author LIKE @q";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@q", "%" + search + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);
                        BooksGrid.ItemsSource = table.DefaultView;
                    }
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }

        // Арендовать выбранную книгу
        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = BooksGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите книгу в таблице!");
                return;
            }
            if (row["Status"].ToString() == "Занята")
            {
                MessageBox.Show("Эта книга уже арендована!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtRenter.Text))
            {
                MessageBox.Show("Введите имя читателя!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Books SET IsRented = 1, RenterName = @r WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@r", txtRenter.Text);
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData(txtSearch.Text);
            txtRenter.Clear();
        }

        // Вернуть выбранную книгу
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = BooksGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите книгу в таблице!");
                return;
            }
            if (row["Status"].ToString() == "Свободна")
            {
                MessageBox.Show("Эта книга и так свободна!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Books SET IsRented = 0, RenterName = '' WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData(txtSearch.Text);
        }
    }
}
