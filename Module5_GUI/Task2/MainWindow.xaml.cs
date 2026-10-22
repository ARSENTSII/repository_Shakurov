using Microsoft.Win32;                // OpenFileDialog, SaveFileDialog (окна выбора файла)
using System;                         // Exception
using System.Collections.Generic;
using System.Linq;
using System.Text;                    // Encoding (кодировка текста)
using System.Threading.Tasks;
using System.Windows;                 // Window, MessageBox, RoutedEventArgs
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;                      // File, Path (работа с файлами и путями)



namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Фильтр для окон выбора файла: пары «название|маска», разделённые знаком |
        // Пользователь увидит два варианта: только .txt или все файлы
        private const string Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

        // Путь к файлу, с которым сейчас работаем.
        // null означает, что файл ещё не открывали и не сохраняли
        private string currentFile = null;

        public MainWindow()
        {
            // Создаёт элементы окна (меню, textBox1) по разметке из XAML
            InitializeComponent();
        }

        // Пункт меню «Файл → Открыть»
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            // Создаём стандартное окно «Открыть файл» и задаём ему фильтр
            OpenFileDialog dialog = new OpenFileDialog { Filter = Filter };

            // ShowDialog() показывает окно и ждёт выбора.
            // Если пользователь нажал «Отмена» (результат не true), выходим из метода
            if (dialog.ShowDialog() != true)
                return;

            // try...catch перехватывает ошибки чтения, чтобы программа не вылетала
            try
            {
                // Читаем весь файл одной строкой и выводим в текстовое поле.
                // Кодировка должна совпадать с той, в которой сохранён файл,
                // иначе вместо русских букв будут «кракозябры»
                textBox1.Text = File.ReadAllText(dialog.FileName, Encoding.UTF8);

                // Запоминаем путь к открытому файлу
                currentFile = dialog.FileName;

                // Показываем имя файла в заголовке окна
                // Path.GetFileName отрезает от полного пути только имя файла
                Title = "Текстовый редактор — " + Path.GetFileName(currentFile);
            }
            catch (Exception ex)
            {
                // Если что-то пошло не так (файл занят, нет доступа и т. д.),
                // показываем сообщение с текстом ошибки
                MessageBox.Show(ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Пункт меню «Файл → Сохранить как»
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            // Создаём стандартное окно «Сохранить как» с тем же фильтром
            SaveFileDialog dialog = new SaveFileDialog { Filter = Filter };

            // Если файл уже открывали или сохраняли, подставляем его имя в диалог
            if (currentFile != null)
                dialog.FileName = Path.GetFileName(currentFile);

            // Отмена в диалоге: ничего не сохраняем
            if (dialog.ShowDialog() != true)
                return;

            try
            {
                // Записываем текст из поля в выбранный файл.
                // Если файл существует, он перезаписывается, если нет, создаётся
                File.WriteAllText(dialog.FileName, textBox1.Text, Encoding.UTF8);

                // Обновляем текущий файл и заголовок окна
                currentFile = dialog.FileName;
                Title = "Текстовый редактор — " + Path.GetFileName(currentFile);
            }
            catch (Exception ex)
            {
                // Ошибка записи (например, нет прав на папку): показываем сообщение
                MessageBox.Show(ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Пункт меню «Файл → Выход»
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Закрывает окно; так как оно единственное, программа завершается
            Close();
        }
    }
}