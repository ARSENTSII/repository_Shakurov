using System;

class Shape
{
    //Геттеры
    public virtual double Area() 
    {
        return 0;
    }

    public virtual double Perimeter()
    {
        return 0;
    }
}
    class Circle: Shape
    {
        private double radius; //Поле класса Circle

        public Circle (double r) //Метод для установки значений (сеттер)
        {
            radius = r;
        }
        
        //Методы для получения значений (геттеры)
        public override double Area()
        {
            return Math.PI * radius * radius;
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    class Rectangle: Shape
    {   
        //Поля класса Rectangle
        private double width;
        private double height;

        public Rectangle(double w, double h) //Метод для установки значений (сеттер)
        {
            width = w;
            height = h;
        }

        //Методы для получения значений (геттеры)
        public override double Area() 
        {
            return width * height;
        }

        public override double Perimeter()
        {
                return 2 * (width + height);
        }
    }


    class Program
    {
        static void Main()
        {
            Shape circle = new Circle(5);
            Shape rectangle = new Rectangle(4, 6);

            Console.WriteLine($"Площадь круга: {circle.Area():F2}, Периметр круга: {circle.Perimeter():F2}");
            Console.WriteLine($"Площадь прямоугольника: {rectangle.Area():F2}, Периметр прямоугольника: {rectangle.Perimeter():F2}");
        }
    }
