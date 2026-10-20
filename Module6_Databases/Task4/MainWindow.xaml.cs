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

namespace Task4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connString = "Data Source=finance.db";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        // Загрузка записей и пересчет баланса
        private void LoadData()
        {
            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM Finances", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    FinanceGrid.ItemsSource = table.DefaultView;
                }

                string sum = "SELECT IFNULL(SUM(CASE WHEN Type='Доход' THEN Amount ELSE -Amount END), 0) FROM Finances";
                using (var cmd = new SQLiteCommand(sum, conn))
                {
                    double balance = Convert.ToDouble(cmd.ExecuteScalar());
                    txtBalance.Text = "Баланс: " + balance;
                }
            }
        }

        // При выборе строки переносим данные в поля
        private void FinanceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = FinanceGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                txtDescription.Text = row["Description"].ToString();
                cmbType.Text = row["Type"].ToString();
                txtAmount.Text = row["Amount"].ToString();
            }
        }

        // Проверка полей
        private bool CheckInput(out double amount)
        {
            amount = 0;
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Введите описание!");
                return false;
            }
            if (!double.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Сумма должна быть положительным числом!");
                return false;
            }
            return true;
        }

        // Добавить
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            if (!CheckInput(out amount)) return;

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Finances (Description, Type, Amount) VALUES (@d, @t, @a)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@d", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@t", cmbType.Text);
                    cmd.Parameters.AddWithValue("@a", amount);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            txtDescription.Clear();
            txtAmount.Clear();
        }

        // Изменить выбранную запись
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = FinanceGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите запись в таблице!");
                return;
            }
            double amount;
            if (!CheckInput(out amount)) return;

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Finances SET Description=@d, Type=@t, Amount=@a WHERE Id=@id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@d", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@t", cmbType.Text);
                    cmd.Parameters.AddWithValue("@a", amount);
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            txtDescription.Clear();
            txtAmount.Clear();
        }

        // Удалить выбранную запись
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = FinanceGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите запись в таблице!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Finances WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }
    }
}
