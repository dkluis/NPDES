using Libraries;

namespace BVPVWebServer.Data;

public class StateService
{
    public readonly AppInfo AppInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
    public readonly MariaDb? Db;

    public SystemState? SystemState;
    public AppState? AppState;
    public string? UserId;
    public bool IsLoggedIn;

    public readonly string ApiServerBase;

    public StateService()
    {
        ApiServerBase = AppInfo.ApiServerBase;
        Db = new MariaDb(AppInfo);
    }

    public void InitUserInfo(string userid)
    {
        UserId = userid;
        InitSystemState(userid);
        InitAppStates(userid);
    }

    public void InitSystemState(string userid)
    {
        SystemState = new SystemState();
        Db.Open();
        var rdr = Db.ExecQuery($"select * from `UserSystemState` where `UserID` = '{userid}'");
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
        Db.Open();
        var rdr = Db.ExecQuery($"select * from `UserAppState` where `UserID` = '{userid}'");
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