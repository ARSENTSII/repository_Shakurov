using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Task5
{
    public partial class MainWindow : Window
    {
        private double firstNumber = 0;     // первое число
        private string currentOp = null;    // выбранная операция: "+", "-", "*", "/"
        private bool resultShown = false;   // на экране результат после «=»

        public MainWindow()
        {
            InitializeComponent();
        }

        // ---------- вспомогательные методы ----------

        // Текст из поля -> число (запятая заменяется на точку)
        private double Parse(string text)
        {
            return double.Parse(text.Replace(",", "."), CultureInfo.InvariantCulture);
        }

        // Число -> текст с запятой (G15 убирает «хвосты» вроде 0,30000000000000004)
        private string Format(double value)
        {
            return value.ToString("G15", CultureInfo.InvariantCulture).Replace(".", ",");
        }

        // Выполняет операцию; false, если считать нельзя
        private bool TryCalculate(double a, double b, string op, out double result)
        {
            result = 0;

            switch (op)
            {
                case "+": result = a + b; return true;
                case "-": result = a - b; return true;
                case "*": result = a * b; return true;
                case "/":
                    if (b == 0)
                    {
                        MessageBox.Show("Деление на ноль невозможно.", "Внимание",
                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                    result = a / b;
                    return true;
                default:
                    return false;
            }
        }

        //цифры и запятая 
        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (resultShown)
            {
                txtDisplay.Clear();
                resultShown = false;
            }

            if (txtDisplay.Text == "0")
                txtDisplay.Text = digit;
            else
                txtDisplay.Text += digit;
        }

        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            if (resultShown)
            {
                txtDisplay.Clear();
                resultShown = false;
            }

            if (txtDisplay.Text.Contains(","))
                return;

            if (txtDisplay.Text == "")
                txtDisplay.Text = "0,";
            else
                txtDisplay.Text += ",";
        }

        // ---------- операции ----------

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            string op = ((Button)sender).Content.ToString();

            // поле пустое: если операция уже выбрана, просто меняем знак
            if (txtDisplay.Text == "")
            {
                if (currentOp != null)
                {
                    currentOp = op;
                    lblExpr.Text = Format(firstNumber) + op;
                }
                return;
            }

            double current = Parse(txtDisplay.Text);

            if (currentOp != null)
            {
                // цепочка: 1 + 2 + ... сначала считаем 1 + 2
                double result;
                if (!TryCalculate(firstNumber, current, currentOp, out result))
                    return;
                firstNumber = result;
            }
            else
            {
                firstNumber = current;
            }

            currentOp = op;
            lblExpr.Text = Format(firstNumber) + op;
            txtDisplay.Clear();
            resultShown = false;
        }

        // ---------- равно ----------

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            // нечего считать: операция не выбрана или второе число не введено
            if (currentOp == null || txtDisplay.Text == "")
                return;

            double second = Parse(txtDisplay.Text);
            double result;

            if (!TryCalculate(firstNumber, second, currentOp, out result))
                return;

            txtDisplay.Text = Format(result);
            lblExpr.Text = "";
            currentOp = null;
            resultShown = true;
        }
    }
}