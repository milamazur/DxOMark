using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;



namespace dxOMark_models
{
    public class Brand : BaseClass
    {
        //Properties
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public IEnumerable<Device> Devices { get; set; }


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
            return obj is Brand brand &&
                   Name == brand.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }



    }
}
