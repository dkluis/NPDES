using System.Net;
using Microsoft.AspNetCore.Components.Forms;

namespace Libraries;

public class FileHandling
{
    private readonly AppInfo _appInfo;

    public FileHandling(AppInfo appInfo)
    {
        _appInfo = appInfo;
    }

    public async Task<Result> ImportFile(IBrowserFile? file)
    {
        Result result = new()
        {
            Success = true
        };
        try
        {
            await using FileStream fs = new($"{BaseConfig.ImportedPath}/{file.Name}", FileMode.CreateNew);
            await file.OpenReadStream().CopyToAsync(fs);
        }
        catch (Exception e)
        {
            _appInfo.TxtFile.Write($"Error: {e.Message}", "FH - ImportFile", 0);
            result.Success = false;
            result.Message = e.Message;
        }
        return result;
    }

    public static bool CheckFileExist(IBrowserFile? file)
    {
        return File.Exists($"{BaseConfig.ImportedPath}/{file.Name}");
    }

    public static (Result, List<string>) GetFilesInDir(string dir)
    {
        var result = new Result()
        {
            Success = true
        };
        
        var allFiles = new List<string>();
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
            _appInfo.TxtFile.Write($"Error: {e.Message}", "FH - ArchiveFile", 0);
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }
    
    
}

