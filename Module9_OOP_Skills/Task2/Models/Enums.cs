using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
        public enum TaskState
        {
            New,
            InProgress,
            Done
        }

        public enum TaskPriority
        {
            Low,
            Medium,
            High
        }

        public enum ProjectStatus
        {
            Active,
            Completed,
            OnHold
        }
}
