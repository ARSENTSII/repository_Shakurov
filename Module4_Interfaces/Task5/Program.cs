using System;

interface IPicture
{
    void DrawLine();

    void DrawCircle();

    void DrawRect();
}

class Canvas : IPicture
{
    public void DrawLine()
    {
        Console.WriteLine("Отрисована линия (-)");
    }

    public void DrawCircle()
    {
        Console.WriteLine("Отрисован круг (О)");
    }

    public void DrawRect()
    {
        Console.WriteLine("Отрисован прямоугольник ([ ])");
    }
}

class Program
{
    static void Main()
    {
        Canvas canvas = new Canvas();

        canvas.DrawLine();
        canvas.DrawCircle();
        canvas.DrawRect();
    }
}