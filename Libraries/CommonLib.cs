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
    public string SystemUserName{ get; }
    public TextFileHandler TxtFile{ get; }
    public string WaterDatDb { get; }
    public string WasteDatDb { get; }
    public string WaterEmsDb { get; }
    public string WasteEmsDb { get; }
    public DateTime BaseDate { get; }

    public AppInfo(string application, string program, string dbConnection)
    {
        Application = application;
        Program = program;
        SystemUserName = Environment.UserName;
        BaseDate = new DateTime(1990,01,01,00,00,00);
        
        ReadKeyFromFile readKeyFromFile = new();
        FullConfigPath = BaseConfig.FullConfigPath;
        if (!File.Exists(FullConfigPath))
        {
            Console.WriteLine($"Full Config File Does not Exist {FullConfigPath}");
            Environment.Exit(666);
        }
        
        ApiServerBase = readKeyFromFile.FindInArray(FullConfigPath, "ApiServer");
        LogLevel = int.Parse(readKeyFromFile.FindInArray(FullConfigPath, "LogLevel"));
        WasteDatDb = readKeyFromFile.FindInArray(FullConfigPath, "DbWasteDat");
        WaterDatDb = readKeyFromFile.FindInArray(FullConfigPath, "DbWaterDat");
        WasteEmsDb = readKeyFromFile.FindInArray(FullConfigPath, "DbWasteEms");
        WaterEmsDb = readKeyFromFile.FindInArray(FullConfigPath, "DbWaterEms");
        
        FileName = Program + ".log";
        FilePath = BaseConfig.LogsPath;
        TxtFile = new TextFileHandler(FileName, Program, FilePath, LogLevel);

        ActiveDbConn = readKeyFromFile.FindInArray(FullConfigPath, dbConnection);
        if (ActiveDbConn == "")
        {
            TxtFile.Write($"Database {dbConnection} could not be found", "AppInfo", 0);
        }
    }
}

public class TextFileHandler
{
    private string App { get; }
    private string FullFilePath { get; }
    private int Level { get; }
    private Stopwatch Timer { get; } = new();

    public TextFileHandler(string filename, string application, string inFilePath, int loglevel)
    {
        Timer.Start();
        App = application;
        Level = loglevel;
        FullFilePath = Path.Combine(inFilePath, filename);
        if (!File.Exists(FullFilePath)) File.Create(FullFilePath).Close();
    }

    public void Start()
    {
        EmptyLine();
        Write(
            $"{App} Started  ########################################################################################",
            App, 0);
    }

    public void Stop()
    {
        Timer.Stop();
        Write(
            $"{App} Finished ####################################  in {Timer.ElapsedMilliseconds} mSec  ####################################",
            App, 0);
    }

    public void Empty()
    {
        using StreamWriter file = new(FullFilePath, false);
    }

    public void Write(string message, string function = "", int loglevel = 3, bool append = true)
    {
        if (string.IsNullOrEmpty(function)) function = App;
        if (function.Length > 19) function = function[..19];

        if (loglevel > Level) return;
        using StreamWriter file = new(FullFilePath, append);
        file.WriteLine($"{DateTime.Now}: {function,-20}: {loglevel,-2} --> {message}");
    }

    public void Write(string[] messages, string function = "", int loglevel = 3, bool append = true)
    {
        if (string.IsNullOrEmpty(function)) function = App;
        if (function.Length > 19) function = function[..19];

        if (loglevel > Level) return;
        using StreamWriter file = new(FullFilePath, append);
        foreach (var msg in messages) file.WriteLine($"{DateTime.Now}: {function,-20}: {loglevel,-2}--> {msg}");
    }

    public void WriteJson(string message)
    {
        using StreamWriter file = new(FullFilePath, false);
        file.WriteLine(message);
    }

    public void Elapsed()
    {
        EmptyLine();
        Write($"{App} Elapsed up to now: {Timer.ElapsedMilliseconds} mSec", "Elapsed Time", 0);
        EmptyLine();
    }

    public void EmptyLine(int lines = 1)
    {
        var idx = 1;
        using StreamWriter file = new(FullFilePath, true);
        while (idx <= lines)
        {
            file.WriteLine("");
            idx++;
        }
    }

    public void WriteNoHead(string message, bool newline = true, bool append = true)
    {
        using StreamWriter file = new(FullFilePath, append);
        if (newline)
            file.WriteLine(message);
        else
            file.Write(message);
    }

    public void WriteNoHead(string[] messages, bool newline = true, bool append = true)
    {
        using StreamWriter file = new(FullFilePath, append);
        foreach (var msg in messages)
            if (newline)
                file.WriteLine(msg);
            else
                file.Write(msg);
    }

    public List<string> ReturnLogContent()
    {
        var content = new List<string>(2048);
        var fileContent = File.ReadAllLines(FullFilePath);
        foreach (var line in fileContent)
        {
            content.Add(line);
        }

        content.Reverse();
        return content;
    }

    public string ReadKeyArray(string find)
    {
        if (!File.Exists(FullFilePath)) return "";
        var filetText = File.ReadAllText(FullFilePath);
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
        if (!File.Exists(FullFilePath)) return "";
        var fileText = File.ReadAllText(FullFilePath);
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
