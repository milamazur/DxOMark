using System;
using System.Collections.Generic;
using System.Text;
using dxOMark_models;

namespace dxOMark_dal
{
    public interface IBrandsRepository
    {    
        //Get brands
        public IEnumerable<Brand> GetBrands();

        //Insert
        public bool InsertBrand(Brand brand);
    }
}
