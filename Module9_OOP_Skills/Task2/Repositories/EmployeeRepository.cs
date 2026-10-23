using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task2.Data;
using Task2.Models;

namespace Task2.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public void Add(Employee item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO Employees (FullName, Position) VALUES (@name, @pos)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", item.FullName);
                    cmd.Parameters.AddWithValue("@pos", item.Position);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Employees WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Employee> GetAll()
        {
            var list = new List<Employee>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, FullName, Position FROM Employees", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = reader.GetInt32(0);
                        employee.FullName = reader.GetString(1);
                        if (reader.IsDBNull(2) == true)
                        {
                            employee.Position = "";
                        }
                        else
                        {
                            employee.Position = reader.GetString(2);
                        }
                        list.Add(employee);
                    }
                }
            }
            return list;
        }

        public Employee GetById(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, FullName, Position FROM Employees WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Employee employee = new Employee();
                            employee.Id = reader.GetInt32(0);
                            employee.FullName = reader.GetString(1);
                            if (reader.IsDBNull(2) == true)
                            {
                                employee.Position = "";
                            }
                            else
                            {
                                employee.Position = reader.GetString(2);
                            }
                            return employee;
                        }
                    }
                    
                }
            }
            return null;
        }

        public void Update(Employee item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("UPDATE Employees SET FullName = @name, Position = @pos WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", item.FullName);
                    cmd.Parameters.AddWithValue("@pos", item.Position);
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
