using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Libraries;

public class AppInfo
{
    public readonly string ActiveDbConn;
    public readonly string Application;
    public readonly TextFileHandler CnfFile;

    public readonly string ConfigFileName;
    public readonly string ConfigFullPath;
    public readonly string ConfigPath;

    public readonly string DbAltConn;
    //public readonly string DbConnection;

    public readonly string DbProdConn;
    public readonly string DbTestConn;
    public readonly string Drive;
    public readonly string FileName;
    public readonly string FilePath;
    public readonly string FullPath;
    public readonly string HomeDir;
    public readonly int LogLevel;

    public readonly string[] MediaExtensions;
    public readonly string Program;
    public readonly string RarbgToken;

    public readonly string TvmazeToken;
    public readonly TextFileHandler TxtFile;

    public AppInfo(string application, string program, string dbConnection)
    {
        Application = application;
        Program = program;

        EnvInfo envInfo = new();
        Drive = envInfo.Drive;
        HomeDir = (envInfo.Os == "Windows"
            ? Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")
            : Environment.GetEnvironmentVariable("HOME")) ?? string.Empty;

        if (HomeDir != "")
        {
            HomeDir = Path.Combine(HomeDir, Application);
        }
        else
        {
            Console.WriteLine("Could not determine HomeDir");
            Environment.Exit(666);
        }

        ConfigFileName = Application + ".cnf";
        ConfigPath = HomeDir;
        ConfigFullPath = Path.Combine(HomeDir, ConfigFileName);
        if (!File.Exists(ConfigFullPath))
        {
            Console.WriteLine($"Log File Does not Exist {ConfigFullPath}");
            Environment.Exit(666);
        }

        ReadKeyFromFile readKeyFromFile = new();
        LogLevel = int.Parse(readKeyFromFile.FindInArray(ConfigFullPath, "LogLevel"));

        FileName = Program + ".log";
        FilePath = Path.Combine(HomeDir, "Logs");
        FullPath = Path.Combine(FilePath, FileName);

        TxtFile = new TextFileHandler(FileName, Program, FilePath, LogLevel);
        CnfFile = new TextFileHandler(ConfigFileName, Program, ConfigPath, LogLevel);

        DbProdConn = readKeyFromFile.FindInArray(ConfigFullPath, "DbProduction");
        DbTestConn = readKeyFromFile.FindInArray(ConfigFullPath, "DbTesting");
        DbAltConn = readKeyFromFile.FindInArray(ConfigFullPath, "DbAlternate");

        TvmazeToken = readKeyFromFile.FindInArray(ConfigFullPath, "TvmazeToken");
        RarbgToken = readKeyFromFile.FindInArray(ConfigFullPath, "RarbgToken");

        var me = readKeyFromFile.FindInArray(ConfigFullPath, "MediaExtensions");
        MediaExtensions = me.Split(", ");

        switch (dbConnection)
        {
            case "DbProduction":
                ActiveDbConn = DbProdConn;
                break;
            case "DbTesting":
                ActiveDbConn = DbTestConn;
                break;
            case "DbAlternate":
                ActiveDbConn = DbAltConn;
                break;
            default:
                ActiveDbConn = "";
                break;
        }
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
    public string FindInArray(string fullPath, string find)
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
            else 
            if (rec[find]?.ToString() != "")
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
    public readonly string Drive;
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
                Drive = @"C:\";
                break;
            case PlatformID.Unix:
            case PlatformID.MacOSX:
                Os = "Linux";
                Drive = @"/";
                break;
            default:
                Os = "Unknown";
                Drive = "Unknown";
                break;
        }

        MachineName = Environment.MachineName;
        WorkingPath = Environment.CurrentDirectory;
        WorkingDrive = Path.GetPathRoot(WorkingPath);
        UserName = Environment.UserName;
    }
}
