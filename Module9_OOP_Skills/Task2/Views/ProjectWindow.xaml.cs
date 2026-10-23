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
using Task2.Services;

namespace Task2.Views
{
    /// <summary>
    /// Логика взаимодействия для ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private int _id;

        public Project Result { get; private set; }
        public ProjectWindow(Project project)
        {
            InitializeComponent();

            cbStatus.Items.Add("Активный");
            cbStatus.Items.Add("Завершён");
            cbStatus.Items.Add("Приостановлен");

            if (project != null)
            {
                Title = "Редактирование проекта";
                _id = project.Id;
                txtName.Text = project.Name;
                txtDescription.Text = project.Description;
                dpStart.SelectedDate = project.StartDate;
                dpEnd.SelectedDate = project.EndDate;
                cbStatus.SelectedItem = EnumConverter.ProjectStatusToDb(project.Status);
            }
            else
            {
                Title = "Новый проект";
                dpStart.SelectedDate = DateTime.Today;
                dpEnd.SelectedDate = DateTime.Today.AddDays(30);
                cbStatus.SelectedIndex = 0;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название проекта");
                return;
            }

            if (dpStart.SelectedDate.HasValue == false || dpEnd.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Выберите даты начала и окончания");
                return;
            }

            if (dpEnd.SelectedDate.Value < dpStart.SelectedDate.Value)
            {
                MessageBox.Show("Дата окончания не может быть раньше даты начала");
                return;
            }

            if (cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус");
                return;
            }

            Result = new Project
            {
                Id = _id,
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                StartDate = dpStart.SelectedDate.Value,
                EndDate = dpEnd.SelectedDate.Value,
                Status = EnumConverter.ProjectStatusFromDb(cbStatus.SelectedItem.ToString())
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
