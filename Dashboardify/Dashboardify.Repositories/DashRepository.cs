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

        private IList<DashBoard> _results;

       

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
            string queryString = "SELECT * FROM DashBoards";
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
                    return db.Query<DashBoard>("SELECT * FROM DashBoards WHERE Id = @Id", new { DashId }).SingleOrDefault();
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
            string query = @"UPDATE dbo.DashBoards
                            SET Content=@Item_Content,LastChecked=@Item_LastChecked 
                            WHERE Id=@Item_Id";
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

        public void Create(DashBoard dash) {
            string query = @"INSERT INTO dbo.DashBoards (UserId, IsActive, Name, DateCreated, DateModified)
                            VALUES (@UId, @IsAct, @Name, @DateCreated, @DateModified);
                            SELECT SCOPE_IDENTITY()";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    Console.WriteLine("Opened connection to DB");
                    command.Parameters.AddWithValue("@UId", dash.UserId);
                    command.Parameters.AddWithValue("@IsAct", dash.IsActive);
                    command.Parameters.AddWithValue("@Name", dash.Name);
                    command.Parameters.AddWithValue("@DateCreated", dash.DateCreated);
                    command.Parameters.AddWithValue("@DateModified", dash.DateModified);

                    command.ExecuteNonQuery();
                    int modified = (int)command.ExecuteScalar();
                    dash.Id = modified;
                    Console.WriteLine("Executed query");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void deleteDashboard(int dashId) {
            SqlConnection connection = new SqlConnection();

            using (SqlConnection sc = new SqlConnection(_connectionString))
            {
                try
                {
                    sc.Open();
                    SqlCommand command = new SqlCommand(
                        "DELETE FROM DashBoards WHERE Id = '@id'" +
                        connection);

                    command.Parameters.AddWithValue("@id", dashId);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
        }
    }
}
