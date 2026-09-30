using System;

class Program
{
    static void Main()
    {
        string[] cities = { "Москва", "Питер", "Краснодар", "Воронеж", "Калининград" }; 
        Console.Write("Введите название города: "); 
        string c = Console.ReadLine(); 

        int foundIndex = -1; //переменная для случая если в массиве не найден введенный пользователем город

        if (string.IsNullOrWhiteSpace(c)) //Проверка на то что ввел ли пользовател город
        {
            Console.WriteLine("Строка пуста"); 
        }

        else 
        {
            for (int i = 0; i < cities.Length; i++) //перебор массива
            {
                if (cities[i] == c) //если в массиве присутствует город, который ввел пользователь 
                {
                    foundIndex = i;
                    break; //завершаем 
                }
            }

            if (foundIndex == -1)
            {
                Console.WriteLine("Город не найден");
            }
            else
            {
                Console.WriteLine($"Город найден, индекс: {foundIndex}"); //вывод индекса найденного города
            }
        }
    }
}



