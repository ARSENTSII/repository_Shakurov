using System;

class Program
{
    static void Main()
    {
        Random rnd = new Random(); // генератор случайных чисел
        int secret = rnd.Next(1, 11); 

        while (true) 
        {
            Console.Write("Угадайте число от 1 до 10: "); 
            int n = int.Parse(Console.ReadLine());

            if (secret == n)
            {
                Console.WriteLine($"Вы угадали число {secret}!");
                break;
            }

            else
            {
                Console.WriteLine($"Увы, но Вы не угадали число {secret}!");
            }
        }
    }
}


