using System;
using System.Linq;
using Dashboardify.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Dashboardify.Repositories
{
    public class UsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
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
                                DateRegistered, 
                                DateModified) 
                           VALUES 
                                (@Name,
                                @Password,
                                @Email,
                                @DateRegistered,
                                @DateModified)
                       
                                SELECT SCOPE_IDENTITY()";


            using (IDbConnection db = new SqlConnection(_connectionString))
                {
                   return db.Query<int>(query, user).SingleOrDefault();
                }
            
          
        }

        public void DeleteUser(int userId)
        {
            
            string query = $"DELETE FROM Users WHERE Id = {userId}"; // nebus injection, nes po security provider


                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, userId);
                }
            

        }
       
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
