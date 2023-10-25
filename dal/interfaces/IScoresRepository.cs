using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dxOMark_dal.interfaces
{
    public interface IScoresRepository
    {
        //Get Scores
        public IEnumerable<DeviceFunctionality> GetScores();
        public DeviceFunctionality GetScoreViaID(int id);

        //Insert
        public bool InsertScore(DeviceFunctionality score);
    }
}
