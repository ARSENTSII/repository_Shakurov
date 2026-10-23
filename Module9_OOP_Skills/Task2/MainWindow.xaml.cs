using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using Task2.Data;
using Task2.Models;
using Task2.Repositories;
using Task2.Services;
using Task2.Views; 

namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeRepository _employeeRepo = new EmployeeRepository();
        private ProjectRepository _projectRepo = new ProjectRepository();

        private TaskRepository _taskRepo = new TaskRepository();

        private NotificationService _notifier = new NotificationService();
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            LoadEmployees();
            LoadProjects();
            LoadTasks();
        }

        private void LoadEmployees()
        {
            try
            {
                dgEmployees.ItemsSource = _employeeRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void LoadProjects()
        {
            try
            {
                dgProjects.ItemsSource = _projectRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void LoadTasks()
        {
            try
            {
                var tasks = _taskRepo.GetAll();
                var projects = _projectRepo.GetAll();
                var employees = _employeeRepo.GetAll();

                foreach (var t in tasks)
                {
                    var p = projects.Find(x => x.Id == t.ProjectId);
                    t.ProjectName = p != null ? p.Name : "";

                    if (t.EmployeeId.HasValue)
                    {
                        var emp = employees.Find(x => x.Id == t.EmployeeId.Value);
                        t.EmployeeName = emp != null ? emp.FullName : "";
                    }
                    else
                    {
                        t.EmployeeName = "Не назначен";
                    }
                }

                dgTasks.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var window = new EmployeeWindow(null);
            if (window.ShowDialog() == true)
            {
                try
                {
                    _employeeRepo.Add(window.Result);
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message);
                }
            }
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProjectWindow(null);
            if (window.ShowDialog() == true)
            {
                try
                {
                    _projectRepo.Add(window.Result);
                    LoadProjects();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message);
                }
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new TaskWindow(null, _projectRepo.GetAll(), _employeeRepo.GetAll());
                if (window.ShowDialog() == true)
                {
                    _taskRepo.Add(window.Result);
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgEmployees.SelectedItem as Employee;
            if (selected == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            var window = new EmployeeWindow(selected);
            if (window.ShowDialog() == true)
            {
                try
                {
                    _employeeRepo.Update(window.Result);
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка изменения: " + ex.Message);
                }
            }
        }

        private void EditProject_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgProjects.SelectedItem as Project;
            if (selected == null)
            {
                MessageBox.Show("Выберите проект");
                return;
            }

            var window = new ProjectWindow(selected);
            if (window.ShowDialog() == true)
            {
                try
                {
                    _projectRepo.Update(window.Result);
                    LoadProjects();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка изменения: " + ex.Message);
                }
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgTasks.SelectedItem as TaskItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите задачу");
                return;
            }

            try
            {
                var window = new TaskWindow(selected, _projectRepo.GetAll(), _employeeRepo.GetAll());
                if (window.ShowDialog() == true)
                {
                    _taskRepo.Update(window.Result);
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения: " + ex.Message);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgEmployees.SelectedItem as Employee;
            if (selected == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            var answer = MessageBox.Show(
                "Удалить сотрудника " + selected.FullName + "?\nЕго задачи станут неназначенными.",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {
                try
                {
                    _employeeRepo.Delete(selected.Id);
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        private void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgProjects.SelectedItem as Project;
            if (selected == null)
            {
                MessageBox.Show("Выберите проект");
                return;
            }

            var answer = MessageBox.Show(
                "Удалить проект \"" + selected.Name + "\" и все его задачи?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (answer == MessageBoxResult.Yes)
            {
                try
                {
                    _projectRepo.Delete(selected.Id);
                    LoadProjects();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgTasks.SelectedItem as TaskItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите задачу");
                return;
            }

            var answer = MessageBox.Show("Удалить задачу \"" + selected.Title + "\"?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {
                try
                {
                    _taskRepo.Delete(selected.Id);
                    LoadTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReportWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void CheckDeadlines()
        {
            try
            {
                string message = _notifier.BuildMessage(_taskRepo.GetAll());
                if (message != null)
                {
                    MessageBox.Show(message, "Приближаются сроки задач",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка проверки сроков: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckDeadlines();

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(5) };
            _timer.Tick += (s, args) =>
            {
                LoadTasks();
                CheckDeadlines();
            };
            _timer.Start();
        }
    }
}
