using System;

class Program
{
    static void Main()
    {
        // Создаём вещественный массив из 10 элементов
        double[] arr = new double[10];

        // Генератор случайных чисел
        Random rnd = new Random();

        // Заполняем массив случайными значениями из диапазона [-10, 10)
        for (int i = 0; i < 10; i++)
        {
            arr[i] = rnd.NextDouble() * 20 - 10;
        }

        // Выводим исходный (неотсортированный) массив
        Console.WriteLine($"Вещественный массив: ");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{arr[i]:F2}   ");
        }
        Console.WriteLine();

        // Создаём массив индексов и заполняем его по порядку (0, 1, 2, ..., 9)
        // Пока это как будто "неотсортированный" порядок обхода массива arr
        int[] indices = new int[10];
        for (int i = 0; i < 10; i++)
        {
            indices[i] = i;
        }

        // Сортировка выбором: сортируем не сам arr, а массив indices,
        // чтобы обход arr в этом порядке дал значения по возрастанию
        for (int i = 0; i < 10; i++)
        {
            // Предполагаем, что минимум среди оставшихся стоит на позиции i
            int minPos = i;

            // Ищем среди ещё не отсортированной части (от i+1 до конца)
            // индекс, за которым в arr скрывается меньшее значение
            for (int j = i + 1; j < 10; j++)
            {
                if (arr[indices[j]] < arr[indices[minPos]])
                {
                    minPos = j; // нашли позицию с ещё меньшим значением
                }
            }

            // Меняем местами indices[i] и indices[minPos] через временную переменную
            int temp = indices[i];
            indices[i] = indices[minPos];
            indices[minPos] = temp;
        }

        // Выводим сам массив индексов — это и есть результат задачи
        Console.WriteLine("Массив индексов:");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{indices[i]}  ");
        }
        Console.WriteLine();

        // Для проверки: если пройти по arr в порядке indices,
        // значения должны идти строго по возрастанию
        Console.WriteLine("Элементы по возрастанию:");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{arr[indices[i]]:F2}   ");
        }
        Console.WriteLine();
    }
}