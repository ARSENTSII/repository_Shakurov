using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Services;
using Task2.Data;
using Task2.Models;

namespace Task2.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        public void Add(Project item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO Projects (Name, Description, StartDate, EndDate, Status) VALUES (@name, @desc, @start, @end, @status)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@desc", item.Description);
                    cmd.Parameters.AddWithValue("@start", item.StartDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@end", item.EndDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", EnumConverter.ProjectStatusToDb(item.Status));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Projects WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Project> GetAll()
        {
            var list = new List<Project>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, Name, Description, StartDate, EndDate, Status FROM Projects", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Project project = new Project();
                        project.Id = reader.GetInt32(0);
                        project.Name = reader.GetString(1);
                        if (reader.IsDBNull(2) == true)
                        {
                            project.Description = "";
                        }
                        else
                        {
                            project.Description = reader.GetString(2);
                        }
                        if (reader.IsDBNull(3) == true)
                        {
                            project.StartDate = DateTime.Today;
                        }
                        else
                        {
                            project.StartDate = DateTime.Parse(reader.GetString(3));
                        }

                        if (reader.IsDBNull(4) == true)
                        {
                            project.EndDate = DateTime.Today;
                        }
                        else
                        {
                            project.EndDate = DateTime.Parse(reader.GetString(4));
                        }

                        if (reader.IsDBNull(5) == true)
                        {
                            project.Status = ProjectStatus.Active;
                        }
                        else
                        {
                            project.Status = EnumConverter.ProjectStatusFromDb(reader.GetString(5));
                        }
                        list.Add(project);
                    }
                }
            }
            return list;
        }

        public Project GetById(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, Name, Description, StartDate, EndDate, Status FROM Projects WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Project project = new Project();
                            project.Id = reader.GetInt32(0);
                            project.Name = reader.GetString(1);
                            if (reader.IsDBNull(2) == true)
                            {
                                project.Description = "";
                            }
                            else
                            {
                                project.Description = reader.GetString(2);
                            }
                            if (reader.IsDBNull(3) == true)
                            {
                                project.StartDate = DateTime.Today;
                            }
                            else
                            {
                                project.StartDate = DateTime.Parse(reader.GetString(3));
                            }

                            if (reader.IsDBNull(4) == true)
                            {
                                project.EndDate = DateTime.Today;
                            }
                            else
                            {
                                project.EndDate = DateTime.Parse(reader.GetString(4));
                            }

                            if (reader.IsDBNull(5) == true)
                            {
                                project.Status = ProjectStatus.Active;
                            }
                            else
                            {
                                project.Status = EnumConverter.ProjectStatusFromDb(reader.GetString(5));
                            }
                            return project;
                        }
                    }

                }
            }
            return null;
        }

        public void Update(Project item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("UPDATE Projects SET Name = @name, Description = @desc, StartDate = @start, EndDate = @end, Status = @status WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@desc", item.Description);
                    cmd.Parameters.AddWithValue("@start", item.StartDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@end", item.EndDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", EnumConverter.ProjectStatusToDb(item.Status));
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
