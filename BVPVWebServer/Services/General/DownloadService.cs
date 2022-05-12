using Libraries;
using Libraries.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace BVPVWebServer.Services.General;

public class DownloadService
{
    public static async Task<Result> ProcessDownloadAsync(AppInfo appInfo, string user, IBrowserFile file)
    {
        var fileHandling = new FileHandling(appInfo);
        var result = await fileHandling.ImportFile(file);

        if (!result.Success) return result;
        
        //ToDo rename the file and passed the renamed filename to RecordDownload
        var renamedFilename = $"{DateTime.Now:yyyy-MM-dd hh-mm-ss} ({user}) {file.Name}";
        var renameResult = fileHandling.RenameImportFile(file.Name, renamedFilename);
        if (!renameResult.Success) return result;
        
        var recordResult = RecordDownload(appInfo, user, renamedFilename, file.Name);
        return recordResult;
    }

    private static Result RecordDownload(AppInfo appInfo, string user, string filename, string originalFilename)
    {
        var result = new Result();
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"insert into `NPDES`.`General-Download` values ('{user}', '{filename}', '{originalFilename}', Null, NOW(3), Null, Null, Null, Null, Null, Null);";
        db.ExecNonQuery(sql);
        result.Success = db.Success;
        result.Message = db.ErrorMessage;

        return result;
    }

    public static (Result, List<ExternalFilesAuditRec>) GetAllDownloadRecs(AppInfo appInfo)
    {
        var result = new Result {Success = true};
        var recList = new List<ExternalFilesAuditRec>(1024);
        
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery("select * from `NPDES`.`General-Download`");
        result.Success = db.Success;
        result.Message = db.ErrorMessage;
        if (!rdr!.HasRows) return (result, recList);
        while (rdr.Read())
        {
            var rec = new ExternalFilesAuditRec
            {
                User = (string) rdr["UserID"],
                FileName = (string) rdr["FileName"],
                OriginalFileName = (string) rdr["OriginalFileName"],
                Function = !DBNull.Value.Equals(rdr["FunctionID"]) ? (string) rdr["FunctionID"] : "",
                DownloadDateTime = (DateTime) rdr["DownloadDateTime"],
                ValidateUser = !DBNull.Value.Equals(rdr["ValidateUserID"]) ? (string) rdr["ValidateUserID"] : "",
                ProcessUser = !DBNull.Value.Equals(rdr["ProcessUserID"]) ?  (string) rdr["ProcessUserID"] : "",
                ValidateDateTime = !DBNull.Value.Equals(rdr["ValidatedDateTime"]) ? (DateTime) rdr["ValidatedDateTime"] : appInfo.BaseDate,
                ProcessDateTime = !DBNull.Value.Equals(rdr["ProcessedDateTime"]) ? (DateTime) rdr["ProcessedDateTime"] : appInfo.BaseDate,
                ArchiveDateTime = !DBNull.Value.Equals(rdr["ArchivedDateTime"]) ? (DateTime) rdr["ArchivedDateTime"] : appInfo.BaseDate,
                ArchiveUser = !DBNull.Value.Equals(rdr["ArchiveUserID"]) ? (string) rdr["ArchiveUserID"] : ""
            };
            recList.Add(rec);
        }

        return (result, recList);
    }
    
    
}

