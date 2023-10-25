using System;
using System.Collections.Generic;
using System.Text;
using dxOMark_models;

namespace dxOMark_dal.interfaces
{
    public interface IFunctionalitiesRepository
    {
        //Get Functionalities
        public IEnumerable<Functionality> GetFunctionalities();

        //Insert/Delete
        public bool InsertFunctionality(Functionality functionality);       
        public bool DeleteFunctionality(int functionalityID);

    }
}
