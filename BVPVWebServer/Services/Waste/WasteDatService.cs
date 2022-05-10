using Libraries;

namespace BVPVWebServer.Services.Waste;

public class WasteDatService
{
        public static (Result, List<ContainerTypeCodeRec>) GetContainerTypes(AppInfo appInfo)
    {
        var allRecords = new List<ContainerTypeCodeRec>(48);
        const string sql = "select * from `WasteData`.`CONTAINER_TYPE_CODES` order by `WasteData`.`CONTAINER_TYPE_CODES`.`Abrv`";
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
}