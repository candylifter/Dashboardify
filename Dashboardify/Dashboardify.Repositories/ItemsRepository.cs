﻿using System;
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

        private static void ReadSingleRow(IDataRecord record)
        {
            Console.WriteLine(String.Format("{0}, {1}", record[0], record[1]));
        }

        public ItemsRepository()
        {

     
            string queryString = "SELECT * FROM dbo.Items";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                _results = new List<Item>();

                while (reader.Read())
                {
                    Item i = new Item();
                    i.Id = (int)reader["Id"];
                    i.DashBoardId = (int)reader["DashBoardId"];
                    i.Name = (string)reader["Name"];
                    i.Url = (string)reader["Website"];
                    i.CheckInterval = (int)reader["CheckInterval"];
                    i.isActive = (bool)reader["IsActive"];
                    i.Xpath = (string)reader["XPath"];
                    i.LastChecked = (DateTime)reader["LastChecked"];
                    i.Created = (DateTime)reader["Created"];
                    i.Modified = (DateTime)reader["Modified"];
                    i.ScrnshtURL = (string)reader["ScrnshtURL"];

                    _results.Add(i);

                }

                reader.Close();

            }



            //    _results = new List<Item>();

            //_results.Add(new Item
            //{
            //    Id = 1,
            //    Url = "http://www.autogidas.lt/automobiliai/?f_1=&f_model_14=&f_215=&f_216=&f_41=&f_42=&f_3=&f_2=&f_376=",
            //    Xpath = "/html/body/div/div[8]/div/div[2]/a[1]/div"
            //});

            //_results.Add(new Item
            //{
            //    Id = 2,
            //    Url = "http://site.adform.com/",
            //    Xpath = "/html[1]/body[1]/div[1]/section[1]/div[2]/div[1]/div[1]/article[1]"
            //});
            //_results.Add(new Item
            //{
            //    Id = 3,
            //    Url = "https://news.ycombinator.com/",
            //    Xpath = "/html/body/table/tbody/tr[14]/td[2]/span[1]"
            //});
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