using System;

class Program
{
    static void Main()
    {
        // Вводим числитель дроби (по условию неотрицательный)
        Console.Write("Введите числитель: ");
        int numerator = int.Parse(Console.ReadLine());

        // Вводим знаменатель дроби (по условию положительный)
        Console.Write("Введите знаменатель: ");
        int denominator = int.Parse(Console.ReadLine());

        // Находим наибольший общий делитель числителя и знаменателя
        int divisor = GCD(numerator, denominator);

        // Делим числитель и знаменатель на их НОД — получаем сокращённую дробь
        int reducedNumerator = numerator / divisor;
        int reducedDenominator = denominator / divisor;

        // Выводим исходную и сокращённую дробь
        Console.WriteLine($"{numerator}/{denominator} = {reducedNumerator}/{reducedDenominator}");
    }

    // Метод вычисляет НОД двух чисел 
    static int GCD(int a, int b)
    {
        // Пока b не станет равным 0, повторяем:
        // новый b — это остаток от деления a на b,
        // новый a — это старое значение b
        while (b != 0)
        {
            int temp = b;   // временно сохраняем текущее b
            b = a % b;      // остаток от деления a на b становится новым b
            a = temp;       // старое b становится новым a
        }
        // когда b == 0, в a остаётся сам НОД
        return a;
    }
}