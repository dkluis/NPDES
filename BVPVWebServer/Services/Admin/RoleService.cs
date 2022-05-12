using Libraries;
using Libraries.Entities;

namespace BVPVWebServer.Services.Admin;

public class RoleService
{
    public static IEnumerable<RoleRec> GetAllRoles(AppInfo appInfo, string searchString = "")
    {
        var allRoles = new List<RoleRec>(32);
        var db = new MariaDb(appInfo);
        db.Open();
        var sql = searchString switch
        {
            "" => "select * from `NPDES`.`Admin-Roles` where `RoleID` = ' ' order by `RoleLevel`",
            "*" => "select * from `NPDES`.`Admin-Roles` order by `RoleLevel`",
            _ => $"select * from `NPDES`.`Admin-Roles` where `RoleID` like '%{searchString}%' order by `RoleLevel`"
        };

        var rdr = db.ExecQuery(sql);
        if (!rdr!.HasRows) return allRoles;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] =="SuperAdmin") continue;
            var role = new RoleRec
            {
                RoleId = (string) rdr["RoleID"],
                RoleLevel = (int) rdr["RoleLevel"],
                ReadOnly = (bool) rdr["ReadOnly"],
                Enabled = (bool) rdr["Enabled"]
            };
            allRoles.Add(role);
        }

        return allRoles;
    }
    
    public static List<string> GetAllRoleIds(AppInfo appInfo)
    {
        var allRoleIds = new List<string>(32);
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery("select `RoleID` from `NPDES`.`Admin-Roles` order by `RoleLevel`, `RoleID`");
        if (!rdr!.HasRows) return allRoleIds;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] =="SuperAdmin") continue;
            allRoleIds.Add((string) rdr["RoleID"]);
        }

        return allRoleIds;
    }

    public static RoleRec GetRole(AppInfo appInfo, string role)
    {
        var gottenRole = new RoleRec();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `NPDES`.`Admin-Roles` where `RoleID` = '{role}'");
        if (!rdr!.HasRows) return gottenRole;
        while (rdr.Read())
        {
            gottenRole.RoleId = (string) rdr["RoleID"];
            gottenRole.RoleLevel = (int) rdr["RoleLevel"];
            gottenRole.ReadOnly = (bool) rdr["ReadOnly"];
            gottenRole.Enabled = (bool) rdr["Enabled"];
        }

        return gottenRole;
    }

    public static bool ChangeRole(AppInfo appInfo, RoleRec roleRec)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        
        var sql = $"update `NPDES`.`Admin-Roles` set `RoleLevel` = {roleRec.RoleLevel}, `ReadOnly` = {roleRec.ReadOnly}, `Enabled` = {roleRec.Enabled} where `RoleID` = '{roleRec.RoleId}';";
        db.ExecNonQuery(sql);
        if (!db.Success) success = false;
        db.Close();
        return success;
    }

    public static bool AddRole(AppInfo appInfo, RoleRec roleRec)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        
        var sql = $"insert into `NPDES`.`Admin-Roles` values ('{roleRec.RoleId}', {roleRec.RoleLevel}, {roleRec.ReadOnly}, {roleRec.Enabled});";
        db.ExecNonQuery(sql);
        if (!db.Success) success = false;
        db.Close();
        return success;
    }
    
    public static bool DeleteRole(AppInfo appInfo, string roleId)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        
        var sql = $"delete from `NPDES`.`Admin-Roles` where `RoleID` = '{roleId}';";
        db.ExecNonQuery(sql);
        if (!db.Success) success = false;
        db.Close();
        return success;
    }

    public static bool DoesRoleExist(AppInfo appInfo,string role)
    {
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `NPDES`.`Admin-Roles` where `RoleID` = '{role}'");
        return rdr is {HasRows: true};
    }
}
