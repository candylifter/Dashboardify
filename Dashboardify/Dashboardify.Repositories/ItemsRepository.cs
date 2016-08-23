using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Dashboardify.Repositories
{
    public class ItemsRepository
    {
        private string _connString;

        public ItemsRepository(string connString)
        {
            this._connString = connString;
            /*
            string queryString = "SELECT * FROM dbo.Items(NOLOCK)";
            DataTable datatable = _GetTableFromDB(queryString);

            _results = new List<Item>();

            foreach (DataRow dr in datatable.Rows)
            {
                Item i = new Item();

                i.Id = (int)dr["Id"];
                i.DashBoardId = (int)dr["DashBoardId"];
                i.Name = (string)dr["Name"];
                i.Url = (string)dr["Website"];
                i.CheckInterval = (int)dr["CheckInterval"];
                i.isActive = (bool)dr["IsActive"];
                i.Xpath = (string)dr["XPath"];
                i.LastChecked = (DateTime)dr["LastChecked"];
                i.Created = (DateTime)dr["Created"];
                i.Modified = (DateTime)dr["Modified"];
                i.ScrnshtURL = (string)dr["ScrnshtURL"];
                i.Content = (string)dr["Content"];

                _results.Add(i);
                */
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

            using (IDbConnection db = new SqlConnection(_connString))
            {
                int rowsAffected = db.Execute(query, item);
                return rowsAffected;
            }

            //using (SqlConnection connection = new SqlConnection(_connString))
            //{

            //    SqlCommand command = new SqlCommand(query, connection);

            //    try
            //    {
            //        connection.Open();
            //        Console.WriteLine("Opened connection to DB");

            //        command.Parameters.AddWithValue("@Item_Id", item.Id);
            //        command.Parameters.AddWithValue("@Item_Content", item.Content);
            //        command.Parameters.AddWithValue("@Item_LastChecked", item.LastChecked.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                
            //        command.ExecuteNonQuery();
            //        Console.WriteLine("Executed query");

            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}


        }
        /// <summary>
        /// Gets single item
        /// </summary>
        /// <param name="itemId">Item Id attribute</param>
        /// <returns>Returns all data of selected item</returns>
        public Item Get(int itemId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<Item>("SELECT * FROM Items WHERE Id = @Id", new {itemId}).SingleOrDefault();
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
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<Item>
                (query).ToList();
            }
        }

       
    }
}