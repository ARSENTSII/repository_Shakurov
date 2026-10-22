using System;                           // Uri, Math, Exception
using System.Windows;                   // Window, MessageBox, RoutedEventArgs
using System.Windows.Media.Imaging;     // BitmapImage (загрузка картинки)
using Microsoft.Win32;                  // OpenFileDialog (окно выбора файла)

namespace Task4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Создаёт элементы окна (кнопки, ползунок, Image) по разметке из XAML
            InitializeComponent();
        }

        // Кнопка «Открыть изображение»: выбор и показ картинки
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            // Стандартное окно «Открыть файл».
            // Фильтр: пары «название|маски», в масках расширения разделены точкой с запятой
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*"
            };    

            // ShowDialog() показывает окно и ждёт выбора.
            // Если файл не выбран (отмена), выходим из метода
            if (dialog.ShowDialog() != true)
                return; // пользователь нажал «Отмена»

            // try...catch перехватывает ошибки (например, файл не является картинкой),
            // чтобы программа не вылетала
            try
            {
                // Создаём объект картинки и загружаем в него файл.
                // Свойства BitmapImage нужно задавать между BeginInit() и EndInit()
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                // OnLoad: прочитать файл в память сразу целиком,
                // чтобы он не оставался занятым программой (его можно будет удалить или переименовать)
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // не блокировать файл

                // Путь к выбранному файлу
                bitmap.UriSource = new Uri(dialog.FileName);
                bitmap.EndInit();

                // Показываем картинку в элементе Image
                imgView.Source = bitmap;

                // Сбрасываем масштаб: новая картинка открывается в натуральном размере.
                // Изменение Value само вызовет zoomSlider_ValueChanged
                zoomSlider.Value = 1; // новая картинка открывается в масштабе 100%

                // Имя файла в заголовок окна.
                // System.IO.Path написан полностью, чтобы не подключать лишний using
                Title = "Просмотр изображений — " + System.IO.Path.GetFileName(dialog.FileName);
            }
            catch (Exception ex)
            {
                // Показываем понятное сообщение с текстом ошибки
                MessageBox.Show("Не удалось открыть изображение:\n" + ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Срабатывает при каждом движении ползунка масштаба
        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // При создании окна ползунок получает начальное значение раньше,
            // чем создаются остальные элементы (scale и txtZoom ещё null).
            // Без этой проверки программа упала бы при запуске
            // при создании окна событие приходит раньше, чем создан остальной интерфейс
            if (scale == null || txtZoom == null)
                return;

            // Масштаб по обеим осям одинаковый, чтобы картинка не искажалась.
            // 1 = 100%, 0.5 = 50%, 2 = 200%
            scale.ScaleX = zoomSlider.Value;
            scale.ScaleY = zoomSlider.Value;

            // Показываем масштаб в процентах (Math.Round убирает дробную часть)
            txtZoom.Text = Math.Round(zoomSlider.Value * 100) + "%";
        }

        // Кнопка «Сбросить»: возврат к 100%
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            // Достаточно поставить ползунок на 1:
            // обновление картинки и подписи сделает zoomSlider_ValueChanged
            zoomSlider.Value = 1;
        }
    }
}