using Libraries;

namespace BVPVWebServer.Services.Water;

public class WaterDatService
{
    public static (Result, List<ARCOSampInfoRec>) GetArcoSampInfo(AppInfo appInfo, string whereClause = "")
    {
        var allRecords = new List<ARCOSampInfoRec>(100000);
        var sql = whereClause == "" ? "select * from `WaterData`.`ARCOSampInfo`" : $"select * from `WaterData`.`ARCOSampInfo` {whereClause}";
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery(sql);
        var result = new Result {Message = db.ErrorMessage, Success = db.Success};
        while (rdr!.Read() && rdr.HasRows)
        {
            var rec = new ARCOSampInfoRec
            {
                HLALABID = !DBNull.Value.Equals(rdr["HLALABID"]) ? (string) rdr["HlALABID"] : "",
                OBJID = !DBNull.Value.Equals(rdr["OBJID"]) ? (string) rdr["OBJID"] : "",
                PERMNUM = !DBNull.Value.Equals(rdr["PERMNUM"]) ? (string) rdr["PERMNUM"] : "",
                ORDERNUM = !DBNull.Value.Equals(rdr["ORDERNUM"]) ? (string) rdr["ORDERNUM"] : "",
                SAMPLEID = !DBNull.Value.Equals(rdr["SAMPLEID"]) ? (string) rdr["SAMPLEID"] : "",
                SAMPTYPE = !DBNull.Value.Equals(rdr["SAMPTYPE"]) ? (string) rdr["SAMPTYPE"] : "",
                SAMPBY = !DBNull.Value.Equals(rdr["SAMPBY"]) ? (string) rdr["SAMPBY"] : "",
                COLLDATE = !DBNull.Value.Equals(rdr["COLLDATE"]) ? (DateTime) rdr["COLLDATE"] : appInfo.BaseDate,
                COLLTIME = !DBNull.Value.Equals(rdr["COLLTIME"]) ? (DateTime) rdr["COLLTIME"] : appInfo.BaseDate,
                SAMPDATE = !DBNull.Value.Equals(rdr["SAMPDATE"]) ? (DateTime) rdr["SAMPDATE"] : appInfo.BaseDate,
                LABNAME = !DBNull.Value.Equals(rdr["LABNAME"]) ? (string) rdr["LABNAME"] : "",
                RECDATE = !DBNull.Value.Equals(rdr["RECDATE"]) ? (DateTime) rdr["RECDATE"] : appInfo.BaseDate,
                COMMENT = !DBNull.Value.Equals(rdr["COMMENT"]) ? (string) rdr["COMMENT"] : "",
                ENTERDATE = !DBNull.Value.Equals(rdr["ENTERDATE"]) ? (DateTime) rdr["ENTERDATE"] : appInfo.BaseDate,
                SOURCE = !DBNull.Value.Equals(rdr["SOURCE"]) ? (string) rdr["SOURCE"] : ""
            };
            allRecords.Add(rec);
        }

        return (result, allRecords);
    }

    public static (Result, List<ARCOParamRec>) GetArcoParam(AppInfo appInfo, string whereClause = "")
    {
        var allRecords = new List<ARCOParamRec>(100000);
        var sql = whereClause == "" ? "select * from `WaterData`.`ARCOParam`" : $"select * from `WaterData`.`ARCOParam` {whereClause}";
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery(sql);
        var result = new Result {Message = db.ErrorMessage, Success = db.Success};
        while (rdr!.Read())
        {
            var rec = new ARCOParamRec
            {
                HLALABID = !DBNull.Value.Equals(rdr["HLALABID"]) ? (string) rdr["HlALABID"] : "",
                PARAM = !DBNull.Value.Equals(rdr["PARAM"]) ? (string) rdr["PARAM"] : "",
                FIELDNUM =  Convert.ToInt16(rdr["FIELDNUM"]),
                LABRESULT = !DBNull.Value.Equals(rdr["LABRESULT"]) ? (string) rdr["LABRESULT"] : "",
                LABUNIT = !DBNull.Value.Equals(rdr["LABUNIT"]) ? (string) rdr["LABUNIT"] : "",
                LABQUAL = !DBNull.Value.Equals(rdr["LABQUAL"]) ? (string) rdr["LABQUAL"] : "",
                RESULT = Convert.ToDouble(rdr["RESULT"]),
                UNIT = !DBNull.Value.Equals(rdr["UNIT"]) ? (string) rdr["UNIT"] : "",
                QUAL = !DBNull.Value.Equals(rdr["QUAL"]) ? (string) rdr["QUAL"] : "",
                METHOD = !DBNull.Value.Equals(rdr["METHOD"]) ? (string) rdr["METHOD"] : "",
                ANALDATE = !DBNull.Value.Equals(rdr["ANALDATE"]) ? (DateTime) rdr["ANALDATE"] : appInfo.BaseDate,
                ANALYST = !DBNull.Value.Equals(rdr["ANALYST"]) ? (string) rdr["ANALYST"] : "",
                DATAUSE = !DBNull.Value.Equals(rdr["DATAUSE"]) ? (string) rdr["DATAUSE"] : ""
            };
            allRecords.Add(rec);
        }
        
        return (result, allRecords);
    }
}

