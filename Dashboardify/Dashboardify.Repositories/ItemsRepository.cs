using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Dashboardify.Repositories
{
    public class ItemsRepository
    {
        private string _connectionString = "Data Source=.;" +
                                            "Initial Catalog=DashBoardify;" +
                                            "User id=DashboardifyUser;" +
                                            "Password=123456;";

        private IList<Item> _results;

        private DataTable _GetTableFromDB(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public ItemsRepository()
        {
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
            }
        }

        public IList<Item> GetList()
        {
            return _results.ToList();
        }

        public void UpdateItem(Item item)
        {

            string query = @"UPDATE dbo.Items
                            SET Content=@Item_Content,LastChecked=@Item_LastChecked 
                            WHERE Id=@Item_Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public void Update(Item item)
        {
            foreach (Item obj in _results)
            {
                if (obj.Id == item.Id)
                {
                    obj.Url = item.Url;
                    obj.Xpath = item.Xpath;
                }
            }
            
        }
    }
}