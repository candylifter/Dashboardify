﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using Dapper;


namespace Dashboardify.Repositories
{
    public class ItemsRepository
    {
        private string _connString;

        public ItemsRepository(string connString)
        {
            this._connString = connString;
        }
        
 

        public IList<Item> GetList()
        {
            IList<Item> listItems = new List<Item>();
            string queryString = "SELECT * FROM Items";
          
            try
            {
                using (IDbConnection db = new
            SqlConnection(_connString))
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

            string query = @"UPDATE dbo.Items
                            SET Content=@Item_Content,LastChecked=@Item_LastChecked 
                            WHERE Id=@Item_Id";
            try
            {
                using (IDbConnection db = new SqlConnection(_connString))
                {
                    int rowsAffected = db.Execute(query, item);
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
        /// Gets single item
        /// </summary>
        /// <param name="itemId">Item Id attribute</param>
        /// <returns>Returns all data of selected item</returns>
        public Item Get(int itemId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connString))
                {
                    return db.Query<Item>("SELECT * FROM Items WHERE Id = @Id", new { itemId }).SingleOrDefault();
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
        public IList<Item> GetByDashId(int dashId)
        {
            string query = "SELECT * FROM items WHERE DashBoardId = " + dashId.ToString();
            try
            {
                using (IDbConnection db = new SqlConnection(_connString))
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
                using (IDbConnection db = new SqlConnection(_connString))
                {
                    string query = @"INSERT INTO Items([Id],[DashBoardId],[Name],[Website],[CheckInterval],[IsActive],[XPath],[LastChecked],[Created],[Modified],[ScrnshtURL],[Content] ) VALUES (@Id,@DashBoardId,@DashBoardId,@Name,@Website,@CheckInterval,@IsActive,@XPath,@LastChecked,@Created,@Modified,@ScrnshtURL,@Content)";
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
                using (IDbConnection db = new SqlConnection(_connString))
                {
                    string query = "DELETE * FROM Items WHERE Id= " + itemId.ToString();
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