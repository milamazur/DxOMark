using System;
using System.Collections.Generic;
using System.Text;

namespace dxOMark_dal
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }

        public BaseRepository()
        {
            ConnectionString = DatabaseConnection.Connectionstring("DxOMarkConnectionString");
        }
    }
}
