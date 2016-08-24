using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using Dapper;

namespace Dashboardify.Repositories
{
    public class UsersRepository
    {
        private string _connectionString;
 
        public UsersRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IList<User> GetList()
        {
            string query = @"SELECT Id,
                            Name,
                            Password,
                            Email,
                            IsActive,
                            DateRegistered,
                            DateModified
                            FROM Users";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    return db.Query<User>
                        (query).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void Update(User user)
        {
            //pasiklausti Zilvino
            string query = @"UPDATE dbo.Users
                            SET Password=@Password,
                            Email=@Email
                            WHERE Id=@Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
            }

        }//needs work

        public User Get(int id)
        {
            
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<User>(@"SELECT 
                            Id,
                            Name,
                            Password,
                            Email,
                            IsActive,
                            DateRegistered,
                            DateModified
                            FROM Users WHERE Id = @Id", new {id}).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }



        }

        public bool CreateUser(User user)
        {
            string query = @"INSERT INTO dbo.Users (Name,Password,Email,IsActive,DateRegistered, DateModified) 
                                            VALUES (@Name,@Password,@Email,@IsActive,@DateRegistered,@Datemodified)";
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var result = db.Execute(query, user);
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
