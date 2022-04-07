using Libraries;

namespace BVPVWebServer.Data;

public class StateService
{
    public static readonly AppInfo AppInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
    public static readonly MariaDb Db = new MariaDb(AppInfo);
    
    public UserSystemState? SystemState;
    public UserAppState? AppState;
    public string? UserId;
    public bool IsLoggedIn;


    public void InitUserInfo(string userid)
    {
        UserId = userid;
        InitSystemState(userid);
        InitAppStates(userid);
        IsLoggedIn = false;
    }
    
    public void InitSystemState(string userid)
    {
        SystemState = new UserSystemState();
        Db.Open();
        var rdr = Db.ExecQueryAsync($"select * from `UserSystemState` where `UserID` = '{userid}'").Result;
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
        var rdr = Db.ExecQueryAsync($"select * from `UserAppState` where `UserID` = '{userid}'").Result;
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