using Libraries;
using Microsoft.AspNetCore.Components.Forms;

namespace BVPVWebServer.Services.General;

public class DownloadService
{
    public static async Task<Result> ProcessDownloadAsync(AppInfo appInfo, string user, IBrowserFile file)
    {
        var fileHandling = new FileHandling(appInfo);
        var result = await fileHandling!.ImportFile(file);

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
        var sql = $"insert into `NPDES`.`General-Download` values ('{user}', '{filename}', '{originalFilename}', Null, NOW(3), Null, Null, Null, Null);";
        db.ExecNonQuery(sql);
        result.Success = db.Success;
        result.Message = db.ErrorMessage;

        return result;
    }

    public static (Result, List<DownloadRec>) GetAllDownloadRecs(AppInfo appInfo)
    {
        var result = new Result() {Success = true};
        var recList = new List<DownloadRec>(1024);
        
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `NPDES`.`General-Download`");
        result.Success = db.Success;
        result.Message = db.ErrorMessage;
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {

                var function = string.Empty;
                var validateUser = string.Empty;
                var processUser = string.Empty;
                if (! DBNull.Value.Equals(rdr["FunctionID"]))
                {
                    function = (string) rdr["FunctionID"];
                }
                if (! DBNull.Value.Equals(rdr["ValidateUserID"]))
                {
                    validateUser = (string) rdr["ValidateUserID"];
                }
                if (! DBNull.Value.Equals(rdr["ProcessUserID"]))
                {
                    processUser = (string) rdr["ProcessUserID"];
                }

                var validateDt = new DateTime();
                var processDt = new DateTime();
                if (! DBNull.Value.Equals(rdr["ValidatedDateTime"]))
                {
                    validateDt = (DateTime) rdr["ValidatedDateTime"];
                }
                if (! DBNull.Value.Equals(rdr["ProcessedDateTime"]))
                {
                    processDt = (DateTime) rdr["ProcessedDateTime"];
                }
                

                var rec = new DownloadRec()
                {
                    User = (string) rdr["UserID"],
                    FileName = (string) rdr["FileName"],
                    Function = (string) function,
                    DownloadDateTime = (DateTime) rdr["DownloadDateTime"],
                    ValidateUser = validateUser,
                    ProcessUser = processUser,
                    ValidateDateTime = validateDt,
                    ProcessDateTime = processDt,
                };
                recList.Add(rec);
            }
        }

        return (result, recList);
    }
    
    
}

