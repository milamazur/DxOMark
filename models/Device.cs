using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace dxOMark_models
{
    public class Device : BaseClass
    {
        //Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal LaunchPrice { get; set; }
        public DateTime LaunchDate { get; set; }
        public string Review { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }

        //Foregin Keys
        public int BrandId { get; set; }

        //Navigation Properties
        public Brand Brand { get; set; }
        public Article Article { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<DeviceFunctionality> DeviceFunctionalities { get; set; }
        public Functionality Functionality { get; set; }

        //Get only properties
        public double SmartphoneScore
        {
            get
            {
                if (DeviceFunctionalities != null)
                {
                    foreach (var deviceFunctionality in DeviceFunctionalities)
                    {
                        {
                            if (deviceFunctionality.FunctionalityId == 30)
                            {
                                return deviceFunctionality.Score;
                            }
                            else
                            {
                                return 0;
                            }

                        }
                    }
                }
                return 0;
            }
        }
        public double CameraScore
        {
            get
            {
                if (DeviceFunctionalities != null)
                {
                    foreach (var deviceFunctionality in DeviceFunctionalities)
                    {
                        {
                            if (deviceFunctionality.FunctionalityId == 26)
                            {
                                return deviceFunctionality.Score;
                            }
                            else
                            {
                                return 0;
                            }

                        }
                    }
                }
                return 0;
            }
        }
        public double SpeakerScore
        {
            get
            {
                if (DeviceFunctionalities != null)
                {
                    foreach (var deviceFunctionality in DeviceFunctionalities)
                    {
                        {
                            if (deviceFunctionality.FunctionalityId == 31)
                            {
                                return deviceFunctionality.Score;
                            }
                            else
                            {
                                return 0;
                            }

                        }
                    }
                }
                return 0;
            }
        }

        //Methods
        public override string ToString()
        {
            return $"{Brand} {this.Name}";
        }

        public string DeviceInfo()
        {
            string result = "";

            if (this.DeviceFunctionalities.Count() > 1)
            {

                foreach (DeviceFunctionality deviceFunctionality in this.DeviceFunctionalities)
                {
                    result += $"-{deviceFunctionality.FunctionalityInfo()} : {deviceFunctionality.Score}" + Environment.NewLine;
                }
            }
            else
            {
                result = $"{Brand} {this.Name} was not yet scored.";
            }
            return result;
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name) && string.IsNullOrWhiteSpace(Name))
                {
                    return "Name is a required field!";
                }
                return "";
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Device device &&
                   Name == device.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
