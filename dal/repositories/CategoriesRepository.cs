using System;
using System.Collections.Generic;
using System.Text;
using dxOMark_models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace dxOMark_dal
{
    public class CategoriesRepository : BaseRepository, ICategoriesRepository
    {
        //GET ALL CATEGORIES
        public IEnumerable<Category> GetCategories()
        {
            string sql = "SELECT * FROM DxOMark.Category ORDER BY id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Category>(sql);
            }
        }

        //GET A CATEGORY VIA ID
        public Category GetCategoryViaID(int id)
        {
            string sql = "SELECT * FROM DxOMark.Category WHERE id = @id";

            var parameters = new { @id = id };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QuerySingleOrDefault<Category>(sql, parameters);
            }
        }

        //INSERT
        public bool InsertCategory(Category category)
        {
            string sql = @"INSERT INTO DxOMark.Category (name)
                           OUTPUT INSERTED.*
                           VALUES (@name)";

            var parameters = new
            {
                @name = category.Name,
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


        //DELETE

        public bool DeleteCategory(int categoryID)
        {
            string sql = @"DELETE FROM DxOMark.Category
                           WHERE id = @categoryID";

            var parameters = new
            {
                @categoryID = categoryID
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

