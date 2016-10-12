using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dashboardify.Models;

namespace Dashboardify.Repositories
{
    public class ScreenshotRepository
    {
        private readonly string _connectionString;

        public ScreenshotRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

 
        public void Create(Screenshot screen)
        {
            const string query = @"INSERT INTO dbo.ScreenShots                               
                                    (ItemId,
                                    ScrnshtURL,
                                    DateTaken) 
                               VALUES 
                                    (@ItemId,
                                    @ScrnshtURL,
                                    @DateTaken)";

            using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                         db.Execute(query, screen);
                    }
        }


        public IList<Screenshot> GetLastsByItemId(int itemId, int count) ////count statinis tai px
        {
            string query = @"SELECT TOP " + count + @" Id,ItemId,ScrnshtUrl,DateTaken 
                            FROM 
                                ScreenShots 
                            WHERE 
                                ItemId =@itemId 
                            ORDER BY 
                                DateTaken DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Screenshot>(query, new {itemId}).ToList();
              
            }
        }

    }
}
