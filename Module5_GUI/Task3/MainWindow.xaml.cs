```
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Task3
{

    // Одна задача: текст и признак «выполнена»
    public class TodoItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isDone;

        public string Title { get; set; }

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                isDone = value;
                // сообщаем интерфейсу об изменении (нужно для зачёркивания)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsDone"));
            }
        }
    }

    public partial class MainWindow : Window
    {
        // Коллекция задач: сама сообщает списку об изменениях
        private ObservableCollection<TodoItem> tasks = new ObservableCollection<TodoItem>();

        public MainWindow()
        {
            InitializeComponent();
            listTasks.ItemsSource = tasks; // подключаем коллекцию к списку
        }

        private void AddTask()
        {
            string text = txtNewTask.Text.Trim();
            if (text == "")
                return; // пустые задачи не добавляем

            tasks.Add(new TodoItem { Title = text });
            txtNewTask.Clear();
            txtNewTask.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void txtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AddTask();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TodoItem selected = listTasks.SelectedItem as TodoItem;

            if (selected == null)
            {
                MessageBox.Show("Выберите задачу для удаления.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            tasks.Remove(selected);
        }
    }
}
```