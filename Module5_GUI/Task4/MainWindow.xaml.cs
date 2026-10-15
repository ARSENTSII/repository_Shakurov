using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Task4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Выбор и показ изображения
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*"
            };

            if (dialog.ShowDialog() != true)
                return; // пользователь нажал «Отмена»

            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // не блокировать файл
                bitmap.UriSource = new Uri(dialog.FileName);
                bitmap.EndInit();

                imgView.Source = bitmap;
                zoomSlider.Value = 1; // новая картинка открывается в масштабе 100%
                Title = "Просмотр изображений — " + System.IO.Path.GetFileName(dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть изображение:\n" + ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Масштаб при движении ползунка
        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // при создании окна событие приходит раньше, чем создан остальной интерфейс
            if (scale == null || txtZoom == null)
                return;

            scale.ScaleX = zoomSlider.Value;
            scale.ScaleY = zoomSlider.Value;
            txtZoom.Text = Math.Round(zoomSlider.Value * 100) + "%";
        }

        // Возврат к 100%
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            zoomSlider.Value = 1;
        }
    }
}