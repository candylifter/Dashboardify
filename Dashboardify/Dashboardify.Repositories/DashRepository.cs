using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Dashboardify.Repositories
{
    public class DashRepository
    {

        private string _connectionString;


        public DashRepository(string constring)
        {
            _connectionString = constring;
        }

        /// <summary>
        /// Gets all data from DashBoards table in a list<>
        /// </summary>
        /// <returns>List of all items</returns>
        public IList<DashBoard> GetList()
        {
            string queryString = @"SELECT
                                 Id,
                                 UserId,
                                 IsActive,
                                 Name,
                                 DateCreated,
                                 DateModified 
                                 FROM DashBoards";
            
            
            using (IDbConnection db = new
        SqlConnection(_connectionString))
            {
                return db.Query<DashBoard>
                (queryString).ToList();

            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DashId"></param>
        /// <returns></returns>
        public DashBoard Get(int DashId)
        {
           

            
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var query = @"SELECT
                                 Id,
                                 UserId,
                                 IsActive,
                                 Name,
                                 DateCreated,
                                 DateModified 
                          FROM 
                                 DashBoards 
                          WHERE 
                                 Id = " + DashId;

                    return db.Query<DashBoard>(query).SingleOrDefault();
                }
            }
            

        

        /// <summary>
        /// Updates dashboard in dashboard table
        /// </summary>
        /// <param name="dash">Dashboard Object</param>
        public void Update(DashBoard dash)
        {
            //if (dash == null)
            //{
            //    throw new Exception("User not found");
            //}

            string query = @"UPDATE DashBoards
                                SET IsActive=@IsActive,
                                Name=@Name,
                                DateModified=@Datemodified";
            
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int rowsAffected = db.Execute(query, dash);
                }

            }
           
         

        /// <summary>
        /// Creates new dashboard
        /// </summary>
        /// <param name="dash">Dashboard</param>
        public bool Create(DashBoard dash)
        {
            //if (dash == null)
            //{
            //    throw new Exception("Object not found in DB");
            //}

            string query = @"INSERT INTO dbo.DashBoards 
                                (UserId, 
                                IsActive, 
                                Name, 
                                DateCreated, 
                                DateModified)
                            VALUES 
                                (@UserId, 
                                @IsActive, 
                                @Name, 
                                @DateCreated, 
                                @DateModified)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                    var result = db.Execute(query, dash);
                    return true;
               
                
            }
        }

        /// <summary>
        /// Deletes dashboard
        /// </summary>
        /// <param name="dashId">DashboardId</param>
        /// <returns>bool</returns>
        public bool DeleteDashboard(int dashId)
        {
            string deleteQuery = "DELETE FROM DashBoards";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                    var result = db.Execute(deleteQuery);
                    return true;
                }
               
            }
        

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Ilist</returns>
        public IList<DashBoard> GetByUserId(int userId)
        {
            string query = @"SELECT
                                Id,
                                UserId,
                                IsActive,
                                Name,
                                DateCreated,
                                DateModified
                                FROM DashBoards 
                           WHERE UserId = " + userId;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {   
                    return db.Query<DashBoard>
                      (query).ToList();
            }
        }

        public User GetUserIdByDashId(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"SELECT
                                [Users].Id,
                                [Users].Name,
                                [Users].Password,
                                [Users].Email  
                            FROM
                                Users
                            LEFT JOIN DashBoards
                                ON [DashBoards].UserId = [Users].Id
                            WHERE [DashBoards].Id = '{id}'";

                return db.Query<User>(query).SingleOrDefault();
            }
        }
    }
}
