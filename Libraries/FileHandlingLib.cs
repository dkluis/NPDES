using Microsoft.AspNetCore.Components.Forms;
using Libraries.Entities;

namespace Libraries;

public class FileHandling
{
    private AppInfo AppInfo { get; }

    public FileHandling(AppInfo appInfo)
    {
        AppInfo = appInfo;
    }

    public async Task<Result> ImportFile(IBrowserFile? file)
    {
        Result result = new()
        {
            Success = true
        };
        try
        {
            await using FileStream fs = new($"{BaseConfig.DownloadsPath}/{file!.Name}", FileMode.CreateNew);
            await file.OpenReadStream().CopyToAsync(fs);
        }
        catch (Exception e)
        {
            AppInfo.TxtFile.Write($"Error: {e.Message}", "FH - ImportFile", 0);
            result.Success = false;
            result.Message = e.Message;
        }
        return result;
    }

    public static bool CheckFileExist(string fileName, string where)
    {
        string path;
        switch (where)
        {
            case "Download":
                path = BaseConfig.DownloadsPath + "/";
                break;
            case "Validated":
                path = BaseConfig.ValidatedPath + "/";
                break;
            case "Processed":
                path = BaseConfig.ProcessedPath + "/";
                break;
            default:
                path = BaseConfig.DownloadsPath + "/";
                break;

        }
        return File.Exists($"{path}{fileName}");
    }
    
    

    public static (Result, List<string>) GetFilesInDir(string dir)
    {
        var result = new Result()
        {
            Success = true
        };
        
        var allFiles = new List<string>(512);
        try
        {
            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                if (file.Contains(".DS_Store")) continue;
                allFiles.Add(file);
            }
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Success = false;
        }
        
        return (result,allFiles);
    }

    public Result ArchiveFile(string fromPath, string fileName)
    {
        Result result = new() { Success = true };
        try
        {
            File.Move($"{fromPath}/{fileName}", $"{BaseConfig.ArchivesPath}/{fileName}");
        }
        catch (Exception e)
        {
            AppInfo.TxtFile.Write($"Error: {e.Message}", "FH - ArchiveFile", 0);
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }
    
    public Result RenameImportFile(string oldName, string newName)
    {
        Result result = new() { Success = true };
        try
        {
            File.Move($"{BaseConfig.DownloadsPath}/{oldName}", $"{BaseConfig.DownloadsPath}/{newName}");
        }
        catch (Exception e)
        {
            AppInfo.TxtFile.Write($"Error: {e.Message}", "FH - RenameImport", 0);
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }
    
}

