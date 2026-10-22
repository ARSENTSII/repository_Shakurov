using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ContactsApp
{
    internal class ContactRepository
    {
        string connString = "Data Source=phonebook.db";

        public ContactRepository()
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Contacts (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PhoneNumber TEXT,
                    Name TEXT NOT NULL);";
                command.ExecuteNonQuery();
            }
        }

        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (var connection = new SQLiteConnection(connString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Contacts";
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contact contact = new Contact();

                        contact.Id = Convert.ToInt32(reader["Id"]);
                        contact.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                        contact.Name = Convert.ToString(reader["Name"]);

                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }

        public void Add(Contact contact)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Contacts (PhoneNumber, Name)" +
                    "Values(@p, @n)";

                command.Parameters.AddWithValue("@p", contact.PhoneNumber);
                command.Parameters.AddWithValue("@n", contact.Name);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Contact contact)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Contacts WHERE Id = @id";

                command.Parameters.AddWithValue("@id", contact.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Contact contact)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Contacts SET PhoneNumber=@p, Name=@n WHERE Id = @id";

                command.Parameters.AddWithValue("@p", contact.PhoneNumber);
                command.Parameters.AddWithValue("@n", contact.Name);
                command.Parameters.AddWithValue("@id", contact.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
