using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using Dapper;


namespace Dashboardify.Repositories
{
    public class ItemsRepository //TODO: Scrnsht url panaikintas pakeisti cia
    {
        private string _connectionString;
        /// <summary>
        /// Item repository
        /// </summary>
        /// <param name="connString">ConnectionString to DB</param>
        public ItemsRepository(string connString)
        {
            this._connectionString = connString;
        }

        /// <summary>
        /// Gets list of all items from Item table
        /// </summary>
        /// <returns>Items</returns>
        public IList<Item> GetList()
        {
            string queryString = @"SELECT Id,
                                          DashBoardId, 
                                          Name, 
                                          Website,
                                          CheckInterval, 
                                          IsActive, 
                                          XPath, 
                                          LastChecked, 
                                          Created, 
                                          Modified, 
                                          Content 
                                  FROM Items";
          
            try
            {
                using (IDbConnection db = new
            SqlConnection(_connectionString))
                {
                    return db.Query<Item>
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
        /// Updates Item in DB, and 
        /// </summary>
        /// <param name="item">Item object</param>
        /// <returns>returns number of lines affected</returns>
        public int Update(Item item)
        {

            //if (item.Id < 1)
            //{
            //    throw new Exception("Invalid item Id error");
            //}

            string query = @"UPDATE dbo.Items
                            SET Content=@Content,
                            LastChecked=@LastChecked,
                            DashBoardId=@DashBoardId,
                            Name=@Name,
                            CheckInterval=@CheckInterval,
                            XPath=@XPath,
                            IsActive=@IsActive
                            WHERE Id=@Id";
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int rowsAffected = db.Execute(query, item);
                    return rowsAffected;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Sekmes Irmantai :))))))))");
                Console.WriteLine(ex.Message);
                throw;
            }
            



        }

        /// <summary>
        /// Gets single item
        /// </summary>
        /// <param name="itemId">Item Id attribute</param>
        /// <returns>Returns all data of selected item</returns>
        public Item Get(int itemId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<Item>(@"SELECT
                                          Id,
                                          DashBoardId,
                                          Name, 
                                          Website,
                                          CheckInterval, 
                                          IsActive, 
                                          XPath, 
                                          LastChecked, 
                                          Created, 
                                          Modified,  
                                          Content 
                                          FROM Items WHERE Id = " + itemId.ToString()).SingleOrDefault(); //item.id
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
        /// Returns List of items in same dashboard
        /// </summary>
        /// <param name="dashId">Dashboard Id</param>
        /// <returns>List of all items in same dash</returns>
        public IList<Item> GetByDashboardId(int dashId)
        {
            string query = @"SELECT
                                Id, 
                                Name, 
                                Website,
                                CheckInterval, 
                                IsActive, 
                                XPath, 
                                LastChecked, 
                                Created, 
                                Modified, 
                                Content  
                             FROM Items 
                                 WHERE DashBoardId = " + dashId.ToString();
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<Item>
                        (query).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// CreatesItem
        /// </summary>
        /// <param name="item">Item object</param>
        public void Create(Item item)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Items( 
                                         DashBoardId, 
                                         Name,
                                         Website,
                                         CheckInterval,
                                         IsActive,
                                         XPath,
                                         LastChecked,
                                         Created,
                                         Modified,
                                         Content) 
                                     VALUES (
                                         @DashBoardId,
                                         @Name,
                                         @Website,
                                         @CheckInterval,
                                         @IsActive,
                                         @XPath,
                                         @LastChecked,
                                         @Created,
                                         @Modified,
                                         @Content)";
                    var result = db.Execute(query, item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }

        /// <summary>
        /// Deletes item by id
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns>Number of rows affected</returns>
        public int Delete(int itemId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Items WHERE Id= " + itemId.ToString();
                    int rowsAffected = db.Execute(query);
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }


       
    }
}