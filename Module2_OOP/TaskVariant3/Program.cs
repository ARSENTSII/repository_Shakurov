using System;

class Program
{
    static void Main()
    {
        //Объект класса 
        Employee employee1 = new Employee("Арсентий", 18, "Junior", 1000);

        Console.WriteLine($"Имя сотрудника: {employee1.GetName()}, Возраст сотрудника: {employee1.GetAge()}, Должность сотрудника: {employee1.GetPosition()}, Зарплата сотрудника: {employee1.GetSalary()}р");
        Console.WriteLine($"Годовой доход: {employee1.GetSalaryPerYear()}р");

        employee1.SetSalary(1500);
        Console.WriteLine($"После повышения: {employee1.GetSalary()}р, годовой доход: {employee1.GetSalaryPerYear()}р");
    }
}

class Employee //Класс сотрудник 
{
    //Поля
    private string name;

    private int age;

    private string position;

    private int salary;

    public Employee(string name, int age, string position, int salary) //Инициализация объектов класса (конструктор)
    {
        this.name = name;
        this.age = age;
        this.position = position;
        this.salary = salary;
    }

    //Сеттеры
    public void SetName(string newName)
    {
        name = newName;
    }

    public void SetAge(int newAge)
    {
        age = newAge;
    }

    public void SetPosition(string newPosition)
    {
        position = newPosition;
    }

    public void SetSalary(int newSalary)
    {
        salary = newSalary;
    }

    //Геттеры
    public int GetSalaryPerYear()
    {
        return salary * 12;
    }

    public int GetSalary()
    {
        return salary;
    }

    public string GetPosition()
    {
        return position;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetName()
    {
        return name;
    }
}
