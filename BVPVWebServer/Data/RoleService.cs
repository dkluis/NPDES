using Libraries;

namespace BVPVWebServer.Data;

public class RoleService
{
    public Task<List<Role>> GetAllRolesAsync()
    {
        var allRoles = new List<Role>();
        var appInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
        var db = new MariaDb(appInfo);
        db.Open();
        var result = db.ExecQueryAsync($"select * from `Roles` order by `RoleLevel`");
        var rdr = result.Result;
        if (!rdr!.HasRows) return Task.FromResult(allRoles);
        while (rdr.Read())
        {
            var role = new Role
            {
                RoleId = rdr["RoleID"].ToString(),
                RoleLevel = int.Parse(rdr["RoleLevel"].ToString()!),
                ReadOnly = bool.Parse(rdr["ReadOnly"].ToString()!)
            };
            allRoles.Add(role);
        }

        return Task.FromResult(allRoles);
    }
    
    public static IEnumerable<Role> GetAllRoles(string SearchFor = "")
    {
        var allRoles = new List<Role>();
        var appInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
        var db = new MariaDb(appInfo);
        db.Open();
        var sql = "";
        if (SearchFor == "")
        {
            sql = $"select * from `Roles` where `RoleID` = ' ' order by `RoleLevel`";
        }
        else if (SearchFor == "*")
        {
            sql = $"select * from `Roles` order by `RoleLevel`";
        }
        else
        {
            sql = $"select * from `Roles` where `RoleID` like '%{SearchFor}%' order by `RoleLevel`";
        }
       
        var rdr = db.ExecQuery(sql);
        if (!rdr!.HasRows) return allRoles;
        while (rdr.Read())
        {
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

    public bool DoesRoleExist(string role)
    {
        var appInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
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