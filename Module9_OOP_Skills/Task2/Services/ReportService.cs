using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;
using Task2.Repositories;

namespace Task2.Services
{
    public class ProjectReportRow
    {
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public int TotalTasks { get; set; }
        public int NewTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int DoneTasks { get; set; }
        public int OverdueTasks { get; set; }
        public int ProgressPercent { get; set; }
    }

    public class EmployeeReportRow
    {
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public int TotalTasks { get; set; }
        public int DoneTasks { get; set; }
        public int OverdueTasks { get; set; }
    }

    public class ReportService
    {

        private ProjectRepository _projectRepo = new ProjectRepository();
        private TaskRepository _taskRepo = new TaskRepository();
        private EmployeeRepository _employeeRepo = new EmployeeRepository();

        private static bool IsOverdue(TaskItem t)
        {
            return t.Deadline.HasValue
                && t.Deadline.Value.Date < DateTime.Today
                && t.Status != TaskState.Done;
        }

        public List<ProjectReportRow> GetProjectReport()
        {
            var projects = _projectRepo.GetAll();
            var tasks = _taskRepo.GetAll();

            return projects.Select(p =>
            {
                var pt = tasks.Where(t => t.ProjectId == p.Id).ToList();
                return new ProjectReportRow
                {
                    ProjectName = p.Name,
                    Status = EnumConverter.ProjectStatusToDb(p.Status),
                    TotalTasks = pt.Count,
                    NewTasks = pt.Count(t => t.Status == TaskState.New),
                    InProgressTasks = pt.Count(t => t.Status == TaskState.InProgress),
                    DoneTasks = pt.Count(t => t.Status == TaskState.Done),
                    OverdueTasks = pt.Count(t => IsOverdue(t)),
                    ProgressPercent = pt.Count == 0 ? 0 : (int)Math.Round(pt.Average(t => t.Progress))
                };
            }).ToList();
        }

        public List<EmployeeReportRow> GetEmployeeReport()
        {
            var employees = _employeeRepo.GetAll();
            var tasks = _taskRepo.GetAll();

            var rows = employees.Select(e =>
            {
                var et = tasks.Where(t => t.EmployeeId == e.Id).ToList();
                return new EmployeeReportRow
                {
                    EmployeeName = e.FullName,
                    Position = e.Position,
                    TotalTasks = et.Count,
                    DoneTasks = et.Count(t => t.Status == TaskState.Done),
                    OverdueTasks = et.Count(t => IsOverdue(t))
                };
            }).ToList();

            var unassigned = tasks.Where(t => t.EmployeeId == null).ToList();
            if (unassigned.Count > 0)
            {
                rows.Add(new EmployeeReportRow
                {
                    EmployeeName = "Не назначены",
                    Position = "",
                    TotalTasks = unassigned.Count,
                    DoneTasks = unassigned.Count(t => t.Status == TaskState.Done),
                    OverdueTasks = unassigned.Count(t => IsOverdue(t))
                });
            }

            return rows;
        }

        public string GetSummary()
        {
            var tasks = _taskRepo.GetAll();
            int projects = _projectRepo.GetAll().Count;
            int done = tasks.Count(t => t.Status == TaskState.Done);
            int overdue = tasks.Count(t => IsOverdue(t));

            return "Проектов: " + projects + "   Задач: " + tasks.Count +
                   "   Выполнено: " + done + "   Просрочено: " + overdue;
        }

        private static string Esc(string s)
        {
            if (s == null) return "";
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }

        public void SaveCsv(string path, List<ProjectReportRow> projects, List<EmployeeReportRow> employees)
        {
            // UTF-8 с BOM и разделитель ';' - так русский Excel открывает файл правильно
            using (var w = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                w.WriteLine("Отчёт от " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                w.WriteLine();
                w.WriteLine("ПО ПРОЕКТАМ");
                w.WriteLine("Проект;Статус;Всего;Новые;В работе;Выполнено;Просрочено;Прогресс %");
                foreach (var r in projects)
                {
                    w.WriteLine(string.Join(";", Esc(r.ProjectName), Esc(r.Status), r.TotalTasks,
                        r.NewTasks, r.InProgressTasks, r.DoneTasks, r.OverdueTasks, r.ProgressPercent));
                }

                w.WriteLine();
                w.WriteLine("ПО СОТРУДНИКАМ");
                w.WriteLine("Сотрудник;Должность;Всего задач;Выполнено;Просрочено");
                foreach (var r in employees)
                {
                    w.WriteLine(string.Join(";", Esc(r.EmployeeName), Esc(r.Position),
                        r.TotalTasks, r.DoneTasks, r.OverdueTasks));
                }
            }
        }
    }
}
