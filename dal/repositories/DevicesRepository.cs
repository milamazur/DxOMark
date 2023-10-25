using Dapper;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Xml.Linq;

namespace dxOMark_dal
{
    public class DevicesRepository : BaseRepository, IDevicesRepository
    {

        //Get smartphones for startView
        public IEnumerable<Device> GetSmartphones()
        {
            var sql = @"SELECT D.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE F.name = 'Overall Camera Score'
                            ORDER BY DF.score DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, deviceFunctionality, functionality) =>
                   {

                       //device has a lot of scores
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //Get cameras for startView
        public IEnumerable<Device> GetCameras()
        {
            var sql = @"SELECT D.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE F.name = 'Overall Sensor Score'
                            ORDER BY DF.score DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, deviceFunctionality, functionality) =>
                   {
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //Get speakers for startView
        public IEnumerable<Device> GetSpeakers()
        {
            var sql = @"SELECT D.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE F.name = 'Overall Speaker Score'
                            ORDER BY DF.score DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, deviceFunctionality, functionality) =>
                   {
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }



        //SELECT DEVICES WITHOUT BRAND
        //1. Getting devices via name

        public IEnumerable<Device> GetDevicesViaName(string brand = "", string name = "")
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE D.name LIKE '%' + @name + '%'
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       //device has one brand
                       device.Brand = brand;
                       //device has a lot of scores
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //2.Getting devices via name and launch date
        public IEnumerable<Device> GetDevicesViaNameLaunchDate(string brand = "", string name = "", DateTime? launchDate = null)
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE 
                                D.name LIKE '%' + @name + '%' 
                                AND YEAR(D.launchDate) = YEAR(@launchDate)
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name, @launchDate = launchDate },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //3. Getting devices via name and launch price
        public IEnumerable<Device> GetDevicesViaNameLaunchPrice(string brand = "", string name = "", decimal launchPrice = 0)
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE 
                                D.name LIKE '%' + @name + '%' 
                                AND D.launchPrice = @launchPrice
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name, @launchPrice = launchPrice },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //4. Getting devices via name, launch price and launch date
        public IEnumerable<Device> GetDevicesViaNameLaunchPriceLaunchDate(string brand = "", string name = "", decimal launchPrice = 0, DateTime? launchDate = null)
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE 
                                D.name LIKE '%' + @name + '%'";

            if (launchPrice != 0 && launchPrice > 0) sql += " AND D.launchPrice = @launchPrice";
            if (launchDate != null) sql += " AND YEAR(D.launchDate) = YEAR(@launchDate)";

            sql += "ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name, @launchPrice = launchPrice, @launchDate = launchDate },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }


        //SELECT DEVICES WITH BRAND
        //1. Getting devices via brand and name

        public IEnumerable<Device> GetDevicesViaBrandName(string brand = "", string name = "")
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE 
                                B.name = @brand 
                                AND D.name LIKE '%' + @name + '%'
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //2. Getting devices via brand, name and launch date
        public IEnumerable<Device> GetDevicesViaBrandNameLaunchDate(string brand = "", string name = "", DateTime? launchDate = null)
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE 
                                B.name = @brand 
                                AND D.name LIKE '%' + @name + '%' 
                                AND YEAR(D.launchDate) = YEAR(@launchDate)
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name, @launchDate = launchDate },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        // 3. Getting devices via brand, name and launch price
        public IEnumerable<Device> GetDevicesViaBrandNameLaunchPrice(string brand = "", string name = "", decimal launchPrice = 0)
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE 
                                B.name = @brand 
                                AND D.name LIKE '%' + @name + '%' 
                                AND D.launchPrice = @launchPrice
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name, @launchPrice = launchPrice },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        // 4. Getting devices via brand, name, launch price and launch date
        public IEnumerable<Device> GetDevicesViaBrandNameLaunchPriceLaunchDate(string brand = "", string name = "", decimal launchPrice = 0, DateTime? launchDate = null)
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            WHERE D.name LIKE '%' + @name + '%'";

 
            if (launchPrice != 0 && launchPrice > 0) sql += " AND D.launchPrice = @launchPrice";
            if (launchDate != null) sql += " AND YEAR(D.launchDate) = YEAR(@launchDate)";

            sql += "ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },
                   new { @brand = brand, @name = name,@launchPrice = launchPrice, @launchDate = launchDate },
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }




        //GET ALL DEVICES
        public IEnumerable<Device> GetDevices()
        {
            string sql = "SELECT * FROM DxOMark.Device ORDER BY id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Device>(sql);
            }
        }

        //GET ALL DEVICES WITH JOINS

        public IEnumerable<Device> GetDevicesJoins()
        {
            var sql = @"SELECT D.*, '' AS SplitCol, B.*, '' AS SplitCol, DF.*, '' AS SplitCol, F.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID
                            LEFT JOIN DxOMark.DeviceFunctionality DF ON D.id = DF.deviceID
                            LEFT JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            ORDER BY D.launchDate DESC";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var devices = db.Query<Device, Brand, DeviceFunctionality, Functionality, Device>(
                   sql,
                   (device, brand, deviceFunctionality, functionality) =>
                   {
                       device.Brand = brand;
                       device.DeviceFunctionalities = new List<DeviceFunctionality>() { deviceFunctionality };
                       deviceFunctionality.Functionality = functionality;
                       return device;
                   },                  
                   splitOn: "SplitCol"
                   );
                return GroupDevicesWithScores(devices);
            }
        }

        //GET A DEVICE VIA ID
        public Device GetDeviceViaID(int id)
        {
            string sql = @"SELECT D.*, '' AS SplitCol, B.*
                            FROM DxOMark.Device D
                            INNER JOIN DxOMark.Brand B ON B.id = D.brandID                      
                            WHERE D.id = @id";
            var parameters = new { @id = id };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Device, Brand, Device>(
                   sql,
                   (device, brand) =>
                   {
                       device.Brand = brand;
                       return device;
                   },
                   parameters,
                   splitOn: "SplitCol"
                   ).SingleOrDefault();
            }
        }

        //GROUP DEVICES WITH SCORES
        private static IEnumerable<Device> GroupDevicesWithScores(IEnumerable<Device> devices)
        {
            var grouped = devices.GroupBy(device => device.Name);

            List<Device> devicesWithScores = new List<Device>();

            foreach (var group in grouped)
            {
                var device = group.First();
                List<DeviceFunctionality> allDeviceFunctionalities = new List<DeviceFunctionality>();

                foreach (var d in group)
                {
                    allDeviceFunctionalities.Add(d.DeviceFunctionalities.First());
                }

                device.DeviceFunctionalities = allDeviceFunctionalities;
                devicesWithScores.Add(device);
            }

            return devicesWithScores;
        }

        //INSERT DEVICE
        public bool InsertDevice(Device device)
        {
            string sql = @"INSERT INTO DxOMark.Device (name, launchPrice, launchDate, review, image, brandID )
                           OUTPUT INSERTED.*
                           VALUES (@name, @launchPrice, @launchDate, @review, @image, @brandID)";

            var parameters = new
            {
                @name = device.Name,
                @launchPrice = device.LaunchPrice,
                @launchDate = device.LaunchDate,               
                @review = device.Review,
                @image = device.Image,
                @brandID = device.BrandId
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

        //UPDATE DEVICE
        public bool UpdateDevice(Device device)
        {
            string sql = @"UPDATE DxOMark.Device SET name = @name,
                                            launchPrice = @launchPrice,
                                            launchDate = @launchDate,
                                            brandID = @brandID
                                    WHERE id = @deviceID";

            var parameters = new
            {
                @deviceID = device.Id,
                @name = device.Name,
                @launchPrice = device.LaunchPrice,
                @launchDate = device.LaunchDate,
                @brandID = device.BrandId

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

        //DELETE DEVICE
        public bool DeleteDevice(int deviceID)
        {
            string sql = @"DELETE FROM DxOMark.Device 
                           WHERE id = @deviceID";

            var parameters = new
            {
                @deviceID = deviceID
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
