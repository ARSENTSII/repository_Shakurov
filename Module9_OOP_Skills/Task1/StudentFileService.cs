using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Task1
{
    public class StudentFileService
    {
        private readonly string filePath = "students.json";

        public void Save(List<Student> students)
        {
            string json = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);

            Console.WriteLine("Список студентов сохранён!");
        }

        public List<Student> Load()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл пока не существует.");
                return new List<Student>();
            }

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Student>>(json)
                   ?? new List<Student>();
        }
    }
}
