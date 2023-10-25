using System;
using System.Collections.Generic;
using System.Text;
using dxOMark_models;

namespace dxOMark_dal
{
    public interface ICategoriesRepository
    {
        //Get categories
        public IEnumerable<Category> GetCategories();
        public Category GetCategoryViaID(int id);

        //Insert/Delete
        public bool InsertCategory(Category category);
        public bool DeleteCategory(int categoryID);

        
    }
}
