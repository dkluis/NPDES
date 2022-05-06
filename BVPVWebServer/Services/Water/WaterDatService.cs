using Libraries;

namespace BVPVWebServer.Services.Water;

public class WaterDatService
{
    public static (Result, List<ARCOSampInfoRec>) GetArcoSampInfo(AppInfo appInfo, string whereClause = "")
    {
        var allRecords = new List<ARCOSampInfoRec>(100000);
        var sql = whereClause == "" ? "select * from ARCOSampInfo" : $"select * from ARCOSampInfo {whereClause}";
        var db = new MariaDb(appInfo, appInfo.WaterDatDb);
        db.Open();
        var rdr = db.ExecQuery(sql);
        var result = new Result {Message = db.ErrorMessage, Success = db.Success};
        while (rdr!.Read() && rdr.HasRows)
        {
            var rec = new ARCOSampInfoRec
            {
                HLALABID = rdr["HLALABID"].ToString() != null ? (string) rdr["HlALABID"] : "",
                OBJID = rdr["OBJID"].ToString() != null ? (string) rdr["OBJID"] : "",
                PERMNUM = rdr["PERMNUM"].ToString() != null ? (string) rdr["PERMNUM"] : "",
                ORDERNUM = rdr["ORDERNUM"].ToString() != null ? (string) rdr["ORDERNUM"] : "",
                SAMPLEID = rdr["SAMPLEID"].ToString() != null ? (string) rdr["SAMPLEID"] : "",
                SAMPTYPE = rdr["SAMPTYPE"].ToString() != null ? (string) rdr["SAMPTYPE"] : "",
                SAMPBY = rdr["SAMPBY"].ToString() != null ? (string) rdr["SAMPBY"] : "",
                COLLDATE = rdr["COLLDATE"].ToString() != null ? (DateTime) rdr["COLLDATE"] : appInfo.BaseDate,
                COLLTIME = rdr["COLLTIME"].ToString() != null ? (DateTime) rdr["COLLTIME"] : appInfo.BaseDate,
                SAMPDATE = rdr["SAMPDATE"].ToString() != null ? (DateTime) rdr["SAMPDATE"] : appInfo.BaseDate,
                LABNAME = rdr["LABNAME"].ToString() != null ? (string) rdr["LABNAME"] : "",
                RECDATE = rdr["RECDATE"].ToString() != null ? (DateTime) rdr["RECDATE"] : appInfo.BaseDate,
                COMMENT = rdr["COMMENT"].ToString() != null ? (string) rdr["COMMENT"] : "",
                ENTERDATE = rdr["ENTERDATE"].ToString() != null ? (DateTime) rdr["ENTERDATE"] : appInfo.BaseDate,
                SOURCE = rdr["SOURCE"].ToString() != null ? (string) rdr["SOURCE"] : ""
            };
            allRecords.Add(rec);
        }

        return (result, allRecords);
    }

    public static (Result, List<ARCOParamRec>) GetArcoParam(AppInfo appInfo, string whereClause = "")
    {
        var allRecords = new List<ARCOParamRec>(100000);
        var sql = whereClause == "" ? "select * from ARCOParam" : $"select * from ARCOParam {whereClause}";
        var db = new MariaDb(appInfo, appInfo.WaterDatDb);
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
                DATAUSE = !DBNull.Value.Equals(rdr["DATAUSE"]) ? (string) rdr["DATAUSE"] : "",
            };
            allRecords.Add(rec);
        }
        
        return (result, allRecords);
    }
}

