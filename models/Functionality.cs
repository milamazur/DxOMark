using System;
using System.Collections.Generic;
using System.Text;

namespace dxOMark_models
{
    public class Functionality : BaseClass
    {
        //Properties
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public IEnumerable<DeviceFunctionality> deviceFunctionalities { get; set; }


        //Methods
        public override string ToString()
        {
            return Name;
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
            return obj is Functionality functionality &&
                   Name == functionality.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }




    }
}
