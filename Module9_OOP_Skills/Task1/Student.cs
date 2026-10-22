using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Group { get; set; } = "";
        public int Course { get; set; }
        public double AverageGrade { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name}, группа {Group}, курс {Course}, балл {AverageGrade:F2}";
        }
    }

}
