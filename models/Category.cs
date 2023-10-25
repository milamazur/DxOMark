using System;
using System.Collections.Generic;
using System.Text;

namespace dxOMark_models
{
    public class Category : BaseClass
    {

        //Properties
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public IEnumerable<Article> Articles { get; set; }

        //Methods
        public override string ToString()
        {
            return this.Name;
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
            return obj is Category category &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }


    }
}
