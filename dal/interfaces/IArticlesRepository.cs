using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using dxOMark_models;

namespace dxOMark_dal
{
    public interface IArticlesRepository
    {
        //StartView Latest Articles
        public ICollection<Article> GetTop3Articles();

        //Get Articles
        public List<Article> GetArticlesViaTitle(string title = "");
        public List<Article> GetArticlesViaUploadDateTitle(DateTime? uploadDate = null, string title = "");
        public IEnumerable<Article> GetArticlesViaCategoryTitle(string category = "", string title = "");
        public IEnumerable<Article> GetArticlesViaCategoryUploadDateTitle(string category = "", DateTime? uploadDate = null, string title = "");


        public IEnumerable<Article> GetArticlesViaDeviceTitle(string device = "", string title = "");
        public IEnumerable<Article> GetArticlesViaDeviceUploadDateTitle(string device = "", DateTime? uploadDate = null, string title = "");
        public IEnumerable<Article> GetArticlesViaDeviceCategoryTitle(string device = "", string category = "", string title = "");
        public IEnumerable<Article> GetArticlesViaDeviceCategoryUploadDateTitle(string device = "", string category = "", DateTime? uploadDate = null, string title = "");


        public IEnumerable<Article> GetArticles();
        public IEnumerable<Article> GetArticlesFK();
        public Article GetArticleViaID(int id);

        //Insert/Update/Delete

        public bool InsertArticle(Article article);
        public bool UpdateArticle(Article article);
        public bool DeleteArticle(int articleID);









    }
}
