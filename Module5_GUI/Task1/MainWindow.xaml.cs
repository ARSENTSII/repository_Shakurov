 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;                 // Window, RoutedEventArgs
using System.Windows.Controls;        // Canvas
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;           // Brushes (цвета линий)
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;          // Line, Ellipse, Rectangle (фигуры)


namespace Task1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Генератор случайных чисел. Создаётся один раз для всего окна,
        // чтобы при быстрых нажатиях не получались одинаковые числа
        private Random rnd = new Random();

        public MainWindow()
        {
            // Создаёт элементы окна (кнопки, холст) по разметке из XAML
            InitializeComponent();

        }

        // Кнопка «Нарисовать линию»
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Текущие размеры холста, чтобы линия не вышла за его границы
            double w = canvas.ActualWidth;
            double h = canvas.ActualHeight;

            // Создаём линию со случайными концами
            // NextDouble() даёт число от 0 до 1, умножаем на размер холста
            Line line = new Line
            {
                X1 = rnd.NextDouble() * w,   // начало линии по X
                Y1 = rnd.NextDouble() * h,   // начало линии по Y
                X2 = rnd.NextDouble() * w,   // конец линии по X
                Y2 = rnd.NextDouble() * h,   // конец линии по Y
                Stroke = Brushes.Black,      // цвет линии
                StrokeThickness = 2          // толщина линии
            };

            // Фигура появится на экране только после добавления на холст
            canvas.Children.Add(line);
        }

        // Кнопка «Нарисовать круг»
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Случайный диаметр от 20 до 99
            double size = rnd.Next(20, 100);

            // Эллипс с равными шириной и высотой — это круг
            Ellipse circle = new Ellipse
            {
                Width = size,
                Height = size,
                Stroke = Brushes.Green,      // зелёный контур (без заливки)
                StrokeThickness = 2
            };

            // Положение фигуры на холсте: отступ слева и сверху.
            // Вычитаем size, чтобы круг целиком помещался в холст.
            // Math.Max(0, ...) защищает от отрицательного числа, если холст меньше фигуры
            Canvas.SetLeft(circle, rnd.NextDouble() * Math.Max(0, canvas.ActualWidth - size));
            Canvas.SetTop(circle, rnd.NextDouble() * Math.Max(0, canvas.ActualHeight - size));

            canvas.Children.Add(circle);
        }

        // Кнопка «Нарисовать квадрат»
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Случайная длина стороны от 20 до 99
            double size = rnd.Next(20, 100);

            // Прямоугольник с равными сторонами — это квадрат
            Rectangle square = new Rectangle
            {
                Width = size,
                Height = size,
                Stroke = Brushes.Red,        // красный контур
                StrokeThickness = 2
            };

            // Случайная позиция внутри холста (так же, как у круга)
            Canvas.SetLeft(square, rnd.NextDouble() * Math.Max(0, canvas.ActualWidth - size));
            Canvas.SetTop(square, rnd.NextDouble() * Math.Max(0, canvas.ActualHeight - size));

            canvas.Children.Add(square);
        }

        // Кнопка «Очистить холст»
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Удаляет все фигуры, добавленные на холст
            canvas.Children.Clear();
        }
    }
}