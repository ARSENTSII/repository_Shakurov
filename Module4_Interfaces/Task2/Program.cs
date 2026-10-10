using System;

interface IProduct
{
    double Cost();

    double Remains();

    string Name();

    double UnitPrice();   // цена за единицу (шт. или кг)
}

class Piece : IProduct //класс для штучных товаров
{
    private string name;

    private double price;

    private int count;

    public Piece(string name, double price, int count)
    {
        this.name = name;
        this.price = price;
        this.count = count;
    }

    public double Cost()
    {
        return price * count;
    }

    public double Remains()
    {
        return count;
    }

    public string Name()
    {
        return name;
    }

    public double UnitPrice()
    {
        return price;
    }
}

class Weight : IProduct //класс для весового товара
{
    private string name;

    private double pricePerKg;

    private double weight;

    public Weight(string name, double pricePerKg, double weight)
    {
        this.name = name;
        this.pricePerKg = pricePerKg;
        this.weight = weight;
    }

    public double Cost()
    {
        return pricePerKg * weight;
    }

    public double Remains()
    {
        return weight;
    }

    public string Name()
    {
        return name;
    }

    public double UnitPrice()
    {
        return pricePerKg;   // цена за килограмм
    }
}

class Discount : IProduct //класс для товара со скидкой
{
    private string name;

    private double price;

    private int count;

    private double discount;

    public Discount(string name, double price, int count, double discount)
    {
        this.name = name;
        this.price = price;
        this.count = count;
        this.discount = discount;
    }

    public double Cost()
    {
        return price * (1 - discount / 100.0) * count;
    }

    public double Remains()
    {
        return count;
    }

    public string Name()
    {
        return name;
    }

    public double UnitPrice()
    {
        return price * (1 - discount / 100.0);   // цена одной штуки уже со скидкой
    }
}

class Program
{
    static void Main()
    {
        Piece piece1 = new Piece("Тетрадь", 1.50, 120);
        Weight weight1 = new Weight("Яблоки", 3.50, 35.5);
        Discount discount1 = new Discount("Молоко", 2.80, 30, 15);

        Console.WriteLine($"{"Название",-10}{"Цена",10}{"Стоимость",12}{"Остаток",10}"); //слово «Название» в поле шириной 10 символов, прижато влево (минус)
        Console.WriteLine(new string('-', 42));

        Console.WriteLine($"{piece1.Name(),-10}{piece1.UnitPrice(),10:F2}{piece1.Cost(),12:F2}{piece1.Remains(),10}");
        Console.WriteLine($"{weight1.Name(),-10}{weight1.UnitPrice(),10:F2}{weight1.Cost(),12:F2}{weight1.Remains(),10}");
        Console.WriteLine($"{discount1.Name(),-10}{discount1.UnitPrice(),10:F2}{discount1.Cost(),12:F2}{discount1.Remains(),10}");

        Console.WriteLine();
        Console.WriteLine("Цена: за шт., у яблок за кг, у молока с учётом скидки");
        Console.WriteLine("Стоимость: цена за весь товар");
        Console.WriteLine("Остаток: количество товара на складе, у яблок - килограмм");
    }
}