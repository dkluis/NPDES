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

    public static (Result, List<DrumTrackingRec>) GetDrumTracking(AppInfo appInfo, string whereClause = "")
    {
        var allRecords = new List<DrumTrackingRec>(2048);
        var sql = whereClause == ""
            ? "select * from `WasteData`.`DRUM_TRACKING`;"
            : $"select * from `WasteData`.`DRUM_TRACKING` {whereClause};";
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery(sql);
        var result = new Result {Message = db.ErrorMessage, Success = db.Success};
        while (rdr!.Read() && rdr.HasRows)
        {
            var rec = new DrumTrackingRec()
            {
                DrumNumber = !DBNull.Value.Equals(rdr["DrumNumber"]) ? (string) rdr["DrumNumber"] : "",
                ProfileNumber = !DBNull.Value.Equals(rdr["ProfileNumber"]) ? (string) rdr["ProfileNumber"] : "",
                HAZNON = !DBNull.Value.Equals(rdr["HAZ/NON"]) ? (string) rdr["HAZ/NON"] : "",
                ContactPerson = !DBNull.Value.Equals(rdr["ContactPerson"]) ? (string) rdr["ContactPerson"] : "",
                SourceProcess = !DBNull.Value.Equals(rdr["SourceProcess"]) ? (string) rdr["SourceProcess"] : "",
                SourceActivity = !DBNull.Value.Equals(rdr["SourceProcess"]) ? (string) rdr["SourceProcess"] : "",
                AccumStartDate = !DBNull.Value.Equals(rdr["AccumStartDate"]) ? (DateTime) rdr["AccumStartDate"] : appInfo.BaseDate,
                ShipppedOffSite = !DBNull.Value.Equals(rdr["ShippedOffSite"]) ? (DateTime) rdr["ShippedOffSite"] : appInfo.BaseDate,
                Comments = !DBNull.Value.Equals(rdr["Comments"]) ? (string) rdr["Comments"] : "",
                CostCenter = !DBNull.Value.Equals(rdr["CostCenter"]) ? (string) rdr["CostCenter"] : "",
                SourceDept = !DBNull.Value.Equals(rdr["SourceDept"]) ? (string) rdr["SourceDept"] : "",
                DrumType = !DBNull.Value.Equals(rdr["DrumType"]) ? (string) rdr["DrumType"] : "",
                verified = (bool) rdr["verified"],
                Location = !DBNull.Value.Equals(rdr["Location"]) ? (string) rdr["Location"] : "",
                AccumulationArea = !DBNull.Value.Equals(rdr["AccumulationArea"]) ? (string) rdr["AccumulationArea"] : ""

            };
            allRecords.Add(rec);
        }

        return (result, allRecords);
    }
}