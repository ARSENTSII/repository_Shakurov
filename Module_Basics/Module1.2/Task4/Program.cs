using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размерность массива K: ");
        int k = int.Parse(Console.ReadLine());

        Console.Write("Введите диапазон A: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите  B: ");
        int b = int.Parse(Console.ReadLine());

        int[] arr = new int[k];
        Random rnd = new Random();

        for (int i = 0; i < k; i++)
        {
            arr[i] = rnd.Next(a, b);
        }

        Console.WriteLine("Массив K: ");
        for (int i = 0; i < k; i++)
        {
            Console.Write($"{arr[i]} ");
        }
        Console.WriteLine();   

        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 0; i < k; i++)
        {
            if (arr[i] < arr[minIndex])
            {
                minIndex = i;
            }

            if (arr[i] > arr[maxIndex])
            {
                maxIndex = i;
            }
        }

        int start = Math.Min(minIndex, maxIndex);
        int end = Math.Max(minIndex, maxIndex);

        Console.WriteLine("Найденные элементы:");   
        for (int i = start; i <= end; i++)
        {
            Console.Write($"{arr[i]} ");            
        }
        Console.WriteLine();
    }
}