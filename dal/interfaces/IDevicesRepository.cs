using System;
using System.Collections.Generic;
using System.Text;
using dxOMark_models;

namespace dxOMark_dal
{
    public interface IDevicesRepository
    {
        //StartView Devices
        public IEnumerable<Device> GetSmartphones();
        public IEnumerable<Device> GetCameras();
        public IEnumerable<Device> GetSpeakers();

        //Get Devices
        public IEnumerable<Device> GetDevicesViaName(string brand= "", string name = "");
        public IEnumerable<Device> GetDevicesViaNameLaunchDate(string brand = "", string name = "", DateTime? launchDate = null);
        public IEnumerable<Device> GetDevicesViaNameLaunchPrice(string brand = "", string name = "", decimal launchPrice = 0);
        public IEnumerable<Device> GetDevicesViaNameLaunchPriceLaunchDate(string brand = "", string name = "", decimal launchPrice = 0, DateTime? launchDate = null);


        public IEnumerable<Device> GetDevicesViaBrandName(string brand = "", string name = "");
        public IEnumerable<Device> GetDevicesViaBrandNameLaunchDate(string brand = "", string name = "", DateTime? launchDate = null);
        public IEnumerable<Device> GetDevicesViaBrandNameLaunchPrice(string brand = "", string name = "", decimal launchPrice = 0);
        public IEnumerable<Device> GetDevicesViaBrandNameLaunchPriceLaunchDate(string brand = "", string name = "", decimal launchPrice = 0, DateTime? launchDate = null);

        public IEnumerable<Device> GetDevices();
        public Device GetDeviceViaID(int id);
        public IEnumerable<Device> GetDevicesJoins();

        //Insert/Update/Delete
        public bool InsertDevice(Device device);
        public bool UpdateDevice(Device device);
        public bool DeleteDevice(int deviceID);
        
    }
}
