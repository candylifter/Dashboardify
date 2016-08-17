using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;

namespace Dashboardify.Repositories
{
    public class UsersRepository
    {
        private string _connectionString = "Data Source=,;" +
                                            "Initial Catalog=DashBoardify;" +
                                            "User id=DashboardifyUser;" +
                                            "Password=123456;";
        private IList<User> _results;

        private DataTable _GetTableFromDB(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public UsersRepository()
        {
            string queryString = "SELECT - FROM dbo.Users";
            DataTable datatable = _GetTableFromDB(queryString);
            _results = new List<User>();

            foreach (DataRow dr in datatable.Rows)
            {
                User u = new User();

                u.UserId = (int)dr["UserId"];
                u.Name = (string)dr["Name"];
                u.Password = (string)dr["Password"];
                u.Email = (string)dr["Email"];
                u.IsActive = (bool)dr["IsActive"];
                u.Registered = (DateTime)dr["DateRegistered"];
                u.Modified = (DateTime)dr["DateModified"];

                _results.Add(u);
            }
        }

        public IList<User> GetList()
        {
            return _results.ToList();
        }

        public void Update(User user, string email, string password)
        {
            string query = @"UPDATE dbo.Users
                            SET Password=@User_Pass
                            SET Email=@User_Email
                            WHERE Id=@User_Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    Console.WriteLine("Opened connection to DB");
                    command.Parameters.AddWithValue("@User_Id", user.UserId);

                    if (!(string.IsNullOrEmpty(email)))
                    {
                        command.Parameters.AddWithValue("@User_Email", email);
                    } else {
                        command.Parameters.AddWithValue("@User_Email", user.Email);
                    }

                    if (!(string.IsNullOrEmpty(password)))
                    {
                        command.Parameters.AddWithValue("@User_Pass", password);
                    } else {
                        command.Parameters.AddWithValue("@User_Pass", user.Password);
                    }

                    command.ExecuteNonQuery();
                    Console.WriteLine("Executed query");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
