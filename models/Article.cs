using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace dxOMark_models
{
    public class Article : BaseClass
    {
        //Properties
        public int Id { get; set; } 
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }


        //Navigation Properties
        public Category Category { get; set; }
        public Device Device { get; set; }
        public IEnumerable<Device> Devices{ get; set; }
      

        //Methods
        public override string ToString()
        {
            return $"{Title} ({UploadDate.ToShortDateString()})";
        }

        public string ArticleInfo()
        {
            return $"{Title} ({UploadDate.ToShortDateString()})" + Environment.NewLine + Environment.NewLine + Text;
        }

        public override bool Equals(object obj)
        {
            return obj is Article article &&
                   Title == article.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title);
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Title) && string.IsNullOrWhiteSpace(Title))
                {
                    return "Title is a required field!";
                }
                return "";
            }
        }






    }
}
