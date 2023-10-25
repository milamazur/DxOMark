using dxOMark_dal;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace dxOMark_wpf
{
    /// <summary>
    /// Interaction logic for DevicesView.xaml
    /// </summary>
    public partial class DevicesView : UserControl
    {
        private IDevicesRepository devicesRepository = new DevicesRepository();
        private IBrandsRepository brandsRepository = new BrandsRepository();
        public DevicesView()
        {
            InitializeComponent();
            cmbBrands.ItemsSource = brandsRepository.GetBrands();
            dgData.ItemsSource = devicesRepository.GetDevicesViaName();
        }

        //SELECT DEVICES
        private void btnDevices_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBrands.SelectedItem is Brand brand)
            {
                string brandName = brand.Name;
                if (string.IsNullOrWhiteSpace(txtLaunchPrice.Text))
                {
                    if (string.IsNullOrWhiteSpace(dpLaunchDate.Text))
                    {
                        string name = txtName.Text;
                        dgData.ItemsSource = devicesRepository.GetDevicesViaBrandName(brandName, name);
                        
                    }
                    else
                    {
                        DateTime launchDate = Convert.ToDateTime(dpLaunchDate.Text);
                        string name = txtName.Text;
                        dgData.ItemsSource = devicesRepository.GetDevicesViaBrandNameLaunchDate(brandName, name, launchDate);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dpLaunchDate.Text))
                    {
                        string name = txtName.Text;
                        decimal launchPrice = Convert.ToDecimal(txtLaunchPrice.Text);
                        dgData.ItemsSource = devicesRepository.GetDevicesViaBrandNameLaunchPrice(brandName, name, launchPrice);
                    }
                    else
                    {
                        DateTime launchDate = Convert.ToDateTime(dpLaunchDate.Text);
                        string name = txtName.Text;
                        decimal launchPrice = Convert.ToDecimal(txtLaunchPrice.Text);
                        dgData.ItemsSource = devicesRepository.GetDevicesViaBrandNameLaunchPriceLaunchDate(brandName, name, launchPrice, launchDate);
                    }
                }

            }
            else
            {
                string brandName = "";
                if (string.IsNullOrWhiteSpace(txtLaunchPrice.Text))
                {
                    if (string.IsNullOrWhiteSpace(dpLaunchDate.Text))
                    {
                        string name = txtName.Text;
                        dgData.ItemsSource = devicesRepository.GetDevicesViaName(brandName, name);
                    }
                    else
                    {
                        DateTime launchDate = Convert.ToDateTime(dpLaunchDate.Text);
                        string name = txtName.Text;
                        dgData.ItemsSource = devicesRepository.GetDevicesViaNameLaunchDate(brandName, name, launchDate);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dpLaunchDate.Text))
                    {
                        string name = txtName.Text;
                        decimal launchPrice = Convert.ToDecimal(txtLaunchPrice.Text);
                        dgData.ItemsSource = devicesRepository.GetDevicesViaNameLaunchPrice(brandName, name, launchPrice);
                    }
                    else
                    {
                        DateTime launchDate = Convert.ToDateTime(dpLaunchDate.Text);
                        string name = txtName.Text;
                        decimal launchPrice = decimal.Parse(txtLaunchPrice.Text);
                        dgData.ItemsSource = devicesRepository.GetDevicesViaNameLaunchPriceLaunchDate(brandName, name, launchPrice, launchDate);
                    }
                }
            }
            txtName.IsReadOnly = false;
            lblInfo.Content = null;
            ResetDeviceForm();
        }


        //Set the data 
        private void SetData(int deviceId)
        {
            Device device = devicesRepository.GetDeviceViaID(deviceId);

            if (device != null)
            {
                txtId.Text = device.Id.ToString();
                txtName.Text = device.Name;
                dpLaunchDate.Text = device.LaunchDate.ToShortDateString();
                txtLaunchPrice.Text = device.LaunchPrice.ToString();
                cmbBrands.SelectedItem = device.Brand;
            }
        }

        //Select the data from DataGrid
        private void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dgData.SelectedItem;
            if (selectedItem is Device)
            {
                var device = (Device)selectedItem;
                lblInfo.Content = device.DeviceInfo();
                SetData(device.Id);
                txtName.IsReadOnly = true;
            }
        }


        //UPDATE THE DEVICE
        private void btnDevicesEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Device device = new Device() { Name = txtName.Text};

                if (device.IsValid())
                {
                    ValidateDeviceLaunchDate();
                    ValidateDeviceLaunchPrice();

                    device.Id = (int)int.Parse(txtId.Text);
                    device.LaunchDate = (DateTime)dpLaunchDate.SelectedDate;
                    device.LaunchPrice = (decimal)decimal.Parse(txtLaunchPrice.Text);
                    device.BrandId = (int)cmbBrands.SelectedIndex + 1;

                    devicesRepository.UpdateDevice(device);
                    MessageBox.Show("Succesfully edited");
                    dgData.ItemsSource = devicesRepository.GetDevicesJoins();
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
            dgData.UnselectAll();           
        }


        //Validate the device

        private void ValidateDeviceLaunchDate()
        {
            if (!dpLaunchDate.SelectedDate.HasValue)
            {
                throw new Exception("Launch date has to be a valid date!");
            }
        }
        private void ValidateDeviceLaunchPrice()
        {
            if (!decimal.TryParse(txtLaunchPrice.Text, out _))
            {
                throw new Exception("Launch price should be a valid number!");
            }
        }

        //Reset the form
        private void ResetDeviceForm()
        {
            txtId.Text = "";
            txtName.Text = "";
            dpLaunchDate.Text = "";
            txtLaunchPrice.Text = "";
            cmbBrands.SelectedItem = null;
            lblInfo.Content = null;
        }


        //DELETE THE DEVICE
        private void btnDevicesDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Confirm deleting", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (devicesRepository.DeleteDevice(((Device)dgData.SelectedItem).Id))
                    {
                        MessageBox.Show("The device was removed");
                        dgData.ItemsSource = devicesRepository.GetDevicesViaName();
                        ResetDeviceForm();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to choose a device!");
            }
        }
    }
}
