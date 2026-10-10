using System;

interface IBook
{
    string Title();

    bool IsAvailable();

    bool Issue();
}

class PaperBook : IBook
{
    private string name;

    private int copies;

    public PaperBook(string name, int copies)
    {
        this.name = name;
        this.copies = copies;
    }

    public string Title()
    {
        return name;
    }

    public int GetCopies()
    {
        return copies;
    }

    public bool IsAvailable() 
    {
        return copies > 0;
    }

    public bool Issue()
    {
        if (copies > 0)
        {
            copies--;
            return true;
        }

        return false;
    } 
}

class EBook : IBook
{
    private string name;

    public EBook(string name)
    {
        this.name = name;
    }

    public string Title()
    {
        return name;
    }

    public bool IsAvailable()
    {
        return true;
    }

    public bool Issue()
    {
        return true;
    }
}

class RareBook : IBook
{
    private string name;

    public RareBook(string name)
    {
        this.name = name;
    }

    public string Title()
    {
        return name;
    }

    public bool IsAvailable()
    {
        return true;
    }

    public bool Issue()
    {
        return false;
    }
}

class Program
{
    static void Main()
    {
        PaperBook paperBook = new PaperBook("Программирование", 10);
        EBook eBook = new EBook("Объектно-ориентированное программирование");
        RareBook rareBook = new RareBook("Защита компьютерной информации");

        IBook[] books = { paperBook, eBook, rareBook };

        // Количество бумажных книг до выдачи
        Console.WriteLine($"Бумажных книг до выдачи: {paperBook.GetCopies()}");
        Console.WriteLine();

        for (int i = 0; i < books.Length; i++)
        {
            Console.WriteLine($"Книга: {books[i].Title()}");
            Console.WriteLine($"Доступна: {books[i].IsAvailable()}");

            if (books[i].Issue())
            {
                Console.WriteLine("Книга выдана");
            }
            else
            {
                Console.WriteLine("В выдаче отказано");
            }

            Console.WriteLine();
        }

        // Количество бумажных книг после выдачи
        Console.WriteLine($"Бумажных книг после выдачи: {paperBook.GetCopies()}");
    }
}
