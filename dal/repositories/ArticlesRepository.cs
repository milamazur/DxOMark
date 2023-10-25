using System;
using System.Collections.Generic;
using System.Text;
using dxOMark_models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Policy;

namespace dxOMark_dal
{
    public class ArticlesRepository : BaseRepository, IArticlesRepository
    {
        //Get the three most recent articles (upload date)
        public ICollection<Article> GetTop3Articles()
        {
            var sql = @"SELECT TOP 3 *
                        FROM DxOMark.Article
                        ORDER BY uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Article>(sql).ToList();
            }
        }


        //ARTICLES WITHOUT DEVICE

        //1. Getting articles via title
        public List<Article> GetArticlesViaTitle(string title = "")
        {
            var sql = @"SELECT * 
                            FROM DxOMark.Article 
                            WHERE title LIKE '%' + @title + '%'
                            ORDER BY uploadDate DESC";
            var parameters = new { @title = title };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Article>(sql, parameters).AsList();
            }
        }

        //2. Getting articles via upload date and title
        public List<Article> GetArticlesViaUploadDateTitle(DateTime? uploadDate = null, string title = "")
        {
            var sql = @"SELECT * 
                            FROM DxOMark.Article
                            WHERE YEAR(uploadDate) = YEAR(@uploadDate) AND title LIKE '%' + @title + '%'
                            ORDER BY uploadDate DESC";
            var parameters = new { @uploadDate = uploadDate, @title = title };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Article>(sql, parameters).AsList();
            }
        }

        //3. Getting articles via category and title
        public IEnumerable<Article> GetArticlesViaCategoryTitle(string category = "", string title = "")
        {
            var sql = @"SELECT A.*, '' AS SplitCol, C.*
                            FROM DxOMark.Article A
                            INNER JOIN DxOMark.ArticleCategory AC ON A.id = AC.ArticleID
                            INNER JOIN DxOMark.Category C ON C.id = AC.CategoryID
                            WHERE (C.name = @category) AND (title LIKE '%' + @title + '%')
                            ORDER BY A.uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var articles = db.Query<Article, Category, Article>(
                   sql,
                   (article, category) =>
                   {
                       article.Category = category;
                       return article;
                   },
                   new { @category = category, @title = title },
                   splitOn: "SplitCol"
                   );
                return articles;
            }
        }

        //4. Getting devices via category, upload date and title
        public IEnumerable<Article> GetArticlesViaCategoryUploadDateTitle(string category = "", DateTime? uploadDate = null, string title = "")
        {
            var sql = @"SELECT A.*, '' AS SplitCol, C.*
                            FROM DxOMark.Article A
                            INNER JOIN DxOMark.ArticleCategory AC ON A.id = AC.ArticleID
                            INNER JOIN DxOMark.Category C ON C.id = AC.CategoryID
                            WHERE (C.name = @category) AND (title LIKE '%' + @title + '%') AND YEAR(A.uploadDate) = YEAR(@uploadDate)
                            ORDER BY A.uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var articles = db.Query<Article, Category, Article>(
                   sql,
                   (article, category) =>
                   {
                       article.Category = category;                       
                       return article;
                   },
                   new { @category = category, @uploadDate = uploadDate, @title = title },
                   splitOn: "SplitCol"
                   );
                return articles;
            }
        }

        //ARTICLES WITH DEVICE

        //1. Get articles via device and title

        public IEnumerable<Article> GetArticlesViaDeviceTitle(string device = "", string title = "")
        {
            string sql = @"SELECT A.*, '' AS SplitCol, D.*
                            FROM DxOMark.Article A                            
                            INNER JOIN DxOMark.ArticleDevice AD ON A.id = AD.ArticleID
                            INNER JOIN DxOMark.Device D ON D.id = AD.DeviceID
                            WHERE (D.name LIKE '%' + @device + '%') AND (title LIKE '%' + @title + '%')
                            ORDER BY A.uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var articles = db.Query<Article, Device, Article>(
                   sql,
                   (article, device) =>
                   {
                       article.Devices = new List<Device> { device };
                       return article;
                   },
                   new { @device = device, @title = title },
                   splitOn: "SplitCol"
                   );
                return articles;
            }
        }

        //2. Get articles via device, upload date and title

        public IEnumerable<Article> GetArticlesViaDeviceUploadDateTitle(string device = "", DateTime? uploadDate = null, string title = "")
        {
            string sql = @"SELECT A.*, '' AS SplitCol, D.*
                            FROM DxOMark.Article A                            
                            INNER JOIN DxOMark.ArticleDevice AD ON A.id = AD.ArticleID
                            INNER JOIN DxOMark.Device D ON D.id = AD.DeviceID
                            WHERE (D.name LIKE '%' + @device + '%') AND (YEAR(uploadDate) = YEAR(@uploadDate)) AND (title LIKE '%' + @title + '%')
                            ORDER BY A.uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var articles = db.Query<Article, Device, Article>(
                   sql,
                   (article, device) =>
                   {
                       article.Devices = new List<Device> { device };
                       return article;
                   },
                   new { @device = device, @uploadDate = uploadDate, @title = title },
                   splitOn: "SplitCol"
                   );
                return articles;
            }
        }

        //3. Get articles via device, category and title

        public IEnumerable<Article> GetArticlesViaDeviceCategoryTitle(string device = "", string category = "", string title = "")
        {
            string sql = @"SELECT A.*, '' AS SplitCol, D.*, '' AS SplitCol, C.*
                            FROM DxOMark.Article A
                            INNER JOIN DxOMark.ArticleCategory AC ON A.id = AC.ArticleID
                            INNER JOIN DxOMark.Category C ON C.id = AC.CategoryID
                            INNER JOIN DxOMark.ArticleDevice AD ON A.id = AD.ArticleID
                            INNER JOIN DxOMark.Device D ON D.id = AD.DeviceID
                            WHERE (D.name LIKE '%' + @device + '%') AND (C.name = @category) AND (title LIKE '%' + @title + '%')
                            ORDER BY A.uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var articles = db.Query<Article, Device, Category, Article>(
                   sql,
                   (article, device, category) =>
                   {
                       article.Devices = new List<Device> { device };
                       article.Category = category;
                       return article;
                   },
                   new { @device = device, @category = category, @title = title },
                   splitOn: "SplitCol"
                   );
                return articles;
            }
        }

        //4. Get articles via device, category, upload date and title
        public IEnumerable<Article> GetArticlesViaDeviceCategoryUploadDateTitle(string device ="", string category = "", DateTime? uploadDate = null, string title = "")
        {
            string sql = @"SELECT A.*, '' AS SplitCol, D.*, '' AS SplitCol, C.*
                            FROM DxOMark.Article A
                            INNER JOIN DxOMark.ArticleCategory AC ON A.id = AC.ArticleID
                            INNER JOIN DxOMark.Category C ON C.id = AC.CategoryID
                            INNER JOIN DxOMark.ArticleDevice AD ON A.id = AD.ArticleID
                            INNER JOIN DxOMark.Device D ON D.id = AD.DeviceID
                            WHERE (D.name LIKE '%' + @device + '%') AND (C.name = @category) AND (title LIKE '%' + @title + '%')";

            if (uploadDate != null) sql += " AND YEAR(A.uploadDate) = YEAR(@uploadDate)";
            sql += "ORDER BY A.uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                 var articles = db.Query<Article, Device, Category, Article>(
                    sql,
                    (article, device, category) =>
                    {                       
                        article.Devices = new List<Device> { device};
                        article.Category= category;                                              
                        return article;
                    },
                    new { @device = device, @category = category, @uploadDate = uploadDate, @title = title  },
                    splitOn: "SplitCol"
                    );
                return articles;               
            }
        }


        //GET ALL ARTICLES
        public IEnumerable<Article> GetArticles()
        {
            string sql = "SELECT * FROM DxOMark.Article ORDER BY uploadDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Article>(sql);
            }
        }

        //Get all articles with FK
        public IEnumerable<Article> GetArticlesFK()
        {
            string sql = @"SELECT A.*, '' AS SplitCol, D.*, '' AS SplitCol, C.*
                            FROM DxOMark.Article A
                            LEFT JOIN DxOMark.ArticleCategory AC ON A.id = AC.ArticleID
                            LEFT JOIN DxOMark.Category C ON C.id = AC.CategoryID
                            LEFT JOIN DxOMark.ArticleDevice AD ON A.id = AD.ArticleID
                            LEFT JOIN DxOMark.Device D ON D.id = AD.DeviceID
                            ORDER BY A.uploadDate DESC, A.id DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var articles = db.Query<Article, Device, Category, Article>(
                   sql,
                   (article, device, category) =>
                   {
                       article.Device = device;
                       article.Category = category;
                       return article;
                   },
                   splitOn: "SplitCol"
                   );
                return articles;
            }
        }

        //GET A DEVICE VIA ID
        public Article GetArticleViaID(int id)
        {
            string sql = "SELECT * FROM DxOMark.Article WHERE id = @id ORDER BY uploadDate DESC";

            var parameters = new { @id = id };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QuerySingleOrDefault<Article>(sql, parameters);
            }
        }


        //INSERT
        public bool InsertArticle(Article article)
        {
            string sql = @"INSERT INTO DxOMark.Article (title, uploadDate, text)
                           OUTPUT INSERTED.* 
                           VALUES (@title, @uploadDate, @text)";

            var parameters = new
            {
                @title = article.Title,
                @uploadDate = article.UploadDate,
                @text = article.Text
            };

            Article newArticle;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                newArticle = db.QuerySingleOrDefault<Article>(sql, parameters);
                if (newArticle == null)
                {
                    return false;
                }
            }

            //adding article category
            string sql2 = @"INSERT INTO DxoMark.ArticleCategory (categoryID, articleID)
                                OUTPUT INSERTED.* 
                                VALUES (@categoryId, @articleId)";

            var parameters2 = new
            {
                @categoryId = article.Category.Id,
                @articleId = newArticle.Id
            };

            Article newArticle2;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                newArticle2 = db.QuerySingleOrDefault<Article>(sql2, parameters2);
                if (newArticle2 == null)
                {
                    return false;
                }
            }

            if (article.Device == null) return true;

            //adding article device
            string sql3 = @"INSERT INTO DxoMark.ArticleDevice (deviceID, articleID)
                                VALUES (@deviceId, @articleId)";

            var parameters3 = new
            {
                @deviceId = article.Device.Id,
                @articleId = newArticle.Id
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql3, parameters3);
                if (affectedRows == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //UPDATE
        public bool UpdateArticle(Article article)
        {
            string sql = @"UPDATE DxOMark.Article SET 
                                            title = @title, 
                                            uploadDate = @uploadDate,
                                            text = @text                                           
                          WHERE id = @articleId";
          
            var parameters = new
            {
                @articleID = article.Id,
                @title = article.Title,
                @uploadDate = article.UploadDate,
                @text = article.Text               
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 0)
                {
                    return false;
                }
            }

            // Update the category into ArticleCategory

            string sql2 = @"UPDATE DxoMark.ArticleCategory SET
                                                categoryID = @categoryId
                                WHERE articleID = @articleId";

            var parameters2 = new
            {
                @articleId = article.Id,
                @categoryId = article.Category.Id
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql2, parameters2);
                if (affectedRows == 0)
                {
                    return false;
                }
            }
            return true;
        }


        //DELETE
        public bool DeleteArticle(int articleID)
        {
            string sql = @"DELETE FROM DxOMark.Article 
                           WHERE id = @articleID";

            var parameters = new
            {
                @articleID = articleID
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows >= 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
