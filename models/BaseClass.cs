using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dxOMark_models
{
    public abstract class BaseClass : IDataErrorInfo
    {
        public abstract string this[string columnName] { get; }

        public string Error
        {
            get
            {
                string errors = "";
                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        string error = this[property.Name];
                        if (!string.IsNullOrWhiteSpace(error))
                        {
                            errors += error + Environment.NewLine;
                        }
                    }
                }
                return errors;
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(Error);
        }
    }
}
