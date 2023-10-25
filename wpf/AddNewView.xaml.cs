using dxOMark_dal;
using dxOMark_dal.interfaces;
using dxOMark_models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
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
    /// Interaction logic for AddNewView.xaml
    /// </summary>
    public partial class AddNewView : UserControl
    {
        private IDevicesRepository devicesRepository = new DevicesRepository();
        private IBrandsRepository brandsRepository = new BrandsRepository();
        private IArticlesRepository articlesRepository = new ArticlesRepository();
        private ICategoriesRepository categoriesRepository = new CategoriesRepository();
        private IFunctionalitiesRepository functionalitiesRepository = new FunctionalitiesRepository();
        private IScoresRepository scoresRepository= new ScoresRepository();

        public AddNewView()
        {
            InitializeComponent();

            //ComboBoxes Filled
            cmbChooseBrand.ItemsSource = brandsRepository.GetBrands();
            cmbChooseCategory.ItemsSource = categoriesRepository.GetCategories();
            cmbChooseArticleDevice.ItemsSource = devicesRepository.GetDevices();
            cmbChooseDevice.ItemsSource = devicesRepository.GetDevices();
            cmbChooseFunctionality.ItemsSource = functionalitiesRepository.GetFunctionalities();

            //DataGrid Filled
            dgArticles.ItemsSource = articlesRepository.GetArticlesFK();
            dgBrands.ItemsSource = brandsRepository.GetBrands();
            dgCategories.ItemsSource = categoriesRepository.GetCategories();
            dgFunctionalities.ItemsSource = functionalitiesRepository.GetFunctionalities();
            dgDevices.ItemsSource = devicesRepository.GetDevicesViaName();
            dgScores.ItemsSource = scoresRepository.GetScores();
        }

        //BRAND
        //Insert the brand
        private void btnAddBrand_Click(object sender, RoutedEventArgs e)
        {
            SaveBrand(true);
            dgBrands.ItemsSource = brandsRepository.GetBrands();
            cmbChooseBrand.ItemsSource = brandsRepository.GetBrands();

        }
        private void SaveBrand(bool isInsert)
        {
            try
            {
                Brand brand = new Brand() { Name = txtAddBrand.Text };
                if (brand.IsValid())
                {
                    if (!ValidateBrandExists(brand))
                    {
                        brandsRepository.InsertBrand(brand);
                        MessageBox.Show("Succesfully added");
                    }
                    else
                    {
                        throw new Exception("The brand already exists!");
                    }
                }
                else
                {
                    throw new Exception(brand.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            txtAddBrand.Text = "";
        }

        //Validate the brand
        private bool ValidateBrandExists(Brand brand)
        {
            return brandsRepository.GetBrands().Contains(brand);
        }


        //CATEGORY
        //Insert the category
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            SaveCategory(true);
            dgCategories.ItemsSource = categoriesRepository.GetCategories();
            cmbChooseCategory.ItemsSource = categoriesRepository.GetCategories();
        }

        private void SaveCategory(bool isInsert)
        {
            try
            {
                Category category = new Category() { Name = txtAddCategory.Text };
                if (category.IsValid())
                {
                    if (!ValidateCategoryExists(category))
                    {
                        categoriesRepository.InsertCategory(category);
                        MessageBox.Show("Succesfully added");
                    }
                    else
                    {
                        throw new Exception("The category already exists!");
                    }
                }
                else
                {
                    throw new Exception(category.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            txtAddCategory.Text = "";            
        }

        //Validate the category
        private bool ValidateCategoryExists(Category category)
        {
            return categoriesRepository.GetCategories().Contains(category);
        }

        //Delete the category
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCategories.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Confirm deleting", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (categoriesRepository.DeleteCategory(((Category)dgCategories.SelectedItem).Id))
                    {
                        MessageBox.Show("The category was removed");
                        dgCategories.ItemsSource = categoriesRepository.GetCategories();
                        cmbChooseCategory.ItemsSource = categoriesRepository.GetCategories();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to choose a category!");
            }          
        }


        //FUNCTIONALITY
        //Insert the functionality

        private void btnAddFunctionality_Click(object sender, RoutedEventArgs e)
        {
            SaveFunctionality(true);
            dgFunctionalities.ItemsSource = functionalitiesRepository.GetFunctionalities();
            cmbChooseFunctionality.ItemsSource = functionalitiesRepository.GetFunctionalities();
        }

        private void SaveFunctionality(bool isInsert)
        {           
            try
            {
                Functionality functionality = new Functionality() { Name = txtAddFunctionality.Text};
                if (functionality.IsValid())
                {
                    if (!ValidateFunctionalityExists(functionality))
                    {
                        functionalitiesRepository.InsertFunctionality(functionality);
                        MessageBox.Show("Succesfully added");
                    }
                    else
                    {
                        throw new Exception("The functionality already exists!");
                    }
                }
                else
                {
                    throw new Exception(functionality.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            txtAddFunctionality.Text = "";
            
        }

        //Validate the functionality

        private bool ValidateFunctionalityExists(Functionality functionality)
        {
            return functionalitiesRepository.GetFunctionalities().Contains(functionality);
        }

        //Delete the functionality
        private void btnDeleteFunctionality_Click(object sender, RoutedEventArgs e)
        {
            if (dgFunctionalities.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Confirm deleting", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (functionalitiesRepository.DeleteFunctionality(((Functionality)dgFunctionalities.SelectedItem).Id))
                    {
                        MessageBox.Show("The functionality was removed");
                        dgFunctionalities.ItemsSource = functionalitiesRepository.GetFunctionalities();
                        cmbChooseFunctionality.ItemsSource = functionalitiesRepository.GetFunctionalities();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to choose a functionality!");
            }
        }

        //ARTICLE
        //Insert the article

        private void btnAddArticle_Click(object sender, RoutedEventArgs e)
        {
            SaveArticle(true);
            dgArticles.ItemsSource = articlesRepository.GetArticlesFK();
        }

        private void SaveArticle(bool isInsert)
        {
            try
            {
                Article article = new Article() { Title = txtAddTitle.Text, Text = txtAddText.Text};
                if (article.IsValid())
                {
                    if (!ValidateArticleExists(article))
                    {
                        ValidateArticleText();
                        ValidateArticleCategory();
                        article.UploadDate = DateTime.Now;
                        article.Category = (Category)cmbChooseCategory.SelectedItem;
                        article.Device = cmbChooseArticleDevice.SelectedIndex != -1 ? (Device)cmbChooseArticleDevice.SelectedItem : null;
                        articlesRepository.InsertArticle(article);
                        MessageBox.Show("Succesfully added");
                    }
                    else
                    {
                        throw new Exception("The article already exists!");
                    }
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

        //Validate the article

        private bool ValidateArticleExists(Article article)
        {
            return articlesRepository.GetArticles().Contains(article);
        }
        private void ValidateArticleText()
        {
            if (string.IsNullOrWhiteSpace(txtAddText.Text))
            {
                throw new Exception("Text is a required field!");
            }
        }
        private void ValidateArticleCategory()
        {
            if (cmbChooseCategory.SelectedItem == null)
            {
                throw new Exception("Choosing the category in required!");
            }
        }


        //Reset the article form
        private void ResetArticleForm()
        {
            txtAddTitle.Text = "";
            txtAddText.Text = "";
            cmbChooseArticleDevice.SelectedItem = null;
            cmbChooseBrand.SelectedItem = null;
            cmbChooseCategory.SelectedItem = null;
        }

        //DEVICE
        //Insert the device

        private void btnAddDevice_Click_1(object sender, RoutedEventArgs e)
        {
            SaveDevice(true);
            dgDevices.ItemsSource = devicesRepository.GetDevices();
            cmbChooseDevice.ItemsSource = devicesRepository.GetDevices();
            cmbChooseArticleDevice.ItemsSource = devicesRepository.GetDevices();
        }

        private void SaveDevice(bool isInsert)
        {
            try
            {
                Device device = new Device() { Name = txtAddName.Text };

                if (device.IsValid())
                {                   
                    if (!ValidateDeviceExists(device))
                    {
                        ValidateDeviceLaunchDate();
                        ValidateDeviceLaunchPrice();
                        ValidateDeviceImage();
                        ValidateDeviceBrand();

                        device.LaunchDate = (DateTime)dpAddLaunchDate.SelectedDate;
                        device.LaunchPrice = (decimal)decimal.Parse(txtAddLaunchPrice.Text);
                        device.Image = txtAddImage.Text;
                        device.Review = txtAddReview.Text;
                        device.BrandId = (int)cmbChooseBrand.SelectedValue;

                        devicesRepository.InsertDevice(device);
                        MessageBox.Show("Succesfully added");
                    }
                    else
                    {
                        throw new Exception("The device already exists!");
                    }

                }
                else
                {
                    throw new Exception(device.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            ResetDeviceForm();

        }

        //Validate the device

        private bool ValidateDeviceExists(Device device)
        {
            return devicesRepository.GetDevices().Contains(device);
        }
        private void ValidateDeviceLaunchDate()
        {
            if (!dpAddLaunchDate.SelectedDate.HasValue)
            {
                throw new Exception("Launch date has to be a valid date!");
            }
        }
        private void ValidateDeviceImage()
        {
            if (string.IsNullOrWhiteSpace(txtAddImage.Text))
            {
                throw new Exception("Image is a required field!");
            }
        }
        private void ValidateDeviceLaunchPrice()
        {
            if (!decimal.TryParse(txtAddLaunchPrice.Text, out _))
            {
                throw new Exception("Launch price should be a valid number!");
            }
        }
        private void ValidateDeviceBrand()
        {
            if (cmbChooseBrand.SelectedItem == null)
            {
                throw new Exception("Choosing the brand in required!");
            }
        }

        //Reset the device form

        private void ResetDeviceForm()
        {
            txtAddName.Text = "";
            dpAddLaunchDate.Text = "";
            txtAddLaunchPrice.Text = "";
            txtAddImage.Text = "";
            txtAddReview.Text = "";
            cmbChooseBrand.SelectedItem = null;
        }

        //DEVICE FUNCTIONALITY (SCORE)

        private void btnAddScore_Click(object sender, RoutedEventArgs e)
        {
            SaveScore(true);
            dgScores.ItemsSource = scoresRepository.GetScores();
        }

        private void SaveScore(bool isInsert)
        {
            try
            {
                DeviceFunctionality score = new DeviceFunctionality();
                if (score.IsValid())
                {
                    ValidateScoreScore();
                    ValidateScoreDevice();
                    ValidateScoreFunctionality();

                    score.Score = double.Parse(txtAddScore.Text);
                    score.FunctionalityId = (int)cmbChooseFunctionality.SelectedValue;
                    score.DeviceId = (int)cmbChooseDevice.SelectedValue;

                    if (!ValidateScoreExists(score))
                    {
                        scoresRepository.InsertScore(score);
                        MessageBox.Show("Succesfully added");
                    }
                    else
                    {
                        throw new Exception("The device already has a score for this functionality!");
                    }
                }
                else
                {
                    throw new Exception(score.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            ResetScoreForm();           
        }

        //Validate the score

        private bool ValidateScoreExists(DeviceFunctionality score)
        {
            return scoresRepository.GetScores().Contains(score);
        }
        private void ValidateScoreScore()
        {
            if (!decimal.TryParse(txtAddScore.Text, out _))
            {
                throw new Exception("Score should be a valid number!");
            }
        }
        private void ValidateScoreDevice()
        {
            if (cmbChooseDevice.SelectedItem == null)
            {
                throw new Exception("Choosing the device in required!");
            }
        }
        private void ValidateScoreFunctionality()
        {
            if (cmbChooseFunctionality.SelectedItem == null)
            {
                throw new Exception("Choosing the functionality in required!");
            }
        }

        //Reset the score form

        private void ResetScoreForm()
        {
            txtAddScore.Text = "";
            cmbChooseFunctionality.SelectedItem = null;
            cmbChooseDevice.SelectedItem= null;
        }
    }
}
