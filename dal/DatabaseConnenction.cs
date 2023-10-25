using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace dxOMark_dal
{
    public static class DatabaseConnection
    {
        public static string Connectionstring(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
