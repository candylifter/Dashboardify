using System;
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
        private string _connectionString;

        public ScreenshotRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Screenshot GetLastByItemId(int ItemId)
        {
            
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        return db.Query<Screenshot>(@"SELECT
	                                                    TOP 1
	                                                    Id,
	                                                    ItemId,
	                                                    ScrnshtURL,
	                                                    DateTaken
                                                    FROM 
	                                                    ScreenShots 
                                                    WHERE 
	                                                    ItemId = @ItemId
                                                    ORDER BY 
	                                                    DateTaken DESC", new { ItemId }).SingleOrDefault();
                    }
               
            

           

        }//Done Debuged

        public IList<Screenshot> GetAll()
        {
            string query = @"SELECT
                                Id, 
                                ItemId,
                                ScrnshtURL,
                                DateTaken
                            FROM 
                                ScreenShots";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                    return db.Query<Screenshot>
                        (query).ToList();
               
            }

        }//Done Debuged

        public bool Create(Screenshot screen)
        {
            //if (screen == null)
            //{
            //    throw new Exception("Null object");                
            //}
            
                string query = @"INSERT INTO dbo.ScreenShots                               
                                    (ItemId,
                                    ScrnshtURL,
                                    DateTaken) 
                               VALUES 
                                    (@ItemId,
                                    @ScrnshtURL,
                                    @DateTaken)";
                
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        var result = db.Execute(query, screen);
                        return true;
                    }
               
            
        } //Done

        public bool Delete(int screenId)
        {
           
            
                
                string query = @"DELETE FROM ScreenShots
                                WHERE Id =" + screenId.ToString();
               
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        db.Execute(query, screenId);
                    }
                    return true;
                
                
            
            
        } // Done

        public Screenshot Get(int id)
        {
           
            
                string query = @"SELECT
                                   Id,
                                   ItemId,
                                   ScrnshtURL, 
                                   DateTaken
                               FROM 
                                    ScreenShots
                                WHERE Id = " + id.ToString();

                
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        return db.Query<Screenshot>(query, id).SingleOrDefault();
                    }
                              
                
            
            
        } //Done

    }
}
