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
            this._connectionString = connectionString;
        }

        public bool AddSession(UserSession session)
        {
            string query = @"INSERT INTO dbo.UserSession                               
                                    (Id,
                                    UserId,
                                    Expires) 
                               VALUES 
                                    (@Id,
                                    @UserId,
                                    @Expires)";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var result = db.Execute(query, session);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;

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
    }
}
