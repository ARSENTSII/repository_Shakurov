using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Task1
{
    public class StudentRepository
    {
        private List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            int maxId = 0;
            foreach (Student s in students)
            {
                if (s.Id > maxId)
                {
                    maxId = s.Id;
                }
            }

            student.Id = maxId + 1;
            students.Add(student);
        }

        public bool Remove(int id)
        {
            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    students.Remove(s);
                    return true;
                }                    
            }

            return false;
        }

        public List<Student> GetAll()
        {
            return students; 
        }

        public bool Edit(int id, string name, string group, int course, double grade)
        {
            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    s.Name = name;
                    s.Group = group;
                    s.Course = course;
                    s.AverageGrade = grade;
                    return true;
                }
            }

            return false;
        }

        public List<Student> FindByName(string text)
        {
            List<Student> result = new List<Student>();

            foreach (Student s in students)
            {
                if(s.Name.Contains(text, StringComparison.OrdinalIgnoreCase)) // если имя содержит text без учёта регистра, добавить s в result
                {
                    result.Add(s);
                }
            }

            return result;
        }

        public List<Student> FindByGroup(string group)
        {
            List<Student> result = new List<Student>();

            foreach (Student s in students)
            {
                if (s.Name.Contains(group, StringComparison.OrdinalIgnoreCase)) 
                {
                    result.Add(s);
                }
            }

            return result;
        }

        public List<Student> FindByCourse(int course)
        {
            List<Student> result = new List<Student>();

            foreach (Student s in students)
            {
                if (s.Course == course)
                {
                    result.Add(s);
                }
            }

            return result;
        }
    }
}
