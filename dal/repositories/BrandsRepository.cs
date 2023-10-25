using dxOMark_dal;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;

namespace dxOMark_dal
{
    public class BrandsRepository : BaseRepository, IBrandsRepository
    {

        //GET ALL BRANDS
        public IEnumerable<Brand> GetBrands()
        {
            string sql = "SELECT * FROM DxOMark.Brand ORDER BY id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Brand>(sql);
            }
        }

        //INSERT
        public bool InsertBrand(Brand brand)
        {
            string sql = @"INSERT INTO DxOMark.Brand (name)
                           OUTPUT INSERTED.*
                           VALUES (@name)";

            var parameters = new
            {
                @name = brand.Name,               
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
    }
}
