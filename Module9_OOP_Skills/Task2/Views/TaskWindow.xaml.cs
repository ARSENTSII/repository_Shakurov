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
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private int _id;

        public TaskItem Result { get; private set; }

        public TaskWindow(TaskItem task, List<Project> projects, List<Employee> employees)
        {
            InitializeComponent();

            cbProject.ItemsSource = projects;

            // первый вариант "не назначен" (null)
            var employeeList = new List<Employee>();
            employeeList.Add(new Employee { Id = 0, FullName = "Не назначен" });
            employeeList.AddRange(employees);
            cbEmployee.ItemsSource = employeeList;

            cbStatus.Items.Add("Новая");
            cbStatus.Items.Add("В работе");
            cbStatus.Items.Add("Выполнена");

            cbPriority.Items.Add("Низкий");
            cbPriority.Items.Add("Средний");
            cbPriority.Items.Add("Высокий");

            if (task != null)
            {
                Title = "Редактирование задачи";
                _id = task.Id;
                txtTitle.Text = task.Title;
                txtDescription.Text = task.Description;
                cbProject.SelectedItem = projects.Find(p => p.Id == task.ProjectId);
                cbEmployee.SelectedItem = task.EmployeeId.HasValue
                    ? employeeList.Find(x => x.Id == task.EmployeeId.Value)
                    : employeeList[0];
                cbStatus.SelectedItem = EnumConverter.TaskStateToDb(task.Status);
                cbPriority.SelectedItem = EnumConverter.TaskPriorityToDb(task.Priority);
                sliderProgress.Value = task.Progress;
                dpDeadline.SelectedDate = task.Deadline;
            }
            else
            {
                Title = "Новая задача";
                cbEmployee.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                cbPriority.SelectedIndex = 1;
                sliderProgress.Value = 0;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название задачи");
                return;
            }

            var project = cbProject.SelectedItem as Project;
            if (project == null)
            {
                MessageBox.Show("Выберите проект");
                return;
            }

            var employee = cbEmployee.SelectedItem as Employee;

            var status = EnumConverter.TaskStateFromDb(cbStatus.SelectedItem.ToString());
            int progress = (int)sliderProgress.Value;

            // связь статуса и прогресса
            if (progress == 100)
            {
                status = TaskState.Done;
            }
            else if (status == TaskState.Done)
            {
                progress = 100;
            }

            Result = new TaskItem
            {
                Id = _id,
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                ProjectId = project.Id,
                EmployeeId = (employee == null || employee.Id == 0) ? (int?)null : employee.Id,
                Status = status,
                Priority = EnumConverter.TaskPriorityFromDb(cbPriority.SelectedItem.ToString()),
                Progress = progress,
                Deadline = dpDeadline.SelectedDate
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
