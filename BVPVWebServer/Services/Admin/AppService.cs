// ReSharper disable All

using Libraries;
using Libraries.Entities;

namespace BVPVWebServer.Services.Admin;

public class AppService
{
    public static List<string> GetAllAppIds(AppInfo appInfo)
    {
        var allAppIds = new List<string>(512);
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery("select `AppID` from `NPDES`.`Admin-Apps` order by `AppID`;");
        if (!rdr!.HasRows) return allAppIds;
        while (rdr.Read())
        {
            if ((string) rdr["AppID"] == "None") continue;
            allAppIds.Add((string) rdr["AppID"]);
        }

        return allAppIds;
    }

    public static List<AppRec> GetAllApps(AppInfo appInfo)
    {
        var allApps = new List<AppRec>(512);
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery("select * from `NPDES`.`Admin-Apps` order by `AppID`;");
        if (!rdr!.HasRows) return allApps;
        while (rdr.Read())
        {
            if ((string) rdr["AppID"] == "None") continue;
            var rec = new AppRec
            {
                AppId = (string) rdr["AppID"],
                FunctionId = (string) rdr["FunctionID"],
                ReportApp = (bool) rdr["ReportApp"]
            };
            allApps.Add(rec);
        }

        return allApps;

    }

    public static List<AppRoleRec> GetAllAppRoles(AppInfo appInfo)
    {
        var allAppRoles = new List<AppRoleRec>(32);
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery("select * from `NPDES`.`Admin-AppRoles` order by `AppID`, `RoleID`;");
        if (!rdr!.HasRows) return allAppRoles;
        while (rdr.Read())
        {
            if ((string) rdr["AppID"] == "None" || (string) rdr["RoleID"] == "SuperAdmin") continue;
            var rec = new AppRoleRec
            {
                AppId = (string) rdr["AppID"],
                RoleId = (string) rdr["RoleID"]
            };
            allAppRoles.Add(rec);
        }

        return allAppRoles;
    }
    
    public static List<AppRec> GetAppsWithoutRoles(AppInfo appInfo)
    {
        var allAppsWithoutRoles = new List<AppRec>(32);
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery("select * from `NPDES`.`Admin-AppsWithoutRoles` order by `App`;");
        if (!rdr!.HasRows) return allAppsWithoutRoles;
        while (rdr.Read())
        {
            if ((string) rdr["App"] == "None") continue;
            var rec = new AppRec
            {
                AppId = (string) rdr["App"],
                FunctionId = (string) rdr["Function"],
                ReportApp = (bool) rdr["Report"]
            };
            allAppsWithoutRoles.Add(rec);
        }

        return allAppsWithoutRoles; 
    }

    public static List<string> GetAllAssignedRoles(AppInfo appInfo, string appid)
    {
        var allApps = new List<string>(32);
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select `RoleID` from `NPDES`.`Admin-AppRoles` where `AppID` = '{appid}' order by `RoleID`;");
        if (!rdr!.HasRows) return allApps;
        while (rdr.Read())
        {
            if ((string) rdr["RoleID"] == "SuperAdmin") continue;
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
            var sql = $"insert into `NPDES`.`Admin-AppRoles` values ('{app}', '{role}');";
            db.ExecNonQuery(sql);
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
            var sql = $"delete from `NPDES`.`Admin-AppRoles` where `AppID`= '{app}' and `RoleID` = '{role}';";
            db.ExecNonQuery(sql);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
    }
}