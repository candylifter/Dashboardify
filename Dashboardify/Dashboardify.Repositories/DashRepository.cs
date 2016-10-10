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

    public DashBoard GetByNameAndUserId(string name, int id) //change to email
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT
                                 Id,
                                 UserId,
                                 IsActive,
                                 Name,
                                 DateCreated,
                                 DateModified 
                          FROM 
                                 DashBoards 
                          WHERE 
                                 UserId = {id} AND Name ='{name}'";

                return db.Query<DashBoard>(query).SingleOrDefault();
            }
        }
    

        /// <summary>
        /// Updates dashboard in dashboard table
        /// </summary>
        /// <param name="dash">Dashboard Object</param>
        public void Update(DashBoard dash)
        {
          
            string query = $@"UPDATE DashBoards
                                SET IsActive=@IsActive,
                                Name=@Name,
                                DateModified=@Datemodified
                            WHERE Id = {dash.Id}";
            
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, dash);
                }

            }





        public int CreateAndGetId(DashBoard dashBoard)
        {
            string query = $@"INSERT INTO dbo.DashBoards 
                                (UserId, 
                                Name, 
                                DateCreated, 
                                DateModified)
                            VALUES 
                                ({dashBoard.UserId}, 
                                '{dashBoard.Name}', 
                                '{dashBoard.DateCreated}', 
                                '{dashBoard.DateModified}')
                            SELECT SCOPE_IDENTITY()";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<int>(query).SingleOrDefault();
            }

        }

        /// <summary>
        /// Creates new dashboard
        /// </summary>
        /// <param name="dash">Dashboard</param>
        public bool Create(DashBoard dash)
        {
            
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
                
                    db.Execute(query, dash);
                    return true;
               
                
            }
        }

        /// <summary>
        /// Deletes dashboard
        /// </summary>
        /// <param name="dashId">DashboardId</param>
        /// <returns>bool</returns>
        public bool DeleteDashboard(int userId, int id)
        {
            string deleteQuery =
                $@"DELETE
                   FROM 
                        DashBoards
                   WHERE UserId ={userId} AND Id ='{id}'";
                            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                    db.Execute(deleteQuery);
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

        public User GetUserByDashId(int id)
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
        /// <summary>
        /// Checks if dash exsists by name
        /// </summary>
        /// <param name="Userid">userId</param>
        /// <param name="name">DashName</param>
        /// <returns>true ir name not taken, false exsists</returns>
        public bool CheckIfNameAvailable(int Userid, string name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query =
                    $@"SELECT
	                                DashBoards.Name
                                FROM DashBoards
                                WHERE UserId = {Userid} AND DashBoards.Name = '{name}'";


                var result = db.Query<string>(query).SingleOrDefault();

                return result == null;
            }
        }

        public bool CheckIfExistsByUserId(int id, int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"SELECT Id
                                 FROM DashBoards
                                 WHERE Id = {id} AND UserId = {userId}";

                var result = db.Query(query).SingleOrDefault();

                return result == null;
            }
        }
    }
}
