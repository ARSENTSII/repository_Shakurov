using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProjectStatus Status { get; set; }

        public string StatusText
        {
            get { return Task2.Services.EnumConverter.ProjectStatusToDb(Status); }
        }
    }
}
