using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Data;
using Task2.Services;
using Task2.Models;

namespace Task2.Repositories
{
    public class TaskRepository : IRepository<TaskItem>
    {
        public void Add(TaskItem item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO Tasks (Title, Description, ProjectId, EmployeeId, Status, Priority, Progress, Deadline) VALUES (@title, @desc, @project, @employee, @status, @priority, @progress, @deadline)", conn))
                {
                    cmd.Parameters.AddWithValue("@title", item.Title);
                    cmd.Parameters.AddWithValue("@desc", item.Description);
                    cmd.Parameters.AddWithValue("@project", item.ProjectId);
                    if (item.EmployeeId.HasValue == true)
                    {
                        cmd.Parameters.AddWithValue("@employee", item.EmployeeId.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@employee", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@status", EnumConverter.TaskStateToDb(item.Status));
                    cmd.Parameters.AddWithValue("@priority", EnumConverter.TaskPriorityToDb(item.Priority));
                    cmd.Parameters.AddWithValue("@progress", item.Progress);
                    if (item.Deadline.HasValue == true)
                    {
                        cmd.Parameters.AddWithValue("@deadline", item.Deadline.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deadline", DBNull.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Tasks WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TaskItem> GetAll()
        {
            var list = new List<TaskItem>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, Title, Description, ProjectId, EmployeeId, Status, Priority, Progress, Deadline FROM Tasks", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TaskItem task = new TaskItem();
                        task.Id = reader.GetInt32(0);
                        task.Title = reader.GetString(1);
                        if (reader.IsDBNull(2) == true)
                        {
                            task.Description = "";
                        }
                        else
                        {
                            task.Description = reader.GetString(2);
                        }

                        task.ProjectId = reader.GetInt32(3);

                        if (reader.IsDBNull(4) == true)
                        {
                            task.EmployeeId = null;
                        }
                        else
                        {
                            task.EmployeeId = reader.GetInt32(4);
                        }

                        task.Status = EnumConverter.TaskStateFromDb(reader.GetString(5));
                        task.Priority = EnumConverter.TaskPriorityFromDb(reader.GetString(6));
                        task.Progress = reader.GetInt32(7);

                        if (reader.IsDBNull(8) == true)
                        {
                            task.Deadline = null;
                        }
                        else
                        {
                            task.Deadline = DateTime.Parse(reader.GetString(8));
                        }

                        list.Add(task);
                    }
                }
            }
            return list;
        }

        public TaskItem GetById(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, Title, Description, ProjectId, EmployeeId, Status, Priority, Progress, Deadline FROM Tasks WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TaskItem task = new TaskItem();
                            task.Id = reader.GetInt32(0);
                            task.Title = reader.GetString(1);

                            if (reader.IsDBNull(2) == true)
                            {
                                task.Description = "";
                            }
                            else
                            {
                                task.Description = reader.GetString(2);
                            }

                            task.ProjectId = reader.GetInt32(3);

                            if (reader.IsDBNull(4) == true)
                            {
                                task.EmployeeId = null;
                            }
                            else
                            {
                                task.EmployeeId = reader.GetInt32(4);
                            }

                            task.Status = EnumConverter.TaskStateFromDb(reader.GetString(5));
                            task.Priority = EnumConverter.TaskPriorityFromDb(reader.GetString(6));
                            task.Progress = reader.GetInt32(7);

                            if (reader.IsDBNull(8) == true)
                            {
                                task.Deadline = null;
                            }
                            else
                            {
                                task.Deadline = DateTime.Parse(reader.GetString(8));
                            }

                            return task;
                        }
                    }
                }
            }
            return null;
        }

        public void Update(TaskItem item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("UPDATE Tasks SET Title = @title, Description = @desc, ProjectId = @project, EmployeeId = @employee, Status = @status, Priority = @priority, Progress = @progress, Deadline = @deadline WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@title", item.Title);
                    cmd.Parameters.AddWithValue("@desc", item.Description);
                    cmd.Parameters.AddWithValue("@project", item.ProjectId);

                    if (item.EmployeeId.HasValue == true)
                    {
                        cmd.Parameters.AddWithValue("@employee", item.EmployeeId.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@employee", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@status", EnumConverter.TaskStateToDb(item.Status));
                    cmd.Parameters.AddWithValue("@priority", EnumConverter.TaskPriorityToDb(item.Priority));
                    cmd.Parameters.AddWithValue("@progress", item.Progress);

                    if (item.Deadline.HasValue == true)
                    {
                        cmd.Parameters.AddWithValue("@deadline", item.Deadline.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deadline", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
