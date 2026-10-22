using System;
using System.Runtime.CompilerServices;
using Task1;

class Program
{
    static void PrintList(List<Student> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Ничего не найдено");
        }
        else
        {
            foreach (Student s in list)
            {
                Console.WriteLine(s);
            }
        }
    }

    static void Main()
    {
        StudentRepository manager = new StudentRepository();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Добавить студента");
            Console.WriteLine("2 - Удалить студента");
            Console.WriteLine("3 - Показать всех");
            Console.WriteLine("4 - Редактировать студента");
            Console.WriteLine("5 - Поиск по имени");
            Console.WriteLine("0 - Выход");

            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break;
            }
            else if (choice == "1")
            {
                Console.Write("Имя: ");
                string name = Console.ReadLine();

                Console.Write("Группа: ");
                string group = Console.ReadLine();

                Console.Write("Курс: ");
                string courseText = Console.ReadLine();

                Console.Write("Средний балл: ");
                string gradeText = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name)
        && int.TryParse(courseText, out int course)
        && double.TryParse(gradeText, out double grade))
                {
                    manager.Add(new Student { Name = name, Group = group, Course = course, AverageGrade = grade });
                    Console.WriteLine("Студент добавлен");
                }

                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Введите Id: ");
                string idText = Console.ReadLine();

                if (int.TryParse(idText, out int id))
                {
                    if (manager.Remove(id))
                    {
                        Console.WriteLine("Студент удалён");
                    }

                    else
                    {
                        Console.WriteLine("Студент с таким Id не найден");
                    }
                }

                else
                {
                    Console.WriteLine("Введите число");
                }
            }
            else if (choice == "3")
            {
                foreach (Student s in manager.GetAll())
                {
                    Console.WriteLine(s);
                }
            }

            else if(choice == "4")
            {
                Console.Write("Введите Id студента: ");
                string idText = Console.ReadLine();

                if (!int.TryParse(idText, out int id))
                {
                    Console.WriteLine("Введите число");
                }
                else
                {
                    Console.Write("Новое имя: ");
                    string name = Console.ReadLine();

                    Console.Write("Новая группа: ");
                    string group = Console.ReadLine();

                    Console.Write("Новый курс: ");
                    string courseText = Console.ReadLine();

                    Console.Write("Новый средний балл: ");
                    string gradeText = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(name)
                        && int.TryParse(courseText, out int course)
                        && double.TryParse(gradeText, out double grade))
                    {
                        if (manager.Edit(id, name, group, course, grade))
                        {
                            Console.WriteLine("Студент изменён");
                        }
                        else
                        {
                            Console.WriteLine("Студент с таким Id не найден");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод");
                    }
                }
            }

            else if (choice == "5")
            {
                Console.WriteLine("Искать по:");
                Console.WriteLine("1 - имени");
                Console.WriteLine("2 - группе");
                Console.WriteLine("3 - курсу");

                string searchChoice = Console.ReadLine();

                if (searchChoice == "1")
                {
                    Console.Write("Введите имя или часть имени: ");
                    string text = Console.ReadLine();
                    PrintList(manager.FindByName(text));
                }
                else if (searchChoice == "2")
                {
                    Console.Write("Введите группу: ");
                    string group = Console.ReadLine();
                    PrintList(manager.FindByGroup(group));
                }
                else if (searchChoice == "3")
                {
                    Console.Write("Введите курс: ");
                    string courseText = Console.ReadLine();

                    if (int.TryParse(courseText, out int course))
                    {
                        PrintList(manager.FindByCourse(course));
                    }
                    else
                    {
                        Console.WriteLine("Введите число");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный пункт");
                }
            }

            else
            {
                Console.WriteLine("Неверный пункт меню");
            }
        }


    }
}

