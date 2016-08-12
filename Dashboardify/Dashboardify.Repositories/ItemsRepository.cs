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

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);

                reader.Close();

                return dt;
            }
        }

        public ItemsRepository()
        {

     
            string queryString = "SELECT * FROM dbo.Items";

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

                _results.Add(i);

            }
        }

        public IList<Item> GetList()
        {
            return _results.ToList();
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