using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Services
{
    public static class EnumConverter
    {
        public static string TaskStateToDb(TaskState state)
        {
            switch (state)
            {
                case TaskState.New:
                    return "Новая";
                case TaskState.InProgress:
                    return "В работе";
                case TaskState.Done:
                    return "Выполнена";
                
                default:
                    return "Новая";
            }
        }

        public static TaskState TaskStateFromDb(string text)
        {
            switch (text)
            {
                case "Новая":
                    return TaskState.New;
                case "В работе":
                    return TaskState.InProgress;
                case "Выполнена":
                    return TaskState.Done;
                
                default:
                    return TaskState.New;
            }
        }

        public static string TaskPriorityToDb(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.Low:
                    return "Низкий";
                case TaskPriority.Medium:
                    return "Средний";
                case TaskPriority.High:
                    return "Высокий";

                default:
                    return "Средний";
            }
        }

        public static TaskPriority TaskPriorityFromDb(string text) 
        {
            switch (text)
            {
                case "Низкий":
                    return TaskPriority.Low;
                case "Средний":
                    return TaskPriority.Medium;
                case "Высокий":
                    return TaskPriority.High;

                default:
                    return TaskPriority.Medium;
            }
        }

        public static string ProjectStatusToDb(ProjectStatus status)
        {
            switch (status)
            {
                case ProjectStatus.Active:
                    return "Активный";
                case ProjectStatus.Completed:
                    return "Завершён";
                case ProjectStatus.OnHold:
                    return "Приостановлен";

                default:
                    return "Активный";
            }
        }

        public static ProjectStatus ProjectStatusFromDb(string text)
        {
            switch (text)
            {
                case "Активный":
                    return ProjectStatus.Active;
                case "Завершён":
                    return ProjectStatus.Completed;
                case "Приостановлен":
                    return ProjectStatus.OnHold;

                default:
                    return ProjectStatus.Active;
            }
        }
    }
}
