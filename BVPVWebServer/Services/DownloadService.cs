using Libraries;

namespace BVPVWebServer.Services;

public class DownloadService
{
    public static Result ProcessDownload(AppInfo appInfo, string user, string filename)
    {
        var result = new Result();
        
        

        if (!result.Success) return result;
        
        var processResult = RecordDownload(appInfo, user, filename);
        return processResult;

    }
    
    public static Result RecordDownload(AppInfo appInfo, string user, string filename)
    {
        var result = new Result();

        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"insert into `General-Download` values ('{user}', '{filename}', Null, Null, Null, Null);";
        db.ExecNonQuery(sql);
        result.Success = db.Success;
        result.Message = db.ErrorMessage;
        
        return result;
    }
}