using Libraries;

namespace BVPVWebServer.Services;

public class RoleService
{
    public static IEnumerable<Role> GetAllRoles(AppInfo appInfo, string searchString = "")
    {
        var allRoles = new List<Role>();
        var db = new MariaDb(appInfo);
        db.Open();
        var sql = searchString switch
        {
            "" => $"select * from `Roles` where `RoleID` = ' ' order by `RoleLevel`",
            "*" => $"select * from `Roles` order by `RoleLevel`",
            _ => $"select * from `Roles` where `RoleID` like '%{searchString}%' order by `RoleLevel`"
        };

        var rdr = db.ExecQuery(sql);
        if (!rdr!.HasRows) return allRoles;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] =="SuperAdmin") continue;
            var role = new Role
            {
                RoleId = rdr["RoleID"].ToString(),
                RoleLevel = int.Parse(rdr["RoleLevel"].ToString()!),
                ReadOnly = bool.Parse(rdr["ReadOnly"].ToString()!)
            };
            allRoles.Add(role);
        }

        return allRoles;
    }
    
    public static List<string> GetAllRoleIds(AppInfo appInfo)
    {
        var allRoleIds = new List<string>();
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select `RoleID` from Roles order by `RoleLevel`, `RoleID`");
        if (!rdr!.HasRows) return allRoleIds;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] =="SuperAdmin") continue;
            allRoleIds.Add((string) rdr["RoleID"]);
        }

        return allRoleIds;
    }

    public static bool AddRole(AppInfo appInfo, Role role)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"insert into `Roles` values ('{role.RoleId}', {role.RoleLevel}, {role.ReadOnly});";
        db.ExecNonQuery(sql, true);
        if (!db.Success) success = false;
        db.Close();
        return success;
    }

    public static bool DoesRoleExist(AppInfo appInfo,string role)
    {
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `Roles` where `RoleID` = '{role}'");
        return (rdr is {HasRows: true});
    }
}

public class Role
{
    public string? RoleId { get; set; }
    public int RoleLevel { get; set; }
    public bool ReadOnly{ get; set; }
}