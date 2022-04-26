using Libraries;
using Microsoft.AspNetCore.Components.Forms;

namespace BVPVWebServer.Services;

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
        
        var recordResult = RecordDownload(appInfo, user, renamedFilename);
        return recordResult;
    }

    private static Result RecordDownload(AppInfo appInfo, string user, string filename)
    {
        var result = new Result();
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"insert into `General-Download` values ('{user}', '{filename}', Null, NOW(3), Null, Null);";
        db.ExecNonQuery(sql);
        result.Success = db.Success;
        result.Message = db.ErrorMessage;

        return result;
    }
}

public class DownloadRec
{
    public string User { get; set; }
    public string FileName { get; set; }
}