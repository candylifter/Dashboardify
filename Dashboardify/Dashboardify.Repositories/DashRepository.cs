using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this._connectionString = constring;
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
                                 DateModified, 
                                 FROM DashBoards";
            try
            {
                using (IDbConnection db = new
            SqlConnection(_connectionString))
                {
                    return db.Query<DashBoard>
                    (queryString).ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MAESTRO SAUNA DAR VIENA EXCEPTIONA");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DashId"></param>
        /// <returns></returns>
        public DashBoard Get(int DashId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<DashBoard>(@"SELECT
                                 Id,
                                 UserId,
                                 IsActive,
                                 Name,
                                 DateCreated,
                                 DateModified, 
                                               FROM DashBoards WHERE Id = @Id", new { DashId }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sekems Irmantai :))))))))");
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        /// <summary>
        /// Updates dashboard in dashboard table
        /// </summary>
        /// <param name="dash">Dashboard Object</param>
        public int Update(DashBoard dash)
        {
            string query = @"UPDATE DashBoards
                            SET IsActive=@IsActive,
                            Name=@Name,
                            DateModified=@Datemod";
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int rowsAffected = db.Execute(query, dash);
                    return rowsAffected;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Sekems Irmantai :))))))))");
                Console.WriteLine(ex.Message);
                throw;
            }
        } 
        /// <summary>
        /// Creates new dashboard
        /// </summary>
        /// <param name="dash">Dashboard</param>
        public bool Create(DashBoard dash)
        {
            string query = @"INSERT INTO dbo.DashBoards (UserId, IsActive, Name, DateCreated, DateModified)
                            VALUES (@UserId, @IsActive, @Name, @DateCreated, @DateModified)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    var result = db.Execute(query, dash);
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine(new Exception().Message);
                    throw;
                }
                
            }
        }
        /// <summary>
        /// Deletes dashboard
        /// </summary>
        /// <param name="dashId">DashboardId</param>
        /// <returns>bool</returns>
        public bool DeleteDashboard(int dashId)
        {
            string deleteQuery = "DELETE * FROM DashBoards";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    var result = db.Execute(deleteQuery);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
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
                            FROM DashBoards WHERE UserId = " + userId.ToString();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    return db.Query<DashBoard>
                      (query).ToList();
                }
                catch (Exception)
                {
                    Console.WriteLine(new Exception().Message);
                    throw;
                }
                
            }
        }
    }
}
