using System;

class Program 
{
    static void Main()
    {
        IDrawable[] shapes = { new Circle(5), new Rectangle(4, 6), new Triangle(5,5,4) }; //Массив объектов

        for (int i = 0; i < shapes.Length; i++) //вызов метода для каждого объекта
        {
            shapes[i].Draw();
        }
    }
}
interface IDrawable //interface IDrawable
{
    void Draw(); //Метод интерфейса
}

class Circle: IDrawable //Класс круг
{
    private double radius; //Поле радиус 

    public Circle(double r) //Метод для установки значения
    {
        radius = r;
    }

    public void Draw() //Метод интерфейса внутри класса Circle
    {
        Console.WriteLine($"Рисуется круг с радиусом: {radius}");
    }
}

class Rectangle: IDrawable
{
    private double height;
    private double weight;

    public Rectangle(double h, double w)
    {
        height = h;
        weight = w;
    }

    public void Draw() 
    {
        Console.WriteLine($"Рисуется прямоугольник с высотой {height} и длинной {weight}");
    }
}

class Triangle: IDrawable
{
    private double firstside;

    private double secondside;

    private double thirdside;

    public Triangle(double a, double b, double c)
    {
        firstside = a; secondside = b; thirdside = c;
    }

    public void Draw()
    {
        Console.WriteLine($"Рисуется треугольник со сторонами {firstside}, {secondside} и {thirdside}");
    }
}
