using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню: ");
            Console.WriteLine("1 - Перевести градусы Цельсия в Фаренгейты");
            Console.WriteLine("2 - Перевести из Фаренгейт в градусы Цельсия");
            Console.WriteLine("0 - Выход");
            Console.WriteLine();

            string choice = Console.ReadLine(); //Считывание выбора пользователя

            if (choice == "0") //Если выбрал 0
            {
                break;
            }

            else if (choice == "1") //Если выбрал 1
            {
                Console.WriteLine("Введите температуру в °C");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double celsius))
                {
                    double result = CelsiusToFahrenheit(celsius); //Вызов метода CelsiusToFahrenheit
                    Console.WriteLine($"{celsius} °C = {result:F2} °F"); //Вывод результата
                }
                else
                {
                    Console.WriteLine("Введите число");
                }

                Console.WriteLine();
            }

            else if (choice == "2") //Если выбрал 2
            {
                Console.WriteLine("Введите температуру в °F");
                string input2 = Console.ReadLine();

                if (double.TryParse(input2, out double fahrenheit))
                {
                    double result = FahrenheitToCelsius(fahrenheit); //Вызов метода FahrenheitToCelsius
                    Console.WriteLine($"{fahrenheit} °F = {result:F2} °C"); //Вывод результата
                }
                else 
                {
                    Console.WriteLine("Введите число");
                }

                Console.WriteLine();
            }

            else
            {
                Console.WriteLine("Неверный пункт меню");
            }
        }
    }

    static double CelsiusToFahrenheit(double celsius) //Метод для преобразования температуры из градусов по Цельсия в градусы Фаренгейта 
    {
        return celsius * 9.0 / 5.0 + 32;
    }

    static double FahrenheitToCelsius(double fahrenheit) //Метод для преобразования температуры из градусов по Фаренгейту в градусы Цельсия 
    {
        return (fahrenheit - 32) * 5.0 / 9.0;
    }
}