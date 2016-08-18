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

        public IList<DashBoard> GetList()
        {
            return _results.ToList();
        }

        public DashBoard Get(int dashboardId) {
            IList<DashBoard> dashboards = GetList();

            foreach (var dashboard in dashboards)
            {
                if (dashboard.Id == dashboardId)
                {
                    return dashboard;
                }
            }
            return null;
        }

        public void Update(DashBoard dash) {

        }

        public void Create(DashBoard dash) {
            string query = @"INSERT INTO dbo.DashBoards (UserId, IsActive, Name, DateCreated, DateModified)
                            VALUES (@UId, @IsAct, @Name, @DateCreated, @DateModified);
                            SELECT SCOPE_IDENTITY()";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    Console.WriteLine("Opened connection to DB");
                    command.Parameters.AddWithValue("@UId", dash.UserId);
                    command.Parameters.AddWithValue("@IsAct", dash.IsActive);
                    command.Parameters.AddWithValue("@Name", dash.Name);
                    command.Parameters.AddWithValue("@DateCreated", dash.DateCreated);
                    command.Parameters.AddWithValue("@DateModified", dash.DateModified);

                    command.ExecuteNonQuery();
                    int modified = (int)command.ExecuteScalar();
                    dash.Id = modified;
                    Console.WriteLine("Executed query");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void deleteDashboard(int dashId) {
            SqlConnection connection = new SqlConnection();

            using (SqlConnection sc = new SqlConnection(_connectionString))
            {
                try
                {
                    sc.Open();
                    SqlCommand command = new SqlCommand(
                        "DELETE FROM DashBoards WHERE Id = '@id'" +
                        connection);

                    command.Parameters.AddWithValue("@id", dashId);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
        }
    }
}
