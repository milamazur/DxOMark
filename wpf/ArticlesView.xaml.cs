using dxOMark_dal;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace dxOMark_wpf
{
    /// <summary>
    /// Interaction logic for ArticlesView.xaml
    /// </summary>
    public partial class ArticlesView : UserControl
    {
        private IArticlesRepository articlesRepository = new ArticlesRepository();
        private ICategoriesRepository categoriesRepository = new CategoriesRepository();

        public ArticlesView()
        {
            InitializeComponent();
            cmbCategories.ItemsSource = categoriesRepository.GetCategories();
            lbData.ItemsSource = articlesRepository.GetArticlesFK();            
        }     

        
        //SELECT ARTICLES
        private void btnArticles_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDevice.Text))
            {
                if (cmbCategories.SelectedItem is Category category)
                {
                    string categoryName = category.Name;

                    if (string.IsNullOrWhiteSpace(dpUploadDate.Text))
                    {
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaCategoryTitle(categoryName, title);
                    }
                    else
                    {
                        DateTime uploadDate = Convert.ToDateTime(dpUploadDate.Text);
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaCategoryUploadDateTitle(categoryName, uploadDate, title);
                    }

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dpUploadDate.Text))
                    {
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaTitle(title);
                    }
                    else
                    {
                        DateTime uploadDate = Convert.ToDateTime(dpUploadDate.Text);
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaUploadDateTitle(uploadDate, title);
                    }
                }
            }
            else
            {
                string device = txtDevice.Text;
                if (cmbCategories.SelectedItem is Category category)
                {
                    string categoryName = category.Name;

                    if (string.IsNullOrWhiteSpace(dpUploadDate.Text))
                    {
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaDeviceCategoryTitle(device, categoryName, title);
                    }
                    else
                    {
                        DateTime uploadDate = Convert.ToDateTime(dpUploadDate.Text);
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaDeviceCategoryUploadDateTitle(device, categoryName, uploadDate, title);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dpUploadDate.Text))
                    {
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaDeviceTitle(device, title);
                    }
                    else
                    {
                        DateTime uploadDate = Convert.ToDateTime(dpUploadDate.Text);
                        string title = txtTitle.Text;
                        lbData.ItemsSource = articlesRepository.GetArticlesViaDeviceUploadDateTitle(device, uploadDate, title);
                    }
                }

            }
            txtInfo.Text = "";
            ResetArticleForm();
        }


        //Viewing a selected article's data and setting the data of this specific article in the form
        private void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lbData.SelectedItem;
            if (selectedItem is Article)
            {
                var article = (Article)selectedItem;
                //Setting the data
                txtId.Text = article.Id.ToString();
                txtTitle.Text = article.Title;
                dpUploadDate.Text = article.UploadDate.ToShortDateString();
                txtText.Text = article.Text;
                cmbCategories.SelectedItem = article.Category;
                txtInfo.Text = article.ArticleInfo();
                txtDevice.IsReadOnly = true;
            }
        }


        //UPDATE THE ARTICLE
        private void btnArticlesEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Article article = new Article() { Title = txtTitle.Text};

                if (article.IsValid())
                {
                    ValidateUploadDate();
                    ValidateArticleText();

                    article.Id = (int)int.Parse(txtId.Text);
                    article.Title = txtTitle.Text;
                    article.UploadDate = (DateTime)dpUploadDate.SelectedDate;
                    article.Text = txtText.Text;                   
                    article.Category = (Category)cmbCategories.SelectedItem;

                    articlesRepository.UpdateArticle(article);
                    MessageBox.Show("Succesfully edited");
                    lbData.ItemsSource = articlesRepository.GetArticlesFK();
                }
                else
                {
                    throw new Exception(article.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            ResetArticleForm();

        }

        //Reset the Articles form
        private void ResetArticleForm()
        {
            txtId.Text = "";
            txtTitle.Text = "";
            dpUploadDate.Text = "";
            txtText.Text = "";
            txtInfo.Text = "";
            cmbCategories.SelectedItem = null;
        }

        //Validate the article
        private void ValidateUploadDate()
        {
            if (!dpUploadDate.SelectedDate.HasValue)
            {
                throw new Exception("Upload date has to be a valid date!");
            }
        }
        private void ValidateArticleText()
        {
            if (string.IsNullOrWhiteSpace(txtText.Text))
            {
                throw new Exception("Text is a required field!");
            }
        }

        //DELETE ARTICLE
        private void btnArticlesDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbData.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Confirm deleting", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (articlesRepository.DeleteArticle(((Article)lbData.SelectedItem).Id))
                    {
                        MessageBox.Show("The article was removed");
                        lbData.ItemsSource = articlesRepository.GetArticlesFK();
                        ResetArticleForm();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to choose an article!");
            }
        }
    }
}

    

