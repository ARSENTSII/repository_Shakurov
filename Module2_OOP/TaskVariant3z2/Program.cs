using System;

class Program
{
    // Метод создаёт два объекта Simple и сразу завершает работу.
    // Как только метод закончится, obj1 и obj2 выходят из области видимости,
    // и становятся доступны для сборки мусора
    static void CreateAndDropObjects()
    {
        Simple obj1 = new Simple("Привет", "Мир");  // объект через конструктор с параметрами
        Simple obj2 = new Simple();                 // объект через конструктор по умолчанию

        Console.WriteLine("Объекты созданы");
    }

    static void Main()
    {
        // Вызываем метод — внутри него создаются и "теряются" объекты
        CreateAndDropObjects();

        // Принудительно запускаем сборщик мусора,
        // чтобы он нашёл и удалил ставшие ненужными объекты
        GC.Collect();

        // Ждём, пока все деструкторы (финализаторы) успеют отработать
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Конец программы");
    }
}

// Класс с двумя полями и двумя конструкторами —
// демонстрирует работу конструкторов и деструктора
class Simple
{
    private string first;
    private string second;

    // Деструктор — вызывается автоматически сборщиком мусора,
    // когда объект больше не используется
    ~Simple()
    {
        Console.WriteLine($"{first}, {second} был удален");
    }

    // Конструктор с входными параметрами
    public Simple(string first, string second)
    {
        this.first = first;
        this.second = second;
    }

    // Конструктор по умолчанию — задаёт поля стандартными значениями,
    // если параметры не переданы
    public Simple()
    {
        first = "Первый объект";
        second = "Второй объект";
    }
}