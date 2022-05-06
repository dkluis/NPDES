namespace Libraries;

public static class BaseConfig
{
    public static string FullConfigPath { get; }
    public static string ConfigPath { get; }
    public static string BasePath { get; }
    public static string HelpFilesPath { get; }
    public static string DownloadsPath { get; }
    public static string ValidatedPath { get; }
    public static string ProcessedPath { get; }
    public static string ExportedPath { get; }
    public static string LogsPath { get; }
    public static string ArchivesPath { get; }
    public static string AccessData { get; }

    static BaseConfig()
    {
        BasePath = string.Empty;
        HelpFilesPath = string.Empty;
        ConfigPath = string.Empty;
        FullConfigPath = string.Empty;
        AccessData = string.Empty;
        var os = Environment.OSVersion;
        var pid = os.Platform;
        switch (pid)
        {
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
                BasePath = "C:/Users/Dick/Documents/NPDES-System/";
                AccessData = "//Win11Dev/NPDES-System/AccessData";
                break;
            case PlatformID.Unix:
                BasePath = Directory.Exists("/Applications") && Directory.Exists("/Volumes")
                    ? "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/"
                    : "/home/dick/NPDES-System/";
                AccessData = Directory.Exists("/Applications") && Directory.Exists("/Volumes")
                    ? "/Volumes/NPDES-System/AccessData"
                    : "/mnt/share/AccessData";
                break;
            case PlatformID.MacOSX:
                BasePath = "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/";
                AccessData = "/Volumes/NPDES-System/AccessData";
                break;
            case PlatformID.Xbox:
            case PlatformID.Other:
            default:
                BasePath = string.Empty;
                break;
        }
        
        ConfigPath = BasePath + "ConfigData";
        FullConfigPath = ConfigPath + "/NPDES-Complete.cnf";
        HelpFilesPath = BasePath + "HelpFiles";
        DownloadsPath = BasePath + "Downloads";
        ValidatedPath = DownloadsPath + "/Validated";
        ExportedPath = BasePath + "Exported";
        ProcessedPath = DownloadsPath + "/Processed";
        LogsPath = BasePath + "Logs";
        ArchivesPath = BasePath + "Archives";
    }
}