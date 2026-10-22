using System;
using System.Globalization;        // CultureInfo (разбор и вывод чисел независимо от настроек Windows)
using System.Windows;              // Window, MessageBox, RoutedEventArgs
using System.Windows.Controls;     // Button

namespace Task5
{
    public partial class MainWindow : Window
    {
        private double firstNumber = 0;     // первое число (запоминается при нажатии операции)
        private string currentOp = null;    // выбранная операция: "+", "-", "*", "/" (null = не выбрана)
        private bool resultShown = false;   // true, когда на экране результат после «=»:
                                            // следующая цифра начнёт ввод заново

        public MainWindow()
        {
            // Создаёт элементы окна (поле, подпись, кнопки) по разметке из XAML
            InitializeComponent();
        }

        // ---------- вспомогательные методы ----------

        // Текст из поля -> число.
        // Запятая заменяется на точку, а InvariantCulture делает разбор одинаковым
        // на любом компьютере, независимо от региональных настроек
        private double Parse(string text)
        {
            return double.Parse(text.Replace(",", "."), CultureInfo.InvariantCulture);
        }

        // Число -> текст с запятой.
        // Формат G15 убирает «хвосты» вида 0,30000000000000004 (особенность double)
        private string Format(double value)
        {
            return value.ToString("G15", CultureInfo.InvariantCulture).Replace(".", ",");
        }

        // Выполняет операцию над двумя числами.
        // Результат возвращается через out-параметр result,
        // а сам метод возвращает true, если посчитать удалось, и false, если нельзя
        private bool TryCalculate(double a, double b, string op, out double result)
        {
            result = 0; // out-параметру обязательно присвоить значение

            switch (op)
            {
                case "+": result = a + b; return true;
                case "-": result = a - b; return true;
                case "*": result = a * b; return true;
                case "/":
                    // Деление на ноль: предупреждаем и не считаем
                    if (b == 0)
                    {
                        MessageBox.Show("Деление на ноль невозможно.", "Внимание",
                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                    result = a / b;
                    return true;
                default:
                    return false; // неизвестная операция
            }
        }

        // ---------- цифры и запятая ----------

        // Один общий обработчик для всех кнопок 0–9
        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            // sender — кнопка, на которую нажали; её надпись (Content) и есть цифра
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            // После «=» на экране результат: новая цифра начинает ввод заново
            if (resultShown)
            {
                txtDisplay.Clear();
                resultShown = false;
            }

            // Если на экране один ноль, заменяем его цифрой (чтобы не получалось 07)
            if (txtDisplay.Text == "0")
                txtDisplay.Text = digit;
            else
                txtDisplay.Text += digit; // иначе дописываем цифру справа
        }

        // Кнопка «,»
        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            // После результата начинаем ввод нового числа
            if (resultShown)
            {
                txtDisplay.Clear();
                resultShown = false;
            }

            // Вторую запятую в одном числе ставить нельзя
            if (txtDisplay.Text.Contains(","))
                return;

            // Если число ещё не начато, получится «0,»
            if (txtDisplay.Text == "")
                txtDisplay.Text = "0,";
            else
                txtDisplay.Text += ",";
        }

        // ---------- операции ----------

        // Один общий обработчик для кнопок + - * /
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            // Знак операции берём из надписи нажатой кнопки
            string op = ((Button)sender).Content.ToString();

            // Поле пустое: если операция уже выбрана, просто меняем знак
            // (например, нажали «+», а хотели «*»)
            if (txtDisplay.Text == "")
            {
                if (currentOp != null)
                {
                    currentOp = op;
                    lblExpr.Text = Format(firstNumber) + op;
                }
                return; // иначе ничего не делаем, программа не падает
            }

            // Число, которое сейчас введено в поле
            double current = Parse(txtDisplay.Text);

            if (currentOp != null)
            {
                // Цепочка операций: при вводе 1 + 2 + ... сначала считаем 1 + 2,
                // и результат становится первым числом
                double result;
                if (!TryCalculate(firstNumber, current, currentOp, out result))
                    return; // посчитать нельзя (например, деление на ноль)
                firstNumber = result;
            }
            else
            {
                // Первая операция: запоминаем введённое число как первое
                firstNumber = current;
            }

            currentOp = op;                              // запоминаем выбранную операцию
            lblExpr.Text = Format(firstNumber) + op;     // в подписи показываем, например, «5+»
            txtDisplay.Clear();                          // поле очищаем для ввода второго числа
            resultShown = false;
        }

        // ---------- равно ----------

        // Кнопка «=»
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            // Нечего считать: операция не выбрана или второе число не введено
            if (currentOp == null || txtDisplay.Text == "")
                return;

            // Второе число берём из поля
            double second = Parse(txtDisplay.Text);
            double result;

            // Считаем; если нельзя (деление на ноль), просто выходим
            if (!TryCalculate(firstNumber, second, currentOp, out result))
                return;

            txtDisplay.Text = Format(result);  // выводим результат в поле
            lblExpr.Text = "";                 // подпись очищаем
            currentOp = null;                  // операция выполнена, сбрасываем её
            resultShown = true;                // следующая цифра начнёт ввод заново
        }
    }
}