using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Services
{
    public class NotificationService
    {
        private readonly HashSet<int> _notified = new HashSet<int>();

        // возвращает текст уведомления или null, если сообщать не о чем
        public string BuildMessage(List<TaskItem> tasks)
        {
            var overdue = new List<TaskItem>();
            var soon = new List<TaskItem>();

            foreach (var t in tasks)
            {
                if (_notified.Contains(t.Id) == true)
                {
                    continue; // о ней уже предупреждали, пропускаем
                }

                if (t.IsOverdue == true)
                {
                    overdue.Add(t);
                }
                else if (t.IsDueSoonNow == true)
                {
                    soon.Add(t);
                }
            }

            if (overdue.Count == 0 && soon.Count == 0)
            {
                return null;
            }

            var sb = new StringBuilder();

            if (overdue.Count > 0)
            {
                sb.AppendLine("ПРОСРОЧЕНО:");
                foreach (var t in overdue)
                {
                    sb.AppendLine(" • " + t.Title + " (срок " + t.Deadline.Value.ToString("dd.MM.yyyy") + ")");
                    _notified.Add(t.Id);
                }
                sb.AppendLine();
            }

            if (soon.Count > 0)
            {
                sb.AppendLine("Срок в ближайшие " + TaskItem.DueSoonDays + " дня:");
                foreach (var t in soon)
                {
                    sb.AppendLine(" • " + t.Title + " (срок " + t.Deadline.Value.ToString("dd.MM.yyyy") + ")");
                    _notified.Add(t.Id);
                }
            }

            return sb.ToString();
        }
    }
}
