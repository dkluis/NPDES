using Libraries;

namespace BVPVWebServer.Data;

public class AppService
{
    public static List<string> GetAllAppIds(AppInfo appInfo)
    {
        var allAppIds = new List<string>();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select `AppID` from Apps order by `AppID`;");
        if (!rdr!.HasRows) return allAppIds;
        while (rdr.Read())
        {
            if ((string) rdr["AppID"] == "None") continue;
            allAppIds.Add((string) rdr["AppID"]);
        }

        return allAppIds;
    }

    public static List<App> GetAllApps(AppInfo appInfo)
    {
        var allApps = new List<App>();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from Apps order by `AppID`;");
        if (!rdr!.HasRows) return allApps;
        while (rdr.Read())
        {
            if ((string) rdr["AppID"] == "None") continue;
            var rec = new App()
            {
                AppId = (string) rdr["AppID"],
                FunctionId = (string) rdr["FunctionID"],
                ReportApp = (bool) rdr["ReportApp"]
            };
            allApps.Add(rec);
        }

        return allApps;

    }
    
}

public class App
{
    public App()
    {
        AppId = string.Empty;
        FunctionId = String.Empty;
        ReportApp = false;
    }

    public string AppId { get; set; }
    public string FunctionId { get; set; }
    public bool ReportApp { get; set; }
}