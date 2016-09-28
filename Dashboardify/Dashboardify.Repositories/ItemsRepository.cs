using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using Dapper;
using Dashboardify.Models;


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
                                          CSS,
                                          LastChecked, 
                                          Created, 
                                          Modified, 
                                          Content 
                                  FROM Items";
          
           
                using (IDbConnection db = new
            SqlConnection(_connectionString))
                {
                    return db.Query<Item>
                    (queryString).ToList();

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
                            Modified=@Modified,
                            DashBoardId=@DashBoardId,
                            Name=@Name,
                            CheckInterval=@CheckInterval,
                            XPath=@XPath,
                            CSS=@CSS,
                            IsActive=@IsActive
                            WHERE Id=@Id";
           
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int rowsAffected = db.Execute(query, item);
                    return rowsAffected;
                }

            
            



        }

        /// <summary>
        /// Gets single item
        /// </summary>
        /// <param name="itemId">Item Id attribute</param>
        /// <returns>Returns all data of selected item</returns>
        public Item Get(int itemId)
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
                                                  CSS,
                                                  LastChecked, 
                                                  Created, 
                                                  Modified,  
                                                  Content 
                                          FROM 
                                                  Items 
                                          WHERE Id = " + itemId).SingleOrDefault(); //item.id
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
                                DashBoardId,
                                Name, 
                                Website,
                                CheckInterval, 
                                IsActive, 
                                XPath, 
                                CSS,
                                LastChecked, 
                                Created, 
                                Modified, 
                                Content  
                             FROM Items 
                                 WHERE DashBoardId = " + dashId.ToString();
            
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<Item>
                        (query).ToList();
                }
          
        }

        /// <summary>
        /// CreatesItem
        /// </summary>
        /// <param name="item">Item object</param>
        public void Create(Item item)
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
                                         CSS,
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
                                         @CSS,
                                         @LastChecked,
                                         @Created,
                                         @Modified,
                                         @Content)";
                    var result = db.Execute(query, item);
                }
            
           
        }

        /// <summary>
        /// Deletes item by id
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns>Number of rows affected</returns>
        public void Delete(int itemId)
        { 
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Items WHERE Id= " + itemId.ToString();
                    db.Execute(query);
                   
                }

        }

        public User GetByUserItemId(int itemId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query =$@"SELECT
                                    Users.Id
                                FROM Users
                                    JOIN DashBoards
                                    ON DashBoards.UserId = Users.Id
                                        JOIN Items
                                        ON Items.DashBoardId = DashBoards.Id
                                WHERE Items.Id = {itemId}";
                return db.Query<User>(query).SingleOrDefault();
            }
        }
    }
}