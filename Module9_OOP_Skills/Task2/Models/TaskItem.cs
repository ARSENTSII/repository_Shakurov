using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int? EmployeeId { get; set; }

        public TaskState Status {  get; set; }

        public TaskPriority Priority { get; set; }

        private int _progress;

        public int Progress
        {
            get { return _progress; }
            set
            {
                if (value < 0)
                {
                    _progress = 0;
                }
                else if (value > 100)
                {
                    _progress = 100;
                }
                else
                {
                    _progress = value;
                }
            }
        }

        public DateTime? Deadline {  get; set; }

        public string ProjectName { get; set; }
        public string EmployeeName { get; set; }

        public string StatusText
        {
            get { return Task2.Services.EnumConverter.TaskStateToDb(Status); }
        }

        public string PriorityText
        {
            get { return Task2.Services.EnumConverter.TaskPriorityToDb(Priority); }
        }

        public const int DueSoonDays = 3;

        public bool IsOverdue
        {
            get
            {
                return Deadline.HasValue && Deadline.Value.Date < DateTime.Today
                       && Status != TaskState.Done;
            }
        }

        public bool IsDueSoonNow
        {
            get
            {
                return Deadline.HasValue && Status != TaskState.Done
                       && Deadline.Value.Date >= DateTime.Today
                       && Deadline.Value.Date <= DateTime.Today.AddDays(DueSoonDays);
            }
        }
    }
}
