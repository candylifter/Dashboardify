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
    public class ScreenshotRepository
    {
        private string _connectionString;

        public ScreenshotRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Screenshot GetLastByItemId(int ItemId)
        {
            if (ItemId < 1)
            {
                throw new Exception("Id must be greater than 1 integer");
            }
            else
            {
                try
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
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

        }//Done Debuged

        public bool Create(Screenshot screen)
        {
            if (screen == null)
            {
                throw new Exception("Null object");                
            }
            else
            {
                string query = @"INSERT INTO dbo.ScreenShots                               
                                    (ItemId,
                                    ScrnshtURL,
                                    DateTaken) 
                               VALUES 
                                    (@ItemId,
                                    @ScrnshtURL,
                                    @DateTaken)";
                try
                {
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        var result = db.Execute(query, screen);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        } //Done

        public bool Delete(int screenId)
        {
            if (screenId < 1)
            {
                throw new Exception("Id must be greater than 0");
            }
            else
            {
                
                string query = @"DELETE FROM ScreenShots
                                WHERE Id =" + screenId.ToString();
                try
                {
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        db.Execute(query, screenId);
                    }
                    return true;
                }
                catch (Exception)
                {                 
                    Console.WriteLine(new Exception().Message);
                    throw;
                }
                
            }
            
        } // Done

        public Screenshot Get(int id)
        {
            if (id < 1)
            {
                throw new Exception("Id must be greater than 0");
            }
            else
            {
                string query = @"SELECT
                                   Id,
                                   ItemId,
                                   ScrnshtURL, 
                                   DateTaken
                               FROM 
                                    ScreenShots
                                WHERE Id = " + id.ToString();

                try
                {
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        return db.Query<Screenshot>(query, id).SingleOrDefault();
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }                 
                
            }
            
        } //Done

    }
}
