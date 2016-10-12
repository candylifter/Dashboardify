using System.Collections.Generic;
using System.Linq;
using Dashboardify.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Dashboardify.Repositories
{
    public class DashRepository
    {

        private readonly string _connectionString;


        public DashRepository(string constring)
        {
            _connectionString = constring;
        }

        
        public DashBoard Get(int dashId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                const string query = @"SELECT
                                 Id,
                                 UserId,
                                 IsActive,
                                 Name,
                                 DateCreated,
                                 DateModified 
                          FROM 
                                 DashBoards 
                          WHERE 
                                 Id = @dashId";

                return db.Query<DashBoard>(query,new {dashId}).SingleOrDefault();
            }
        }

        
        public void Update(DashBoard dash)
        {
          
            string query = $@"UPDATE DashBoards
                                SET IsActive=@IsActive,
                                Name=@Name,
                                DateModified=@Datemodified
                            WHERE Id = @Id";
            
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query, dash);
                }

            }





        public int CreateAndGetId(DashBoard dashBoard)
        {
            string query =
                $@"INSERT INTO dbo.DashBoards 
                                (UserId, 
                                Name, 
                                DateCreated, 
                                DateModified)
                            VALUES 
                                (@UserId, 
                                @Name, 
                                @DateCreated, 
                                @DateModified)
                            SELECT SCOPE_IDENTITY()";
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<int>(query, dashBoard).SingleOrDefault();
            }

        }

        

        public void Create(DashBoard dash)
        {
            
            string query = @"INSERT INTO dbo.DashBoards 
                                (UserId, 
                                IsActive, 
                                Name, 
                                DateCreated, 
                                DateModified)
                            VALUES 
                                (@UserId, 
                                @IsActive, 
                                @Name, 
                                @DateCreated, 
                                @DateModified)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                
                    db.Execute(query, dash);
             }
        }

        
        public bool DeleteDashboard(int userId, int id)
        {
            string deleteQuery =
                $@"DELETE
                   FROM 
                        DashBoards
                   WHERE UserId ={userId} AND Id ='{id}'";
                            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                    db.Execute(deleteQuery);
                    return true;
                }
               
            }
        

       
        public IList<DashBoard> GetByUserId(int userId)
        {
            string query = @"SELECT
                                Id,
                                UserId,
                                IsActive,
                                Name,
                                DateCreated,
                                DateModified
                                FROM DashBoards 
                           WHERE UserId = @userId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<DashBoard>(query, new {userId}).ToList();
            }
        }

        public User GetUserByDashId(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>(@"SELECT
                                [Users].Id,
                                [Users].Name,
                                [Users].Password,
                                [Users].Email  
                            FROM
                                Users
                            LEFT JOIN DashBoards
                                ON [DashBoards].UserId = [Users].Id
                            WHERE [DashBoards].Id = @id",new {id}).SingleOrDefault();
            }
        }
       
        
    }
}
