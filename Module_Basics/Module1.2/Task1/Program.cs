using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива: ");
        int N = int.Parse(Console.ReadLine());

        double[] arr = new double [N]; 
        
        for (int i = 0; i < N; i++) 
        {
            Console.Write($"Элемент {i}: ");
            arr[i] = double.Parse(Console.ReadLine()); //заносим элемент в массив
        }

        double maxAbs = Math.Abs(arr[0]); //старт с модуля первого элемента

        for (int i = 0; i < N; i++) 
        {
            if (Math.Abs(arr[i]) > maxAbs)
            {
                maxAbs = Math.Abs(arr[i]);
            }
        }

        for (int i = 0; i < N; i++)
        {
            arr[i] = arr[i] / maxAbs;
        }

        Console.WriteLine("Новые элементы массива");

        for ( int i = 0; i < N; i++)
        {
            Console.WriteLine($"Элемент {i}: {arr[i]:F2}");
        }
    }
}
