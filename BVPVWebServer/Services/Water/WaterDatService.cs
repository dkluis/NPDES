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
}

