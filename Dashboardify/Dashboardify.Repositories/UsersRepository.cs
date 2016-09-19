using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
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
                
                    db.Execute(query, user);

                
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
                
                    db.Execute(query, isActive);

               
            }

        }

        public User Get(int id)
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
            
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var result = db.Execute(query, user);
                    return true;
                }
            
          
        }

        public void DeleteUser(int userId)
        {
            
            string query = $"DELETE FROM Users WHERE Id = {userId}";


                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, userId);
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
