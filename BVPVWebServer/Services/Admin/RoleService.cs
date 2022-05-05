using Libraries;

namespace BVPVWebServer.Services.Admin;

public class RoleService
{
    public static IEnumerable<Role> GetAllRoles(AppInfo appInfo, string searchString = "")
    {
        var allRoles = new List<Role>(32);
        var db = new MariaDb(appInfo);
        db.Open();
        var sql = searchString switch
        {
            "" => $"select * from `Admin-Roles` where `RoleID` = ' ' order by `RoleLevel`",
            "*" => $"select * from `Admin-Roles` order by `RoleLevel`",
            _ => $"select * from `Admin-Roles` where `RoleID` like '%{searchString}%' order by `RoleLevel`"
        };

        var rdr = db.ExecQuery(sql);
        if (!rdr!.HasRows) return allRoles;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] =="SuperAdmin") continue;
            var role = new Role
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
        var rdr = db.ExecQuery($"select `RoleID` from `Admin-Roles` order by `RoleLevel`, `RoleID`");
        if (!rdr!.HasRows) return allRoleIds;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] =="SuperAdmin") continue;
            allRoleIds.Add((string) rdr["RoleID"]);
        }

        return allRoleIds;
    }

    public static Role GetRole(AppInfo appInfo, string role)
    {
        var gottenRole = new Role();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `Admin-Roles` where `RoleID` = '{role}'");
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

    public static bool ChangeRole(AppInfo appInfo, Role role)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        
        var sql = $"update `Admin-Roles` set `RoleLevel` = {role.RoleLevel}, `ReadOnly` = {role.ReadOnly}, `Enabled` = {role.Enabled} where `RoleID` = '{role.RoleId}';";
        db.ExecNonQuery(sql);
        if (!db.Success) success = false;
        db.Close();
        return success;
    }

    public static bool AddRole(AppInfo appInfo, Role role)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        
        var sql = $"insert into `Admin-Roles` values ('{role.RoleId}', {role.RoleLevel}, {role.ReadOnly}, {role.Enabled});";
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
        
        var sql = $"delete from `Admin-Roles` where `RoleID` = '{roleId}';";
        db.ExecNonQuery(sql);
        if (!db.Success) success = false;
        db.Close();
        return success;
    }

    public static bool DoesRoleExist(AppInfo appInfo,string role)
    {
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `Admin-Roles` where `RoleID` = '{role}'");
        return (rdr is {HasRows: true});
    }
}

public class Role
{
    public string? RoleId { get; set; }
    public int RoleLevel { get; set; }
    public bool ReadOnly { get; set; }
    public bool Enabled { get; set; }
}