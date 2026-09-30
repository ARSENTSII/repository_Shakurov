using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string s = Console.ReadLine(); //Переменная s - наша строка

        //Проверка на то, что пуста ли строка
        if (string.IsNullOrWhiteSpace(s)) //использовали встроенного метода который ловит все три случая: строка пуста, строки нет, строка из пробелов
        {
            Console.WriteLine("Строка пуста");
        }

        else
        {
            char[] letters = s.ToCharArray(); //превращаем строку массив из символов 
            Array.Reverse(letters); //разворачиваем массив
            string reversed = new string(letters); //собираем обратно

            //Проверка на палиндром (Строка палиндром, если она равна самой себе, записанной задом наперёд. Например, казак, шалаш, level). 
            if (s == reversed) //reversed - перевернутая строка
            {
                Console.WriteLine("Строка является палиндромом"); 
            }

            else
            {
                Console.WriteLine("Строка не является палиндромом");
            } 
        }
    }
}