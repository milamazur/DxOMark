using dxOMark_dal.interfaces;
using dxOMark_models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;

namespace dxOMark_dal
{
    public class ScoresRepository : BaseRepository, IScoresRepository
    {

        //GET ALL SCORES
        public IEnumerable<DeviceFunctionality> GetScores()
        {
            string sql = @"SELECT DF.*, '' AS SplitCol, D.*, '' AS SplitCol, F.*
                            FROM DxOMark.DeviceFunctionality DF
                            INNER JOIN DxOMark.Device D ON D.id = DF.deviceID   
                            INNER JOIN DxOMark.Functionality F ON F.id = DF.functionalityID
                            ORDER BY D.name";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var scores = db.Query<DeviceFunctionality, Device, Functionality, DeviceFunctionality>(
                   sql,
                   (score, device, functionality) =>
                   {
                       score.Device = device;
                       score.Functionality = functionality;
                       return score;
                   },

                   splitOn: "SplitCol");
                return scores;
            }
            
        }

        //GET SCORE VIA ID
        public DeviceFunctionality GetScoreViaID(int id)
        {
            string sql = @"SELECT DF.*, '' AS SplitCol, D.*, '' AS SplitCol, F.*
                            FROM DxOMark.DeviceFunctionality DF
                            INNER JOIN DxOMark.Device D ON D.id = DF.deviceID   
                            INNER JOIN DxOMark.Functionality F ON F.id = DF.functionalityID  
                            WHERE D.id = @id";
            var parameters = new { @id = id };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<DeviceFunctionality, Device, Functionality, DeviceFunctionality>(
                   sql,
                   (score, device, functionality ) =>
                   {
                       score.Device = device;
                       score.Functionality = functionality;
                       return score;
                   },
                   parameters,
                   splitOn: "SplitCol"
                   ).SingleOrDefault();
            }
        }


        //INSERT SCORE
        public bool InsertScore(DeviceFunctionality score)
        {
            string sql = @"INSERT INTO DxOMark.DeviceFunctionality (score, deviceID, functionalityID )
                           OUTPUT INSERTED.*
                           VALUES (@score, @deviceID, @functionalityID)";

            var parameters = new
            {
                @score = score.Score,
                @deviceID = score.DeviceId,
                @functionalityID = score.FunctionalityId
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
