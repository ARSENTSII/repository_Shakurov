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

namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connString = "Data Source=tasks.db";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        // Загрузка всех задач в таблицу
        private void LoadData()
        {
            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM Tasks", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    TasksGrid.ItemsSource = table.DefaultView;
                }
            }
        }

        // При выборе строки переносим данные в поля
        private void TasksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = TasksGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                txtTitle.Text = row["Title"].ToString();
                txtDescription.Text = row["Description"].ToString();
                cmbStatus.Text = row["Status"].ToString();
            }
        }

        // Добавить
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Tasks (Title, Description, Status) VALUES (@t, @d, @s)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@d", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@s", cmbStatus.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            txtTitle.Clear();
            txtDescription.Clear();
        }

        // Изменить выбранную
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = TasksGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите задачу в таблице!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Tasks SET Title=@t, Description=@d, Status=@s WHERE Id=@id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@d", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@s", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            txtTitle.Clear();
            txtDescription.Clear();
        }

        // Удалить выбранную
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = TasksGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите задачу в таблице!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Tasks WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }
    }
}
