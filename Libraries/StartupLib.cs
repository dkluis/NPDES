namespace Libraries;

public class BaseConfig
{
    public string FullConfigPath; 

    public BaseConfig()
    {
        var os = Environment.OSVersion;
        var pid = os.Platform;
        switch (pid)
        {
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
                FullConfigPath = string.Empty;
                break;
            case PlatformID.Unix:
                FullConfigPath = Directory.Exists("/Applications") && Directory.Exists("/Volumes")
                    ? "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/ConfigData/NPDES-Complete.cnf"
                    : "/home/dick/NPDES-System/ConfigData/NPDES-Complete.cnf";
                break;
            case PlatformID.MacOSX:
                FullConfigPath = "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/ConfigData/NPDES-Complete.cnf";
                break;
            case PlatformID.Xbox:
            case PlatformID.Other:
            default:
                FullConfigPath = string.Empty;
                break;
        }
    }
}