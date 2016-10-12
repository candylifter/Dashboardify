using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dashboardify.Models;

namespace Dashboardify.Repositories
{
    public class UserSessionRepository
    {
        private readonly string _connectionString;

        public UserSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddSession(UserSession session)
        {
            string query = @"INSERT INTO dbo.UserSession                               
                                    (Ticket,
                                    UserId,
                                    Expires) 
                               VALUES 
                                    (@Ticket,
                                    @UserId,
                                    @Expires)";

         
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, session);
                }
                return true;  
        }

        public UserSession GetSession(string ticket)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<UserSession>(@"SELECT
                           Id,
                           Ticket,
                           Expires
                        FROM UserSession
                            WHERE Ticket = @ticket",new { ticket }).SingleOrDefault();
            }
        }
  
        public User GetUserBySessionId(string ticket)
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
	            WHERE Ticket = @ticket";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>(query, new {ticket}).SingleOrDefault();
            }
        }

        public void DeleteUserSession(int userId)
        {
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
               db.Execute($@"DELETE FROM 
                                UserSession 
                            WHERE UserSession.UserId = {userId}"); //Nebus SQL injection, nes po security providerio ateina id is ten
             
            }

        }
    }
}
