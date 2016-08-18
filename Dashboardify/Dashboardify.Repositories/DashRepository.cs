using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Models;
using System.Data;
using System.Data.SqlClient;

namespace Dashboardify.Repositories
{
    public class DashRepository
    {

        private string _connectionString = "Data Source=.;" +
                                            "Initial Catalog =DashBoardify;" +
                                            "User id=DashBoardify;" +
                                            "Password=123456;";

        private IList<DashBoard> _results;

        private DataTable _GetTableFromDB(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    Console.WriteLine("Connected Succesfully");
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

        public DashRepository()
        {
            string queryString = "SELECT * FROM dbo.DashBoards";

            DataTable datatable = _GetTableFromDB(queryString);

            _results = new List<DashBoard>();

            foreach (DataRow dr in datatable.Rows)
            {
                DashBoard d = new DashBoard();

                d.Id = (int)dr["DashId"];
                d.UserId = (int)dr["UserId"];
                d.IsActive = (bool)dr["IsActive"];
                d.Name = (string)dr["Name"];
                d.DateCreated = (DateTime)dr["DateCreated"];
                d.DateModified = (DateTime)dr["DateModified"];

                _results.Add(d);
            }
        }

        public DashBoard getDashboard(int DashboardId) { };
        public IList<DashBoard> getDashboards() { };
        public void updateDasboard(DashBoard dash) { };
        public int createDashboard(DashBoard dash) { };
        public void deleteDashboard(int DashId) { };
    }
}
