using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
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

        public int CreateUserAndGetHisId(User user)
        {
            string query = $@"INSERT INTO dbo.Users
                                (Name,
                                Password,
                                Email,
                                IsActive,
                                DateRegistered, 
                                DateModified) 
                           VALUES 
                                ('{user.Name}',
                                '{user.Password}',
                                '{user.Email}',
                                '{user.IsActive}',
                                '{user.DateRegistered}',
                                '{user.DateModified}')
                            
                                SELECT SCOPE_IDENTITY()";


            using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    //db.Execute(query, user);
                    return db.Query<int>(query).SingleOrDefault();
                }
            
          
        }

        public int GetLatestUserId()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"SELECT SCOPE_IDENTITY() FROM USERS";

                return db.Query<int>(query).SingleOrDefault();
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
        /// <summary>
        /// Returns User object by query null if does not exsists
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User ReturnIfExsists(string email, string password)
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
                                Email = '{email}' AND 
                                Password = '{password}'";
                                   

                var result = db.Query<User>(query).SingleOrDefault();

                return result;
            }
        }
        /// <summary>
        /// Return email by query
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ReturnEmail(string email)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query =
                $@"SELECT
                                Email
                            FROM
                                Users
                            WHERE 
                                Email = '{email}'";

                var result = db.Query<string>(query).SingleOrDefault();

                return result;
            }
        }



        public bool CheckIfEmailAvailable(string email)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query =
                $@"SELECT
                                Email
                            FROM
                                Users
                            WHERE 
                                Email = '{email}'";

                var result = db.Query<string>(query).SingleOrDefault();

                return result == null;
            }
        }


    }
}
