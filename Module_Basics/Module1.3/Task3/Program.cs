using System;

class Program
{
    static void Main()
    {
        // Вводим размер квадратной матрицы
        Console.Write("Введите размер матрицы N: ");
        int n = int.Parse(Console.ReadLine());

        // Генерируем матрицу со случайными значениями через отдельный метод
        int[,] matrix = GenerateMatrix(n);

        // Выводим исходную (ещё не отсортированную) матрицу
        Console.WriteLine("Исходная матрица:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{matrix[i, j],4}"); // выравнивание по правому краю, ширина поля 4 символа
            }
            Console.WriteLine(); // переход на новую строку после вывода всей строки матрицы
        }

        // Считаем сумму элементов каждой строки через отдельный метод
        int[] rowSums = GetRowSums(matrix, n);

        int[] rowIndices = new int[n];
        for (int i = 0; i < n; i++)
        {
            rowIndices[i] = i;
        }

        for (int i = 0; i < n; i++)
        {
            int minPos = i;
            for (int j = i + 1; j < n; j++)
            {
                if (rowSums[rowIndices[j]] < rowSums[rowIndices[minPos]])
                {
                    minPos = j;
                }
            }
            int temp = rowIndices[i];
            rowIndices[i] = rowIndices[minPos];
            rowIndices[minPos] = temp;
        }


    }

    // Метод создаёт квадратную матрицу n x n и заполняет её
    // случайными целыми числами из диапазона [-50, 50]
    static int[,] GenerateMatrix(int n)
    {
        int[,] matrix = new int[n, n];
        Random rnd = new Random();

        // Проходим по всем строкам (i) и столбцам (j) матрицы
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rnd.Next(-50, 51); // 51 не включается, значит 50 тоже может выпасть
            }
        }

        return matrix;
    }

    // Метод считает сумму элементов каждой строки матрицы
    // и возвращает массив этих сумм (rowSums[i] — сумма строки i)
    static int[] GetRowSums(int[,] matrix, int n)
    {
        int[] rowSums = new int[n];

        // Для каждой строки i считаем сумму её элементов
        for (int i = 0; i < n; i++)
        {
            int sum = 0;
            for (int j = 0; j < n; j++)
            {
                sum += matrix[i, j]; // прибавляем каждый элемент строки
            }
            rowSums[i] = sum; // сохраняем итоговую сумму строки
        }

        return rowSums;
    }
}