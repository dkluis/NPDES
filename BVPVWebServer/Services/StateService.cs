using Libraries;

namespace BVPVWebServer.Services;

public class StateService
{
    private readonly AppInfo _appInfo;
    private readonly MariaDb? _db;

    public SystemState? SystemState;
    public AppState? AppState;
    public string? UserId;
    public bool IsLoggedIn;
    public bool IsEnabled;

    public readonly string ApiServerBase;

    public StateService()
    {
        _appInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
        _db = new MariaDb(_appInfo);
        ApiServerBase = _appInfo.ApiServerBase;
    }

    public void InitUserInfo(string userid)
    {
        UserId = userid;
        InitSystemState(userid);
        InitAppStates(userid);
    }

    public AppInfo GetAppInfo()
    {
        return _appInfo;
    }
    
    public void InitSystemState(string userid)
    {
        SystemState = new SystemState();
        _db!.Open();
        var rdr = _db.ExecQuery($"select * from `UserSystemState` where `UserID` = '{userid}'");
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                UserId = (string) rdr["UserID"];
                SystemState.DarkTheme = (bool) rdr["DarkTheme"];
                SystemState.LastPage = (string) rdr["LastPage"];
            }
        }

        _db.Close();
    }

    public void InitAppStates(string userid)
    {
        AppState = new AppState();
        _db!.Open();
        var rdr = _db.ExecQuery($"select * from `UserAppState` where `UserID` = '{userid}'");
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                AppState!.App = rdr["AppID"].ToString();
                var kv = new List<KeyValuePair<string, string>>();
                AppState.Setting = kv;
            }
        }
        else
        {
            AppState.App = "";
            var kv = new List<KeyValuePair<string, string>>();
            AppState.Setting = kv;
        }

        _db.Close();
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
    
}



public class SystemState
{
    public bool DarkTheme { get; set; }
    public string? LastPage { get; set; }

    public SystemState()
    {
        DarkTheme = true;
        LastPage = "";
    }
}

public class AppState
{
    public string? App { get; set; }
    public List<KeyValuePair<string, string>>? Setting { get; set; }
}