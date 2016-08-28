using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dashboardify.Models;

namespace Dashboardify.Repositories
{
    class ScreenshotRepository
    {
        private string _connectionString;

        public ScreenshotRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Screenshot GetLast(int itemId)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public IList<Screenshot> GetAll()
        {
            string query = @"SELECT
                                Id, 
                                ItemId,
                                ScrnshtURL,
                                DateTaken
                            FROM 
                                ScreenShots
                            WHERE Id=" + "";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    return db.Query<Screenshot>
                        (query).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

        }

    }
}
