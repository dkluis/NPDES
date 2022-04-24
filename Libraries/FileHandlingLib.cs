using Microsoft.AspNetCore.Components.Forms;

namespace Libraries;

public class FileHandling
{
    public readonly string ImportPath;
    public string ProcessedPath;
    public string LogPath;
    public string ArchivePath;
    private readonly AppInfo _appInfo;

    public FileHandling(AppInfo appInfo)
    {
        var basePath = new BaseConfig().BasePath;
        ImportPath = basePath + "Downloads/Imported";
        ProcessedPath = basePath + "DownLoads/Processed";
        LogPath = new BaseConfig().ConfigPath;
        ArchivePath = basePath + "Archives";
        _appInfo = appInfo;
    }

    public async Task<bool> ImportFile(IBrowserFile file)
    {
        try
        {
            await using FileStream fs = new($"{ImportPath}/{file.Name}", FileMode.CreateNew);
            await file.OpenReadStream().CopyToAsync(fs);
        }
        catch (Exception e)
        {
            _appInfo.TxtFile.Write($"Error: {e.Message}", "FH - ImportFile", 0);
            return false;
        }
        return true;
    }

    public bool CheckFileExist(IBrowserFile file)
    {
        return File.Exists($"{ImportPath}/{file.Name}");
    }

    public bool ArchiveFile(string from, string to, string file)
    {
        File.Move(from, to);
        return true;
    }
}