namespace Libraries;

public class BaseConfig
{
    public readonly string FullConfigPath;
    public readonly string ConfigPath;

    public BaseConfig()
    {
        ConfigPath = string.Empty;
        var os = Environment.OSVersion;
        var pid = os.Platform;
        switch (pid)
        {
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
                ConfigPath = "C:/Users/Dick/NPDES-System/ConfigData";
                FullConfigPath = $"{ConfigPath}/NPDES-Complete.cnf";
                break;
            case PlatformID.Unix:
                ConfigPath = Directory.Exists("/Applications") && Directory.Exists("/Volumes")
                    ? "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/ConfigData"
                    : "/home/dick/NPDES-System/ConfigData";
                FullConfigPath = $"{ConfigPath}/NPDES-Complete.cnf";
                break;
            case PlatformID.MacOSX:
                ConfigPath = $"/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/ConfigData";
                FullConfigPath = $"{ConfigPath}/NPDES-Complete.cnf";
                break;
            case PlatformID.Xbox:
            case PlatformID.Other:
            default:
                FullConfigPath = string.Empty;
                break;
        }
    }
}