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
        var sql = $"insert into `General-Download` values ('{user}', '{filename}', '{originalFilename}', Null, NOW(3), Null, Null, Null, Null);";
        db.ExecNonQuery(sql);
        result.Success = db.Success;
        result.Message = db.ErrorMessage;

        return result;
    }

    public static (Result, List<DownloadRec>) GetAllDownloadRecs()
    {
        var result = new Result() {Success = true};
        var recList = new List<DownloadRec>();

        

        return (result, recList);
    }
}

public class DownloadRec
{
    public string? User { get; set; }
    public string? FileName { get; set; }
    public string? OriginalFileName { get; set; }
    public string? Function { get; set; }
    public DateTime DownloadDateTime { get; set; }
    public DateTime ValidateDateTime { get; set; }
    public string? ValidateUser { get; set; }
    public DateTime ProcessDateTime { get; set; }
    public string? ProcessUser { get; set; }
}