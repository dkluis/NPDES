using Libraries;

namespace BVPVWebServer.Data;

public class RoleService
{
    public Task<List<Role>> GetAllRoles()
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

    public Task<bool> DoesRoleExist(string role)
    {
        var appInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
        var db = new MariaDb(appInfo);
        db.Open();
        var result = db.ExecQueryAsync($"select * from `Roles` where `RoleID` = '{role}'");
        var rdr = result.Result;
        return Task.FromResult(rdr is {HasRows: true});
    }
}

public class Role
{
    public string? RoleId { get; set; }
    public int RoleLevel { get; set; }
    public bool ReadOnly{ get; set; }
}