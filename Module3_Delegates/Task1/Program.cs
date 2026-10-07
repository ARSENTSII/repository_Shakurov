using System;

class Figure
{
    public virtual double Area()
    {
        return 0;
    }
}

class Circle : Figure
{
    private double radius;

    public Circle (double r)
    {
        radius = r;
    }

    public override double Area()
    {
        return Math.PI * radius * radius;        
    }
}

class Rectangle : Figure
{
    private double length;

    private double width;

    public Rectangle(double l, double w)
    {
        length = l; width = w;
    }

    public override double Area()
    {
        return length * width;
    }
}

class Triangle : Figure
{
    private double osnova;

    private double height;

    public Triangle(double a, double h)
    {
        osnova = a; height = h;
    }

    public override double Area()
    {
        return (osnova * height) / 2;
    }
}

delegate double AreaCalculator();

class Program
{
    static void Main()
    {
        Circle circle = new Circle(5);
        Rectangle rectangle = new Rectangle(4, 6);
        Triangle triangle = new Triangle(15, 20);

        AreaCalculator calculator = circle.Area;
        Console.WriteLine($"Площадь круга: {calculator():F2}");

        calculator = rectangle.Area;
        Console.WriteLine($"Площадь прямоугольника: {calculator():F2}");

        calculator = triangle.Area;
        Console.WriteLine($"Площадь треугольника: {calculator():F2}");
    }
}