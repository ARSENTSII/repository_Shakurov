using System;

class Person
{
    //Поля класса Person
    private string name;
    private int age;
    private string address;

    //Методы для установки значений (сеттера)
    public void SetName(string newName)
    {
        name = newName;
    }

    public void SetAge(int newAge)
    {
        age = newAge;
    }

    public void SetAddress(string newAddress)
    {
        address = newAddress;
    }

    //Методы для получения значений (геттеры) 
    public string GetName()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetAddress()
    {
        return address;
    }
}


class Program
{
    static void Main()
    {
        Person person1 = new Person(); //Объект класса Person

        //Вызов методов 
        person1.SetName("Арсентий");
        person1.SetAge(18);
        person1.SetAddress("Орша");

        Console.WriteLine($"Имя: {person1.GetName()}, Возрост: {person1.GetAge()}, Адрес: {person1.GetAddress()}");
    }
}
