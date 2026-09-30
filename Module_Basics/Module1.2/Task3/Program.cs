using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите K: ");
        int k = int.Parse(Console.ReadLine());

        int count = 0;
        int candidate = 2;

        while (count < k)
        {
            if (IsPrime(candidate))
            {
                Console.Write($"{candidate} ");
                count++;

                if (count % 10 == 0)
                {
                    Console.WriteLine();
                }
            }

            candidate++;
        }
    }

    static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int d = 2; d < number; d++)
        {
            if (number % d == 0)
            {
                return false;
            }
        }

        return true;
    }
}