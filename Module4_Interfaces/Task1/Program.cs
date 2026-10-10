using System;

interface IFigure
{
    double Square();

    double Perimeter();
}

class Circle: IFigure
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public double Square()
    {
        return Math.PI * (radius * radius);
    }

    public double Perimeter()
    {
        return 2 * Math.PI * radius;
    }
}

class Rectangle : IFigure
{
    private double length;
    private double width;

    public Rectangle(double length, double width)
    {
        this.length = length;
        this.width = width;
    }

    public double Square()
    {
        return length * width;
    }

    public double Perimeter()
    {
        return 2 * (length + width);
    }
}

class Triangle : IFigure
{
    private double a;

    private double b;

    private double c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double Square()
    {
        //По формуле Герона
        double p = (a + b + c) / 2;                       // полупериметр
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public double Perimeter()
    {
        return a + b + c;
    }
}

class Program
{
    static void Main() 
    {
        Circle circle1 = new Circle(4);
        Rectangle rectangle1 = new Rectangle(4, 8);
        Triangle triangle1 = new Triangle(3, 4, 5);

        Console.WriteLine($"Круг c площадью {circle1.Square():F2} и периметром {circle1.Perimeter():F2}");
        Console.WriteLine($"Прямоугольник с площадью {rectangle1.Square():F2} и периметром {rectangle1.Perimeter():F2}");
        Console.WriteLine($"Треугольник с площадью {triangle1.Square():F2} и периметром {triangle1.Perimeter():F2}");
    }
}
