using System;
using System.Collections.Generic;

// Делегат: метод, который принимает строку и возвращает true/false
delegate bool FilterCondition(string item);

class Program
{
    // Правило №1: строка содержит слово "Купить"
    static bool WordFilter(string item)
    {
        return item.Contains("Купить");
    }

    // Правило №2: строка содержит дату "01.10.2026"
    static bool DateFilter(string item)
    {
        return item.Contains("01.10.2026");
    }

    static void Main()
    {
        List<string> data = new List<string>
        {
            "Встреча 01.10.2026 важно",
            "Купить молоко",
            "Отчёт важно сдать",
            "Прогулка 05.10.2026"
        };

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Фильтрация по дате");
            Console.WriteLine("2 - Фильтрация по ключевому слову");
            Console.WriteLine("0 - Выход");

            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break;
            }

            // Выбираем правило: кладём нужный метод в переменную-делегат
            FilterCondition filter;

            if (choice == "1")
            {
                filter = DateFilter;
            }
            else if (choice == "2")
            {
                filter = WordFilter;
            }
            else
            {
                Console.WriteLine("Неверный пункт меню");
                continue; // пропускаем остаток цикла и показываем меню заново
            }

            // Проходим по данным и выводим те строки, которые подходят
            bool found = false;

            foreach (string item in data)
            {
                if (filter(item))   // вызываем выбранное правило
                {
                    Console.WriteLine(item);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ничего не найдено");
            }
        }
    }
}