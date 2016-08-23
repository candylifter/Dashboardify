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
        
        
        private DataTable _GetTableFromDB(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    //Console.WriteLine("Connected Succesfully");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        

        public IList<Item> GetList()
        {
            IList<Item> listItems = new List<Item>();
            string queryString = "SELECT * FROM Items";
            //try
            //{
            //    DataTable data = _GetTableFromDB(queryString);

            //    foreach (DataRow dr in data.Rows)
            //    {
            //        //Item d = new Item();

            //        //d.Id = (int) dr["Id"];
            //        //d.DashBoardId = (int) dr["DashBoardId"];
            //        //d.Name = (string) dr["Name"];
            //        //d.Url = (string) dr["Website"];
            //        //d.CheckInterval = (int) dr["CheckInterval"];
            //        //d.isActive = (bool) dr["IsActive"];
            //        //d.Xpath = (string) dr["XPath"];
            //        //d.LastChecked = (DateTime) dr["LastChecked"];
            //        //d.Created = (DateTime) dr["Created"];
            //        //d.Modified = (DateTime) dr["Modified"];
            //        //d.ScrnshtURL = (string) dr["ScrnshtURL"];
            //        //d.Content = (string) dr["Content"];

            //        //listItems.Add(d);

            //    }
            //    return listItems.ToList();
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine("Sekmės Irmantai :)");
            //    //Console.WriteLine(ex.Message);
            //    //throw;
            //}
            try
            {
                using (IDbConnection db = new
            SqlConnection(_connString))
                {
                    return db.Query<Item>
                    (queryString).ToList();

                }
            }
            catch (Exception)
            {
                Console.WriteLine("MAESTRO SAUNA DAR VIENA EXCEPTIONA");
                throw;
            }
            

        }

        public void UpdateItem(Item item)
        {

            string query = @"UPDATE dbo.Items
                            SET Content=@Item_Content,LastChecked=@Item_LastChecked 
                            WHERE Id=@Item_Id";

            using (SqlConnection connection = new SqlConnection(_connString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    Console.WriteLine("Opened connection to DB");

                    command.Parameters.AddWithValue("@Item_Id", item.Id);
                    command.Parameters.AddWithValue("@Item_Content", item.Content);
                    command.Parameters.AddWithValue("@Item_LastChecked", item.LastChecked.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                
                    command.ExecuteNonQuery();
                    Console.WriteLine("Executed query");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }

       
    }
}