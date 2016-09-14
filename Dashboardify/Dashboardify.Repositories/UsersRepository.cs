using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
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
                            FROM 
                                Users";
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
            user.DateModified = DateTime.Now;

            string query = @"
                            UPDATE 
                                Users
                            SET 
                                Name = @Name,
                                Password = @Password,
                                Email = @Email,
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
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

        }

        public void ChangeStatus(int id, bool isActive)
        {
            string query = @"
                            UPDATE 
                                Users
                            SET 
                                IsActive = @IsActive
                            WHERE
                                Id = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    db.Execute(query, isActive);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

        }

        public User Get(int id)
        {
            //var userTest = Get(id);
            //if (userTest == null)
            //{
            //   throw new Exception("User was not found in database");
            //}

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
            //var user = Get(userId);

            //if (user == null)
            //{
            //    throw new Exception("User not found in data base!");
            //}

            string query = $"DELETE FROM Users WHERE Id = {userId}";


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

        public User ReturnIfExsists(string username, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"
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
                                Name = '{username}' AND 
                                Password = '{password}'";
                                   

                var result = db.Query<User>(query).SingleOrDefault();

                return result;
            }
        }
    }
}
