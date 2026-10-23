using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class Employee : BaseEntity
    {
        public string FullName {  get; set; }

        public string Position {  get; set; }
    }
}
