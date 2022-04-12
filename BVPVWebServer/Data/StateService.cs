using IronXL.Xml.Spreadsheet;
using Libraries;
using MudBlazor.Utilities;
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
    public string? LastDateLoggedIn;

    public string ApiServerBase;

    public StateService()
    {
        ApiServerBase = AppInfo.ApiServerBase;
    }

    public void InitUserInfo(string userid)
    {
        UserId = userid;
        InitSystemState(userid);
        InitAppStates(userid);
    }

    public void InitSystemState(string userid)
    {
        SystemState = new UserSystemState();
        Db.Open();
        var rdr = Db.ExecQuery($"select * from `UserSystemState` where `UserID` = '{userid}'");
        if (!rdr!.HasRows)
        {
            SystemState.DarkTheme = (bool) rdr["DarkTheme"];
            SystemState.LastLoginDate = (string) rdr["LastLoginDate"];
            SystemState.LastPage = (string) rdr["LastPage"];
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
    
}



public class UserSystemState
{
    public bool DarkTheme { get; set; }
    public string LastLoginDate { get; set; }
    public string LastPage { get; set; }
}

public class UserAppState
{
    public string? App { get; set; }
    public List<KeyValuePair<string, string>>? Setting { get; set; }
}