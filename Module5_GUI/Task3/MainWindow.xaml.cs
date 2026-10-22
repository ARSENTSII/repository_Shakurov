using System.Collections.ObjectModel; // ObservableCollection (список, который сообщает об изменениях)
using System.ComponentModel;          // INotifyPropertyChanged, PropertyChangedEventArgs
using System.Windows;                 // Window, MessageBox, RoutedEventArgs
using System.Windows.Input;           // KeyEventArgs, Key (работа с клавиатурой)

namespace Task3
{

    // Одна задача: текст и признак «выполнена»
    // Интерфейс INotifyPropertyChanged позволяет объекту сообщать
    // интерфейсу (через Binding), что его свойство изменилось
    public class TodoItem : INotifyPropertyChanged   
    {
        // Событие, которое требует интерфейс.
        // На него автоматически подписывается привязка данных из XAML
        public event PropertyChangedEventHandler PropertyChanged;

        // Поле, в котором хранится значение «выполнена»
        private bool isDone;

        // Текст задачи. Он не меняется после создания,
        // поэтому достаточно обычного автосвойства без уведомлений
        public string Title { get; set; }

        // Признак «выполнена» (галочка в списке)
        public bool IsDone
        {
            get { return isDone; }
            set
            {
                isDone = value; // записываем новое значение

                // Сообщаем интерфейсу об изменении (нужно для зачёркивания).
                // Проверка на null нужна: если на событие никто не подписан, вызывать его нельзя
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsDone"));
            }
        }
    }

    public partial class MainWindow : Window
    {
        // Коллекция всех задач.
        // ObservableCollection сама сообщает списку об добавлении и удалении элементов,
        // поэтому ListBox обновляется автоматически
        private ObservableCollection<TodoItem> tasks = new ObservableCollection<TodoItem>();

        public MainWindow()
        {
            // Создаёт элементы окна по разметке из XAML
            InitializeComponent();

            // Подключаем коллекцию к списку на экране.
            // Каждая задача будет нарисована по шаблону (DataTemplate) из XAML
            listTasks.ItemsSource = tasks;
        }

        // Добавляет новую задачу из поля ввода.
        // Вынесено в отдельный метод, потому что его вызывают и кнопка, и клавиша Enter
        private void AddTask()
        {
            // Берём текст и убираем пробелы по краям
            string text = txtNewTask.Text.Trim();

            // Пустые задачи не добавляем
            if (text == "")
                return;

            // Создаём задачу и добавляем в коллекцию: она сразу появится в списке.
            // IsDone по умолчанию false, то есть задача невыполненная
            tasks.Add(new TodoItem { Title = text });

            // Очищаем поле ввода и возвращаем в него курсор для следующей задачи
            txtNewTask.Clear();
            txtNewTask.Focus();
        }

        // Кнопка «Добавить»
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        // Нажатие любой клавиши в поле ввода
        private void txtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            // Реагируем только на Enter: добавляем задачу
            if (e.Key == Key.Enter)
                AddTask();
        }

        // Кнопка «Удалить выбранную»
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Выбранный в списке элемент. SelectedItem имеет тип object,
            // поэтому приводим его к TodoItem оператором as
            // (если ничего не выбрано или тип не подошёл, получим null)
            TodoItem selected = listTasks.SelectedItem as TodoItem;

            // Ничего не выбрано: показываем подсказку и выходим
            if (selected == null)
            {
                MessageBox.Show("Выберите задачу для удаления.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Удаляем задачу из коллекции, из списка она исчезнет сама
            tasks.Remove(selected);
        }
    }
}