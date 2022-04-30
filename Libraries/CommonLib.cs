using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Libraries;

public class AppInfo
{
    public string ActiveDbConn { get; }
    private string Application { get; }
    private string Program { get; }
    public string ApiServerBase { get; }
    private string? FullConfigPath { get; }
    private string FileName { get; }
    private string FilePath{ get; }
    private int LogLevel{ get; }
    public string SystemUserName{ get; set; }
    public TextFileHandler TxtFile{ get; set; }

    public AppInfo(string application, string program, string dbConnection)
    {
        Application = application;
        Program = program;
        SystemUserName = Environment.UserName;
        
        ReadKeyFromFile readKeyFromFile = new();
        FullConfigPath = BaseConfig.FullConfigPath;
        if (!File.Exists(FullConfigPath))
        {
            Console.WriteLine($"Full Config File Does not Exist {FullConfigPath}");
            Environment.Exit(666);
        }
        
        ApiServerBase = readKeyFromFile.FindInArray(FullConfigPath, "ApiServer");
        LogLevel = int.Parse(readKeyFromFile.FindInArray(FullConfigPath, "LogLevel"));

        FileName = Program + ".log";
        FilePath = BaseConfig.LogsPath;
        TxtFile = new TextFileHandler(FileName, Program, FilePath, LogLevel);

        var dbProdConn = readKeyFromFile.FindInArray(FullConfigPath, "DbProduction");
        var dbTestConn = readKeyFromFile.FindInArray(FullConfigPath, "DbTesting");
        var dbAltConn = readKeyFromFile.FindInArray(FullConfigPath, "DbAlternate");

        ActiveDbConn = dbConnection switch
        {
            "DbProduction" => dbProdConn,
            "DbTesting" => dbTestConn,
            "DbAlternate" => dbAltConn,
            _ => ""
        };
    }
}

public class TextFileHandler
{
    private readonly string _app;
    private readonly string _fullFilePath;
    private readonly int _level;
    private readonly Stopwatch _timer = new();

    public TextFileHandler(string filename, string application, string inFilePath, int loglevel)
    {
        _timer.Start();
        _app = application;
        _level = loglevel;
        _fullFilePath = Path.Combine(inFilePath, filename);
        if (!File.Exists(_fullFilePath)) File.Create(_fullFilePath).Close();
    }

    public void Start()
    {
        EmptyLine();
        Write(
            $"{_app} Started  ########################################################################################",
            _app, 0);
    }

    public void Stop()
    {
        _timer.Stop();
        Write(
            $"{_app} Finished ####################################  in {_timer.ElapsedMilliseconds} mSec  ####################################",
            _app, 0);
    }

    public void Empty()
    {
        using StreamWriter file = new(_fullFilePath, false);
    }

    public void Write(string message, string function = "", int loglevel = 3, bool append = true)
    {
        if (string.IsNullOrEmpty(function)) function = _app;
        if (function.Length > 19) function = function[..19];

        if (loglevel > _level) return;
        using StreamWriter file = new(_fullFilePath, append);
        file.WriteLine($"{DateTime.Now}: {function,-20}: {loglevel,-2} --> {message}");
    }

    public void Write(string[] messages, string function = "", int loglevel = 3, bool append = true)
    {
        if (string.IsNullOrEmpty(function)) function = _app;
        if (function.Length > 19) function = function[..19];

        if (loglevel > _level) return;
        using StreamWriter file = new(_fullFilePath, append);
        foreach (var msg in messages) file.WriteLine($"{DateTime.Now}: {function,-20}: {loglevel,-2}--> {msg}");
    }

    public void WriteJson(string message)
    {
        using StreamWriter file = new(_fullFilePath, false);
        file.WriteLine(message);
    }

    public void Elapsed()
    {
        EmptyLine();
        Write($"{_app} Elapsed up to now: {_timer.ElapsedMilliseconds} mSec", "Elapsed Time", 0);
        EmptyLine();
    }

    public void EmptyLine(int lines = 1)
    {
        var idx = 1;
        using StreamWriter file = new(_fullFilePath, true);
        while (idx <= lines)
        {
            file.WriteLine("");
            idx++;
        }
    }

    public void WriteNoHead(string message, bool newline = true, bool append = true)
    {
        using StreamWriter file = new(_fullFilePath, append);
        if (newline)
            file.WriteLine(message);
        else
            file.Write(message);
    }

    public void WriteNoHead(string[] messages, bool newline = true, bool append = true)
    {
        using StreamWriter file = new(_fullFilePath, append);
        foreach (var msg in messages)
            if (newline)
                file.WriteLine(msg);
            else
                file.Write(msg);
    }

    public List<string> ReturnLogContent()
    {
        var content = new List<string>();
        var fileContent = File.ReadAllLines(_fullFilePath);
        foreach (var line in fileContent)
        {
            content.Add(line);
        }

        content.Reverse();
        return content;
    }

    public string ReadKeyArray(string find)
    {
        if (!File.Exists(_fullFilePath)) return "";
        var filetText = File.ReadAllText(_fullFilePath);
        var keyValuePair = ConvertJsonTxt.ConvertStringToJArray(filetText);
        foreach (var rec in keyValuePair)
        {
            if (rec[find] is null)
            {
                return "";
            }
            else if (rec[find]?.ToString() != "")
            {
                return rec[find]?.ToString() ?? "";
            }
        }

        return "";
    }

    public string ReadKeyObject(string find)
    {
        if (!File.Exists(_fullFilePath)) return "";
        var fileText = File.ReadAllText(_fullFilePath);
        var keyValuePair = ConvertJsonTxt.ConvertStringToJObject(fileText);
        foreach (var rec in keyValuePair)
            if (rec.Key == find)
                return rec.Value!.ToString();
        return "";
    }
}

public static class ConvertJsonTxt
{
    public static JArray ConvertStringToJArray(string message)
    {
        if (message == "")
        {
            JArray empty = new();
            return empty;
        }

        var jA = JArray.Parse(message);
        return jA;
    }

    public static JObject ConvertStringToJObject(string message)
    {
        if (message == "")
        {
            JObject empty = new();
            return empty;
        }

        var jO = JObject.Parse(message);
        return jO;
    }
}

public class ReadKeyFromFile
{
    public string FindInArray(string? fullPath, string find)
    {
        if (!File.Exists(fullPath)) return "";
        var fileText = File.ReadAllText(fullPath);
        var keyValuePair = ConvertJsonTxt.ConvertStringToJArray(fileText);
        foreach (var rec in keyValuePair)
        {
            if (rec[find] is null)
            {
                return "";
            }
            else if (rec[find]?.ToString() != "")
            {
                return rec[find]?.ToString() ?? "";
            }
        }

        return "";
    }

    public string FindInObject(string fullPath, string find)
    {
        if (!File.Exists(fullPath)) return "";
        var fileText = File.ReadAllText(fullPath);
        var keyValuePair = ConvertJsonTxt.ConvertStringToJObject(fileText);
        foreach (var rec in keyValuePair)
            if (rec.Key == find)
                return rec.Value!.ToString();
        return "";
    }
}

public class EnvInfo
{
    public readonly string MachineName;
    public readonly string Os;
    public readonly string UserName;
    public readonly string? WorkingDrive;
    public readonly string WorkingPath;

    public EnvInfo()
    {
        var os = Environment.OSVersion;
        var pid = os.Platform;
        switch (pid)
        {
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
                Os = "Windows";
                break;
            case PlatformID.Unix:
            case PlatformID.MacOSX:
                Os = "Linux";
                break;
            case PlatformID.Xbox:
            case PlatformID.Other:
            default:
                Os = "Unknown";
                break;
        }

        MachineName = Environment.MachineName;
        WorkingPath = Environment.CurrentDirectory;
        WorkingDrive = Path.GetPathRoot(WorkingPath);
        UserName = Environment.UserName;
    }
}