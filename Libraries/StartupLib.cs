namespace Libraries;

public class BaseConfig
{
    public readonly string FullConfigPath;
    public readonly string ConfigPath;
    public readonly string BasePath;
    public readonly string HelpFilesPath;

    public BaseConfig()
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
    }
}