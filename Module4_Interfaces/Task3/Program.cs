using System;

interface IStudent
{
    double AvrGrade();

    string InfCourse();

    string Name();
}

class FYStudent : IStudent
{
    private static Random rnd = new Random();  

    private string name;
    private double[] grades;

    public FYStudent(string name, int gradesCount)
    {
        this.name = name;
        grades = new double[gradesCount];

        for (int i = 0; i < gradesCount; i++)
        {
            grades[i] = rnd.Next(4, 11); 
        }
    }

    public double AvrGrade()
    {
        double sum = 0;

        for (int i = 0; i < grades.Length; i++)
        {
            sum += grades[i]; 
        }

        return sum / grades.Length;
    }

    public string Name()
    {
        return name;
    }

    public string InfCourse()
    {
        return $"{name} учится на 1 курсе";
    }
}

class TYStudent : IStudent
{
    private static Random rnd = new Random();  

    private string name;
    private double[] grades;

    public TYStudent(string name, int gradesCount)
    {
        this.name = name;
        grades = new double[gradesCount];

        for (int i = 0; i < gradesCount; i++)
        {
            grades[i] = rnd.Next(4, 11);
        }
    }

    public double AvrGrade()
    {
        double sum = 0;

        for (int i = 0; i < grades.Length; i++)
        {
            sum += grades[i]; 
        }

        return sum / grades.Length;   
    }

    public string Name()
    {
        return name;
    }

    public string InfCourse()
    {
        return $"{name} учится на 3 курсе";
    }
}

class Program
{
    static void Main()
    {
        FYStudent fYStudent = new FYStudent("Вася", 10);
        TYStudent tYStudent = new TYStudent("Вова", 10);

        Console.WriteLine($"{"Средний балл",-10}{"Имя",10}{"Какой курс",12}");
        Console.WriteLine(new string('-', 42));

        Console.WriteLine($"{fYStudent.AvrGrade(), -18:F2} {fYStudent.InfCourse()}");
        Console.WriteLine($"{tYStudent.AvrGrade(), -18:F2} {tYStudent.InfCourse()}");
    }
}
