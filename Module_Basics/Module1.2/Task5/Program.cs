using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размерность символьного массива K: ");
        int k = int.Parse(Console.ReadLine());

        char[] letters = new char[k];

        string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        Random rnd = new Random();

        for (int i = 0; i < k; i++)
        {
            int index = rnd.Next(0, alphabet.Length);
            letters[i] = alphabet[index]; 
        }

        Console.Write($"Символьный массив: ");
        for (int i = 0;i < k; i++)
        {
            Console.Write($"{letters[i]}, ");
        }

        //Фильтрация согласных
        string glas = "аеёиоуыэюя";

        char[] consonants = new char[k];//массив для хранения согласных
        int count = 0;

        for (int i = 0; i < k; i++)
        {
            if (!glas.Contains(letters[i])) //проверка на согласные(! - это НЕ гласная?). 
            {
                consonants[count] = letters[i]; //помещаем в массив 
                count++;
            }
        }

        Console.WriteLine();
        Console.Write("Согласные буквы: ");
        for (int i = 0; i < count; i++)
        {
            Console.Write($"{consonants[i]}, ");
        }
    }
}
