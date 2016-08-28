using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
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

        // TODO: Needs work
        public void Update(User user) //Daugiau if validation
        {
            var origin = Get(user.Id);

            if (origin == null)
            {
                throw new Exception("User not found in data base!");
            }

            if (origin.Name != user.Name)
            {
                origin.Name = user.Name;
            }
            if (origin.Password != user.Password)
            {
                origin.Password = user.Password;
            }
            if (origin.Email != user.Email)
            {
                origin.Email = user.Email;
            }
            if (origin.IsActive != user.IsActive)
            {
                origin.IsActive = user.IsActive;
            }
            origin.DateModified = DateTime.Now;

            string query = @"
                            UPDATE 
                                Users
                            SET 
                                Name = @Name,
                                Password = @Password,
                                Email = @Email,
                                IsActive = @IsActive, 
                                DateModified = @DateModified
                            WHERE 
                                Id = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    db.Execute(query, user);

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        public User Get(int id)
        {

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<User>(@"
                            SELECT 
                                Id,
                                Name,
                                Password,
                                Email,
                                IsActive,
                                DateRegistered,
                                DateModified
                            FROM 
                                Users 
                            WHERE 
                                Id = @Id", new { id }).SingleOrDefault();

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
            string query = @"INSERT INTO dbo.Users
                                (Name,
                                Password,
                                Email,
                                IsActive,
                                DateRegistered, 
                                DateModified) 
                           VALUES 
                                (@Name,
                                @Password,
                                @Email,
                                @IsActive,
                                @DateRegistered,
                                @Datemodified)";
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var result = db.Execute(query, user);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteUser(int userId)
        {
            var user = Get(userId);

            if (user == null)
            {
                throw new Exception("User not found in data base!");
            }

            string query = @"DELETE FROM Users 
                            WHERE 
                                Id = @Id";


            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, userId);
                }
            }
            catch (Exception eex)
            {
                Console.WriteLine(eex.Message);
                throw;
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
