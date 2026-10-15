using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Task1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double w = canvas.ActualWidth;
            double h = canvas.ActualHeight;

            Line line = new Line
            {
                X1 = rnd.NextDouble() * w,
                Y1 = rnd.NextDouble() * h,
                X2 = rnd.NextDouble() * w,
                Y2 = rnd.NextDouble() * h,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            canvas.Children.Add(line);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double size = rnd.Next(20, 100);

            Ellipse circle = new Ellipse
            {
                Width = size,
                Height = size,
                Stroke = Brushes.Green,
                StrokeThickness = 2
            };
            Canvas.SetLeft(circle, rnd.NextDouble() * Math.Max(0, canvas.ActualWidth - size));
            Canvas.SetTop(circle, rnd.NextDouble() * Math.Max(0, canvas.ActualHeight - size));
            canvas.Children.Add(circle);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double size = rnd.Next(20, 100);

            Rectangle square = new Rectangle
            {
                Width = size,
                Height = size,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            Canvas.SetLeft(square, rnd.NextDouble() * Math.Max(0, canvas.ActualWidth - size));
            Canvas.SetTop(square, rnd.NextDouble() * Math.Max(0, canvas.ActualHeight - size));
            canvas.Children.Add(square);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }
    }
}