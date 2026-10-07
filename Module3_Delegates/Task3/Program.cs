using System;
using System.Collections.Generic;

delegate void TaskAction(string taskName);

class TaskItem
{
    private string name;
    private TaskAction action;

    public TaskItem(string name, TaskAction action)
    {
        this.name = name;
        this.action = action;
    }

    public void Execute()
    {
        action(name);
    }

    public string GetName()
    {
        return name;
    }
}


class Program
{
    static void SendNotification(string taskName)
    {
        Console.WriteLine($"[Уведомление] Выполнена задача: {taskName}");
    }

    static void LogToJournal(string taskName)
    {
        Console.WriteLine($"[Журнал] Запись о задаче: {taskName}");
    }


    static void Main()
    {
        List<TaskItem> tasks = new List<TaskItem>();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Добавить задачу с уведомлением");
            Console.WriteLine("2 - Добавить задачу с записью в журнал");
            Console.WriteLine("3 - Выполнить все задачи");
            Console.WriteLine("4 - Показать все задачи");
            Console.WriteLine("0 - Выход");

            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break;
            }

            else if (choice == "1")
            {
                Console.Write("Введите название задачи: ");
                string taskName = Console.ReadLine();
                tasks.Add(new TaskItem(taskName, SendNotification));
                Console.WriteLine("Задача добавлена");
            }

            else if (choice == "2")
            {
                Console.Write("Введите название задачи: ");
                string taskName = Console.ReadLine();
                tasks.Add(new TaskItem(taskName, LogToJournal));
                Console.WriteLine("Задача добавлена");
            }

            else if (choice == "3")
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Список задач пуст");
                }
                else
                {
                    foreach (TaskItem t in tasks)
                    {
                        t.Execute();
                    }
                }
            }

            else if (choice == "4")
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Список задач пуст");
                }

                else
                {
                    Console.WriteLine("Список всех задач:");
                    foreach (TaskItem t in tasks)
                    {
                        Console.WriteLine($"- {t.GetName()}");
                    }
                }
            }

            else
            {
                Console.WriteLine("Неверный пункт меню");
            }
        }
    }
}