using System.Numerics;
using Libraries;

namespace BVPVWebServer.Services.Waste;

public class WasteDatService
{
    public static (Result, List<ContainerTypeCodeRec>) GetContainerTypes(AppInfo appInfo)
    {
        var allRecords = new List<ContainerTypeCodeRec>(48);
        const string sql =
            "select * from `WasteData`.`CONTAINER_TYPE_CODES` order by `WasteData`.`CONTAINER_TYPE_CODES`.`Abrv`";
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery(sql);
        var result = new Result {Message = db.ErrorMessage, Success = db.Success};
        while (rdr!.Read() && rdr.HasRows)
        {
            var rec = new ContainerTypeCodeRec
            {
                ContainerType = !DBNull.Value.Equals(rdr["ContainerType"]) ? (string) rdr["ContainerType"] : "",
                Abrv = !DBNull.Value.Equals(rdr["Abrv"]) ? (string) rdr["Abrv"] : ""
            };
            allRecords.Add(rec);
        }

        return (result, allRecords);
    }

    public static (Result, List<WasteShipmentsByYearRec>) GetWasteShipmentsByYear(AppInfo appInfo, string whereClause = "")
    {
        var allRecords = new List<WasteShipmentsByYearRec>(2048);
        var sql = whereClause == ""
            ? "select * from `WasteData`.`WasteShipmentsByYear`;"
            : $"select * from `WasteData`.`WasteShipmentsByYear` {whereClause};";
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery(sql);
        var result = new Result {Message = db.ErrorMessage, Success = db.Success};
        while (rdr!.Read() && rdr.HasRows)
        {
            var rec = new WasteShipmentsByYearRec
            {
                ProfileNum = !DBNull.Value.Equals(rdr["Profile Num"]) ? (string) rdr["Profile Num"] : "",
                WasteName = !DBNull.Value.Equals(rdr["Waste Name"]) ? (string) rdr["Waste Name"] : "",
                Year = (int) rdr["Shipment Year"],
                TotalQuantity = (double) rdr["Total Quantity"],
                AvgQuantity = (double) rdr["Avg Quantity"],
                MinQuantity = (double) rdr["Min Quantity"],
                MaxQuantity = (double) rdr["Max Quantity"],
                NumberOfShipments = BigInteger.Parse(rdr["Number of Shipments"].ToString()!)
            };
            allRecords.Add(rec);
        }

        return (result, allRecords);
    }
}