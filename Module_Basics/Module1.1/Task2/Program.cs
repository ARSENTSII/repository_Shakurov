using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Проверка четности числа");
        Console.Write("Введите число: ");
        int n = int.Parse(Console.ReadLine()); //Перевод строки в число

        if (n % 2 == 0) //используем оператор отстатка от деления % и сравниваем остаток с 0
            {
                Console.WriteLine("Число четное"); //остаток == 0 
            }

            else
            {
                Console.WriteLine("Число не четное"); // остаток не == 0
            }
                
        }
    }


