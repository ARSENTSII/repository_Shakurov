using System;

class Program
{
    static void Main()
    {
        // Вводим предел, который не должна превышать сумма элементов массива
        Console.Write("Введите предел суммы: ");
        int limit = int.Parse(Console.ReadLine());

        // Вызываем метод, который генерирует массив по правилу и возвращает его
        int[] result = GenerateArray(limit);

        // Выводим полученный массив
        Console.WriteLine("Полученный массив:");
        for (int i = 0; i < result.Length; i++)
        {
            Console.Write($"{result[i]} ");
        }
        Console.WriteLine();

        // Считаем сумму элементов полученного массива (для проверки/вывода)
        int sum = 0;
        for (int i = 0; i < result.Length; i++)
        {
            sum += result[i];
        }

        // Выводим итоговую сумму и количество элементов
        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Количество элементов: {result.Length}");
    }

    // Метод генерирует массив случайных чисел от 1 до 9,
    // добавляя их по одному, пока сумма не приблизится к пределу limit.
    // Возвращает готовый массив минимального размера, укладывающийся в предел.
    static int[] GenerateArray(int limit)
    {
        int[] arr = new int[1000];  // массив "с запасом" — реальный размер заранее не известен
        int count = 0;              // сколько элементов реально добавлено
        int sum = 0;                // текущая накопленная сумма
        Random rnd = new Random();

        while (true)
        {
            // генерируем случайное число от 1 до 9
            int nextValue = rnd.Next(1, 10);

            // если добавление этого числа превысит предел — останавливаемся,
            // само число в массив не попадает
            if (sum + nextValue > limit)
            {
                break;
            }

            // иначе добавляем число в массив и обновляем сумму и счётчик
            arr[count] = nextValue;
            count++;
            sum += nextValue;
        }

        // обрезаем массив до реального количества элементов (убираем лишние пустые ячейки)
        Array.Resize(ref arr, count);
        return arr;
    }
}