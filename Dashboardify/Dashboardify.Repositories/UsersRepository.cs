using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

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

        public void Update(User user, string email, string password)// string uzsisteina konstruktorium, private_results nereikia
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

        public User Get(int id)
        {
            IList<User> users = GetList();

            foreach (var user in users)
            {
                if (user.UserId == id)
                {
                    return user;
                }
            }
            return null;
        }

        public void CreateUser(User user)
        {
            string query = "INSERT INTO dbo.Users (Name,Password,Email,IsActive,DateRegistered, DateModified) VALUES (@name, @password, @email, @isactive)";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    

                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = user.Name;

                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = _HashPassword(user.Password);

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;

                    cmd.Parameters.Add("@isactive", SqlDbType.VarChar).Value = user.IsActive;

                    

                    

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteUser(int userId)
        {
            SqlConnection connection = new SqlConnection();

            using (SqlConnection sc = new SqlConnection(_connectionString))
            {
                try
                {
                    sc.Open();
                    SqlCommand command = new SqlCommand(
                        "DELETE FROM Users WHERE id = '@id'" +
                        connection);

                    command.Parameters.AddWithValue("@id", userId);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
        }

        private string _HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
