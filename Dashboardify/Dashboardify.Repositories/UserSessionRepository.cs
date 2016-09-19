using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dashboardify.Models;

namespace Dashboardify.Repositories
{
    public class UserSessionRepository
    {
        private string _connectionString;

        public UserSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddSession(UserSession session)
        {
            string query = @"INSERT INTO dbo.UserSession                               
                                    (SessionId,
                                    UserId,
                                    Expires) 
                               VALUES 
                                    (@SessionId,
                                    @UserId,
                                    @Expires)";

         
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, session);
                }
                return true;  
        }

        public DateTime GetExpireDate(string sessionId, int userId)
        {

            string query = $@"SELECT Expires
                            FROM UserSession
                            WHERE SessionId = '{sessionId}'
                            AND 
                            UserId ='{userId}'";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<DateTime>(query).SingleOrDefault();
            }
            
        }

        public IList<UserSession> GetAll()
        {
            string query = @"SELECT 
                                Id,
                                UserId,
                                Expire
                            FROM
                                UserSession";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<UserSession>
                        (query).ToList();
                }
                
            }
            catch (Exception ex) 
            {
                
                throw;
            }


        }

        public User GetUserBySessionId(string sessionId)
        {
            string query =
                $@"SELECT 
		                Users.Id,
		                Name,
		                Password,
		                Email,
		                IsActive,
		                DateRegistered,
		                DateModified
                FROM Users
                JOIN UserSession
	                ON UserSession.UserId = Users.Id
	                WHERE SessionId = '{sessionId}'";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>(query).SingleOrDefault();
            }
        }
    }
}
