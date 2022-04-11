using Libraries;
using Newtonsoft.Json;

namespace BVPVWebServer.Data;

public class StateService
{
    public static readonly AppInfo AppInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
    public static readonly MariaDb Db = new MariaDb(AppInfo);
    
    public UserSystemState? SystemState;
    public UserAppState? AppState;
    public string? UserId;
    public bool IsLoggedIn;

    public string ApiServerBase;
    public string ConfigPath;
    
    private static TextFileHandler? _sessionInfo;
    private bool _sessioninitialized = false;
    public string? LastDateLoggedIn;

    public StateService()
    {
        ApiServerBase = AppInfo.ApiServerBase;
    }

    public AppInfo GetAppInfo()
    {
        return AppInfo;
    }

    public void InitUserInfo(string userid)
    {
        UserId = userid;
        InitSystemState(userid);
        InitAppStates(userid);
        ConfigPath = AppInfo.ConfigPath;
        IsLoggedIn = false;
        ApiServerBase = AppInfo.ApiServerBase;
        InitSessionState();
    }
    
    public void InitSystemState(string userid)
    {
        SystemState = new UserSystemState();
        Db.Open();
        var rdr = Db.ExecQuery($"select * from `UserSystemState` where `UserID` = '{userid}'");
        if (!rdr!.HasRows)
        {
            SystemState.DarkTheme = (bool) rdr["DarkTheme"];
        }
        Db.Close();
    }

    public void InitAppStates(string userid)
    {
        AppState = new UserAppState();
        Db.Open();
        var rdr = Db.ExecQuery($"select * from `UserAppState` where `UserID` = '{userid}'");
        if (!rdr!.HasRows)
        {
            AppState!.App = rdr["AppID"].ToString();
            var kv = new List<KeyValuePair<string, string>>();
            //TODO break string of setting in DB out into key value pairs !!!!!
            AppState.Setting = kv;
        }

        Db.Close();
    }

    public void UpdateAll()
    {
    }

    public void UpdateSystemState()
    {
    }

    public void UpdateAppState()
    {
    }

    public void Delete()
    {
    }
    
    public void InitSessionState()
    {
        _sessionInfo = new TextFileHandler($"{UserId}-{DateTime.Now:yyyy-MM-dd}.txt", "SessionService",
            $"{ConfigPath}", 3);
        _sessioninitialized = true;
        LastDateLoggedIn = _sessionInfo.ReadKeyArray("Login");
    }

    public bool WriteJson(List<KeyValuePair<string, string>> keyValuePair)
    {
        if (!_sessioninitialized) return false;
        var json = JsonConvert.SerializeObject(keyValuePair);
        _sessionInfo!.WriteJson(json);
        return true;
    }
}



public class UserSystemState
{
    public bool DarkTheme { get; set; }
}

public class UserAppState
{
    public string? App { get; set; }
    public List<KeyValuePair<string, string>>? Setting { get; set; }
}