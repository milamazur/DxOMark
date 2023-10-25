using dxOMark_dal;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace dxOMark_wpf
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        private IArticlesRepository articlesRepository = new ArticlesRepository();
        private IDevicesRepository devicesRepository = new DevicesRepository();
        private List <Article> top3Articles= new List <Article> ();

        public StartView()
        {
            InitializeComponent();

            top3Articles = articlesRepository.GetTop3Articles().ToList();

            if(top3Articles.Count > 2)
            {
                txtNewArticleFirst.Text = top3Articles[0].Title;
                txtNewArticleSecond.Text = top3Articles[1].Title;
                txtNewArticleThird.Text = top3Articles[2].Title;
            }

            dgDataSmartphones.ItemsSource = devicesRepository.GetSmartphones();
            dgDataCameras.ItemsSource = devicesRepository.GetCameras();
            dgDataSpeakers.ItemsSource = devicesRepository.GetSpeakers();
        }


        //MOST RECENT ARTICLE 1.
        private void imgNewest_Click(object sender, RoutedEventArgs e)
        {
            if (top3Articles.Count > 0)
            {
                articleVisibility();
                txtNewArticle.Text = top3Articles[0].ArticleInfo();
            }            
        }

        //MOST RECENT ARTICLE 2.
        private void imgSecondNewest_Click_1(object sender, RoutedEventArgs e)
        {
            if (top3Articles.Count > 1)
            {
                articleVisibility();
                txtNewArticle.Text = top3Articles[1].ArticleInfo();
            }            
        }

        //MOST RECENT ARTICLE 3.
        private void imgThirdNewest_Click(object sender, RoutedEventArgs e)
        {
            if (top3Articles.Count > 2)
            {
                articleVisibility();
                txtNewArticle.Text = top3Articles[2].ArticleInfo();
            }           
        }

        //METHOD TO CHANGE VISIBILITY OF ARTICLES

        private void articleVisibility()
        {
            spArticles.Visibility = Visibility.Hidden;
            spArticlesSecond.Visibility = Visibility.Hidden;
            spArticlesThird.Visibility = Visibility.Hidden;
            txtNewArticle.Visibility = Visibility.Visible;
        }

    }
}
