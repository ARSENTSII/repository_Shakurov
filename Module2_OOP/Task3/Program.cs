using System;

class Author 
{
    //Поля класса Author
    private string name;
    private int birth;

    public Author(string name, int birth)
    {
        this.name = name;
        this.birth = birth;
    }

    public string GetName()
    {
        return name;
    }

    public int GetBirth()
    {
        return birth;
    }
}

class Book
{
    private string title;
    private int age;
    private Author author; //Композиция

    public Book(string title, int age, Author author)
    {
        this.title = title;
        this.age = age;
        this.author = author;
    }

    public string GetTitle()
    {
        return title;
    }

    public int GetAge()
    {
        return age;
    }

    public Author GetAuthor()
    {
        return author;
    }
    
    //Метод для вывода информации
    public void PrintInfo()
    {
        Console.WriteLine($"Книга: {title}, год издания: {age}, автор: {author.GetName()} ({author.GetBirth()} г.р.)");
    }
}

class Program
{
    static void Main()
    {
        Author author1 = new Author("Лев Толстой", 1828);
        Book book1 = new Book("Война и мир", 1869, author1);

        Author author2 = new Author("Фёдор Достоевский", 1821);
        Book book2 = new Book("Преступление и наказание", 1866, author2);

        book1.PrintInfo();
        book2.PrintInfo();
    }
}
