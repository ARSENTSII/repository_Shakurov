using System;
using System.Collections.Generic; // подключаем List<T> для хранения коллекции книг

class Program
{
    static void Main()
    {
        // Создаём двух авторов
        Author author1 = new Author("Лев", "Толстой", 1828);
        Author author2 = new Author("Фёдор", "Достоевский", 1821);

        // Создаём три книги, связывая их с авторами через композицию
        Book book1 = new Book("Война и мир", author1, 1869);
        Book book2 = new Book("Анна Каренина", author1, 1877);
        Book book3 = new Book("Преступление и наказание", author2, 1866);

        // Создаём библиотеку и добавляем в неё книги
        Library library = new Library();
        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);

        // Выводим все книги библиотеки
        Console.WriteLine("Все книги в библиотеке:");
        library.PrintAllBooks();

        // Ищем книги, изданные в 1866 году
        Console.WriteLine();
        Console.WriteLine("Книги 1866 года издания:");
        List<Book> booksByYear = library.FindByYear(1866);
        foreach (Book b in booksByYear)
        {
            b.PrintInfo();
        }

        // Ищем книги конкретного автора
        Console.WriteLine();
        Console.WriteLine("Книги Льва Толстого:");
        List<Book> booksByAuthor = library.FindByAuthor("Лев Толстой");
        foreach (Book b in booksByAuthor)
        {
            b.PrintInfo();
        }

        // Удаляем книгу из библиотеки и проверяем, что список обновился
        Console.WriteLine();
        Console.WriteLine("Удаляем книгу 'Анна Каренина'");
        library.RemoveBook(book2);
        library.PrintAllBooks();
    }
}

// Класс Library управляет коллекцией книг:
// добавление, удаление и поиск по разным критериям
class Library
{
    private List<Book> books = new List<Book>(); // список книг, растёт и уменьшается автоматически

    // Добавляет книгу в библиотеку
    public void AddBook(Book book)
    {
        books.Add(book);
    }

    // Удаляет книгу из библиотеки
    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    // Выводит информацию обо всех книгах в библиотеке
    public void PrintAllBooks()
    {
        foreach (Book b in books)
        {
            b.PrintInfo();
        }
    }

    // Ищет все книги конкретного автора (по полному имени)
    public List<Book> FindByAuthor(string authorFullName)
    {
        List<Book> result = new List<Book>(); // сюда соберём найденные книги

        foreach (Book b in books)
        {
            if (b.GetAuthor().GetFullName() == authorFullName)
            {
                result.Add(b);
            }
        }

        return result;
    }

    // Ищет все книги, изданные в указанном году
    public List<Book> FindByYear(int year)
    {
        List<Book> result = new List<Book>(); // сюда соберём найденные книги

        foreach (Book b in books)
        {
            if (b.GetYear() == year)
            {
                result.Add(b);
            }
        }

        return result;
    }
}

// Класс Book хранит информацию о книге,
// включая объект автора (композиция)
class Book
{
    private string title;
    private Author author; // поле типа Author связывает книгу с её автором
    private int year;

    public Book(string title, Author author, int year)
    {
        this.title = title;
        this.author = author;
        this.year = year;
    }

    public string GetTitle()
    {
        return title;
    }

    public Author GetAuthor()
    {
        return author;
    }

    public int GetYear()
    {
        return year;
    }

    // Выводит полную информацию о книге, включая данные автора
    public void PrintInfo()
    {
        Console.WriteLine($"«{title}», {year} г., автор: {author.GetFullName()} ({author.GetBirthYear()} г.р.)");
    }
}

// Класс Author хранит информацию об авторе книги
class Author
{
    private string name;
    private string lastname;
    private int birthyear;

    public Author(string name, string lastname, int birthyear)
    {
        this.name = name;
        this.lastname = lastname;
        this.birthyear = birthyear;
    }

    // Возвращает имя и фамилию автора одной строкой
    public string GetFullName()
    {
        return name + " " + lastname;
    }

    public int GetBirthYear()
    {
        return birthyear;
    }
}