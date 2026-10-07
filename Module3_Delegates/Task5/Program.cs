using System;

//Делегат: метод, который принимает массив и сортирует его
delegate void SortMethod(int[] arr);

class Program
{
    //Методы с той же сигнатурой (void, один параметр int[])
    static void BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)           // сколько проходов
        {
            for (int j = 0; j < arr.Length - 1 - i; j++)   // идём по соседним парам
            {
                if (arr[j] > arr[j + 1])                   // левый больше правого?
                {
                    int tmp = arr[j];                      // меняем местами
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                }
            }
        }
    }

    static void InsertionSort(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)       // с 1, т.к. один элемент уже "отсортирован"
        {
            int key = arr[i];                      // запоминаем элемент, который вставляем
            int j = i - 1;                         // начинаем смотреть слева от него

            while (j >= 0 && arr[j] > key)         // пока слева есть элемент больше key
            {
                arr[j + 1] = arr[j];               // сдвигаем его вправо
                j--;                               // идём дальше влево
            }

            arr[j + 1] = key;                      // ставим key в освободившееся место
        }
    }

    static void Main()
    {
        while (true)
        {
            //Ввод чисел
            Console.WriteLine("Введите числа через пробел:");
            string[] parts = Console.ReadLine().Split(' ');
            int[] numbers = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                numbers[i] = int.Parse(parts[i]);

            //Выбор метода
            Console.WriteLine("1 - пузырьком, 2 - вставками");
            int choice = int.Parse(Console.ReadLine());

            //Делегат хранит ссылку на нужный метод
            SortMethod sort;
            if (choice == 1)
                sort = BubbleSort;
            else
                sort = InsertionSort;

            sort(numbers); //  вызываем выбранный метод

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}