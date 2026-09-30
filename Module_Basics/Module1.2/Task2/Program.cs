using System;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("Массив до изменения:");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine($"Элемент {i}: {arr[i]}");
        }

        Console.Write("Введите число для замены маскимального элемента в массиве: ");
        int n = int.Parse(Console.ReadLine());

        int maxIndex = 0;

        for (int i = 0; i < arr.Length; i++)
        {

            if (arr[i] > arr[maxIndex])
            {
                maxIndex = i;
            }
        }

        arr[maxIndex] = n;

        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine(arr[i]);
        }
    }
}

