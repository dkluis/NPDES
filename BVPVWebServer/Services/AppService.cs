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

    public static List<AppRole> GetAllAppRoles(AppInfo appInfo)
    {
        var allAppRoles = new List<AppRole>();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from AppRoles order by `AppID`, `RoleID`;");
        if (!rdr!.HasRows) return allAppRoles;
        while (rdr.Read())
        {
            if ((string) rdr["AppID"] == "None") continue;
            var rec = new AppRole()
            {
                AppId = (string) rdr["AppID"],
                RoleId = (string) rdr["RoleID"]
            };
            allAppRoles.Add(rec);
        }

        return allAppRoles;
    }

    public static List<string> GetAllAssignedRoles(AppInfo appInfo, string appid)
    {
        var allApps = new List<string>();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select `RoleID` from AppRoles where `AppID` = '{appid}' order by `RoleID`;");
        if (!rdr!.HasRows) return allApps;
        while (rdr.Read())
        {
            allApps.Add((string) rdr["RoleID"]);
        }

        return allApps;
    }
    
    public static bool AddAppRoles(AppInfo appInfo, string app, List<string> appRoles)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        foreach (var role in appRoles)
        {
            var sql = $"insert into AppRoles values ('{app}', '{role}');";
            db.ExecNonQuery(sql, true);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
    }
    
    public static bool DeleteAppRoles(AppInfo appInfo, string app, List<string> appRoles)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        foreach (var role in appRoles)
        {
            string sql = $"delete from AppRoles where `AppID`= '{app}' and `RoleID` = '{role}';";
            db.ExecNonQuery(sql, true);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
    }
}

public class App
{
    public App()
    {
        AppId = string.Empty;
        FunctionId = string.Empty;
        ReportApp = false;
    }

    public string AppId { get; set; }
    public string FunctionId { get; set; }
    public bool ReportApp { get; set; }
}

public class AppRole
{
    public AppRole()
    {
        AppId = string.Empty;
        RoleId = string.Empty;
    }
    
    public string AppId { get; set; }
    public string RoleId { get; set; }
}