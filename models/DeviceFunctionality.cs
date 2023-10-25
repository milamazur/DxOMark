using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace dxOMark_models
{
    public class DeviceFunctionality : BaseClass
    {
        //Properties
        public int Id { get; set; }
        public double Score { get; set; }
        public int DeviceId { get; set; }
        public int FunctionalityId { get; set; }

        //Navigation Properties
        public IEnumerable<Functionality> Functionalities { get; set; }
        public Functionality Functionality { get; set; }
        public Device Device { get; set; }

        //Methods
        public override string ToString()
        {
            return Score.ToString();
        }

        public string FunctionalityInfo()
        {
            return Functionality.Name;
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Score) && Score < 0)
                {
                    return "Score must be a valid number!";
                }
                return "";
            }
        }

        public override bool Equals(object obj)
        {
            return obj is DeviceFunctionality score &&
                   DeviceId == score.DeviceId &&
                   FunctionalityId == score.FunctionalityId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DeviceId, FunctionalityId);
        }
    }
}
