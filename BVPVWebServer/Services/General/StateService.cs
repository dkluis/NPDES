using Libraries;

namespace BVPVWebServer.Services.General;

public class StateService
{
    private AppInfo AppInfo { get; }
    private MariaDb? Db { get; }

    public SystemState? SystemState { get; set; }
    public AppState? AppState { get; set; }
    public string? UserId { get; set; }
    public bool IsLoggedIn { get; set; }
    public bool IsEnabled { get; set; }
    public string ApiServerBase { get; }

    public StateService()
    {
        AppInfo = new AppInfo("NPDES", "WebUI", "DbNPDES");
        Db = new MariaDb(AppInfo);
        ApiServerBase = AppInfo.ApiServerBase;
    }

    public void InitUserInfo(string userid)
    {
        UserId = userid;
        InitSystemState(userid);
        InitAppStates(userid);
    }

    public AppInfo GetAppInfo()
    {
        return AppInfo;
    }
    
    public void InitSystemState(string userid)
    {
        SystemState = new SystemState();
        Db!.Open();
        var rdr = Db.ExecQuery($"select * from `NPDES`.`Admin-UserSystemState` where `UserID` = '{userid}'");
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                UserId = (string) rdr["UserID"];
                SystemState.DarkTheme = (bool) rdr["DarkTheme"];
                SystemState.LastPage = (string) rdr["LastPage"];
            }
        }

        Db.Close();
    }

    public void InitAppStates(string userid)
    {
        AppState = new AppState();
        Db!.Open();
        var rdr = Db.ExecQuery($"select * from `NPDES`.`Admin-UserAppState` where `UserID` = '{userid}'");
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                AppState!.App = rdr["AppID"].ToString();
                var kv = new List<KeyValuePair<string, string>>(4);
                AppState.Setting = kv;
            }
        }
        else
        {
            AppState.App = "";
            var kv = new List<KeyValuePair<string, string>>(4);
            AppState.Setting = kv;
        }

        Db.Close();
    }

    public void ReloadSystemState()
    {
        InitSystemState(UserId!);
    }
}

public class SystemState
{
    public bool DarkTheme { get; set; }
    public string? LastPage { get; set; }

    public SystemState()
    {
        DarkTheme = false;
        LastPage = "";
    }
}

public class AppState
{
    public string? App { get; set; }
    public List<KeyValuePair<string, string>>? Setting { get; set; }
}