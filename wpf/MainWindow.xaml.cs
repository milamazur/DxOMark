using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dxOMark_models;
using dxOMark_dal;

namespace dxOMark_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //SELECT A NEW TAB (MENU)
        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedTab = (tabControl.SelectedItem as TabItem).Name;
            switch (selectedTab)
            {
                case "tabStart":
                    ContentWindow.Content = new StartView();
                    break;
                case "tabDevices":
                    ContentWindow.Content = new DevicesView();
                    break;
                case "tabArticles":
                    ContentWindow.Content = new ArticlesView();
                    break;
                case "tabAddNew":
                    ContentWindow.Content = new AddNewView();
                    break;
            }
        }

    }
}
