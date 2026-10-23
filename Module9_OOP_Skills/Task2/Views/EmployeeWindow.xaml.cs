using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Task2.Models;

namespace Task2.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {

        private int _id;

        public Employee Result { get; private set; } 

        public EmployeeWindow(Employee employee)
        {
            InitializeComponent();

            if (employee != null)
            {
                Title = "Редактирование сотрудника";
                _id = employee.Id;
                txtFullName.Text = employee.FullName;
                txtPosition.Text = employee.Position;
            }
            else
            {
                Title = "Новый сотрудник";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО");
                return;
            }

            Result = new Employee
            {
                Id = _id,
                FullName = txtFullName.Text.Trim(),
                Position = txtPosition.Text.Trim()
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
