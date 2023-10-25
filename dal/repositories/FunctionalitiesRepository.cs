using dxOMark_dal;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using dxOMark_dal.interfaces;

namespace dxOMark_dal
{
    public class FunctionalitiesRepository : BaseRepository, IFunctionalitiesRepository
    {

        //GET ALL FUNCTIONALITIES
        public IEnumerable<Functionality> GetFunctionalities()
        {
            string sql = "SELECT * FROM DxOMark.Functionality ORDER BY id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Functionality>(sql);
            }
        }

        //INSERT FUNCTIONALITY
        public bool InsertFunctionality(Functionality functionality)
        {
            string sql = @"INSERT INTO DxOMark.Functionality (name)
                           OUTPUT INSERTED.*
                           VALUES (@name)";

            var parameters = new
            {
                @name = functionality.Name,
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }


        //DELETE FUNCTIONALITY
        public bool DeleteFunctionality(int functionalityID)
        {
            string sql = @"DELETE FROM DxOMark.Functionality
                           WHERE id = @functionalityID";

            var parameters = new
            {
                @functionalityID = functionalityID
            };
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionString))
                {
                    var affectedRows = db.Execute(sql, parameters);
                    if (affectedRows >= 1)
                    {
                        return true;
                    }
                }               
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
