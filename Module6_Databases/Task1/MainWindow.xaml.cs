using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Task1       
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connString = "Data Source=employees.db";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() 
        {
            using (var conn = new SQLiteConnection(connString)) 
            {
                conn.Open();                                   
                string query = "SELECT * FROM Employees";        
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())   
                    {
                        var table = new DataTable();           
                        table.Load(reader);
                        EmployeesGrid.ItemsSource = table.DefaultView;
                    }
                }
            }
        }

        // При выборе строки переносим данные в поля ввода
        private void EmployeesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = EmployeesGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                txtName.Text = row["FullName"].ToString();
                txtPosition.Text = row["Position"].ToString();
                txtDept.Text = row["Department"].ToString();
                txtSalary.Text = row["Salary"].ToString();
            }
        }

        // Добавить
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "INSERT INTO Employees (FullName, Position, Department, Salary) " +
                               "VALUES (@n, @p, @d, @s)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text);
                    cmd.Parameters.AddWithValue("@p", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@d", txtDept.Text);
                    cmd.Parameters.AddWithValue("@s", txtSalary.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            txtName.Clear();
            txtPosition.Clear();
            txtDept.Clear();
            txtSalary.Clear();
        }

        // Изменить выбранного
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = EmployeesGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Employees SET FullName=@n, Position=@p, " +
                               "Department=@d, Salary=@s WHERE Id=@id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text);
                    cmd.Parameters.AddWithValue("@p", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@d", txtDept.Text);
                    cmd.Parameters.AddWithValue("@s", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            txtName.Clear();
            txtPosition.Clear();
            txtDept.Clear();
            txtSalary.Clear();
        }

        // Удалить выбранного
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = EmployeesGrid.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице!");
                return;
            }

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Employees WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }
    }
}
