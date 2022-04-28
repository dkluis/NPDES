namespace Libraries;

public static class BaseConfig
{
    public static readonly string FullConfigPath;
    public static readonly string ConfigPath;
    public static readonly string BasePath;
    public static readonly string HelpFilesPath;
    public static readonly string DownloadsPath;
    public static readonly string ValidatedPath;
    public static readonly string ProcessedPath;
    public static readonly string ExportedPath;
    public static readonly string LogsPath;
    public static readonly string ArchivesPath;
    public static readonly string AccessData;

    static BaseConfig()
    {
        BasePath = string.Empty;
        HelpFilesPath = string.Empty;
        ConfigPath = string.Empty;
        FullConfigPath = string.Empty;
        var os = Environment.OSVersion;
        var pid = os.Platform;
        switch (pid)
        {
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
                BasePath = "C:/Users/Dick/NPDES-System/";
                break;
            case PlatformID.Unix:
                BasePath = Directory.Exists("/Applications") && Directory.Exists("/Volumes")
                    ? "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/"
                    : "/home/dick/NPDES-System/";
                break;
            case PlatformID.MacOSX:
                BasePath = "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/";
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
        AccessData = BasePath + "AccessData";
    }
}