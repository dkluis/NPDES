using Libraries;

namespace BVPVWebServer.Services;

public class UserService
{
    public static User LoadUser(AppInfo appInfo, string username, string unencryptedPassword, bool checkPw = true)
    {
        var user = new User
        {
            ValidPassword = false,
            ValidUser = false,
            HighestRoleId = string.Empty,
            UserId = username,
            Enabled = true
        };

        using var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from Users where `UserID` = '{username}';");
        if (rdr is {HasRows: false})
        {
            user.ValidUser = false;
            user.UserId = string.Empty;
            user.Enabled = false;
            db.Close();
            return user;
        }

        var storedPassword = string.Empty;
        var storedSalt = string.Empty;
        while (rdr != null && rdr.Read())
        {
            storedPassword = rdr["Password"].ToString() ?? string.Empty;
            storedSalt = rdr["Salt"].ToString() ?? string.Empty;
            user.Enabled = (bool) rdr["Enabled"];
        }
        
        db.Close();
        user.ValidUser = true;

        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(unencryptedPassword, storedSalt);
        if (encryptedPassword != storedPassword && !checkPw)
        {
            return user;
        }

        user.ValidPassword = true;
        db.Open();
        rdr = db.ExecQuery(
            $"select * from UserRolesView where `User` = '{user.UserId}' order by `Role Level` Limit 1;");
        if (rdr is {HasRows: true})
        {
            while (rdr.Read())
            {
                user.HighestRoleId = (string) rdr["Role"];
            }
        }

        db.Close();
        
        return user;
    }

    public static bool AddUser(AppInfo appInfo, string userName, string password, bool enabled = true)
    {
        var user = new User();
        LoadUser(appInfo, userName, password, false);
        var success = false;
        if (user.ValidUser)
        {
            appInfo.TxtFile.Write($"Error adding user: {userName}",
                "UserLib-Add", 0);
            return success;
        }

        var mySalt = BCrypt.Net.BCrypt.GenerateSalt();
        var encryptedPasswrd = BCrypt.Net.BCrypt.HashPassword(password, mySalt);
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"insert into Users values ('{userName}', '{encryptedPasswrd}', '{mySalt}', {enabled});";
        db.ExecNonQuery(sql);
        success = db.Success;
        db.Close();
        return success;
    }
    
    public static bool ChangePassword(AppInfo appInfo, string userName, string password)
    {
        var mySalt = BCrypt.Net.BCrypt.GenerateSalt();
        var encryptedPasswrd = BCrypt.Net.BCrypt.HashPassword(password, mySalt);
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"update Users set `Password` = '{encryptedPasswrd}', `Salt` ='{mySalt}' where `UserID` = '{userName}';";
        db.ExecNonQuery(sql);
        var success = db.Success;
        db.Close();
        return success;
    }
    
    public static bool ChangeEnabled(AppInfo appInfo, string userName, bool enabled)
    {
        using var db = new MariaDb(appInfo);
        db.Open();
        var dbEnabled = 0;
        if (enabled) dbEnabled = 1;
        var sql = $"update Users set `Enabled` = '{dbEnabled}' where `UserID` = '{userName}';";
        db.ExecNonQuery(sql);
        var success = db.Success;
        db.Close();
        return success;
    }

    public static bool DeleteUser(AppInfo appInfo, string userName)
    {
        var user = new User();
        LoadUser(appInfo, userName, "", false);
        var success = false;
        if (user.ValidUser)
        {
            appInfo.TxtFile.Write($"Error adding user: {userName}",
                "UserLib-Add", 0);
            return success;
        }
        
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"delete from Users where `UserID` = '{userName}';";
        db.ExecNonQuery(sql);
        success = db.Success;
        db.Close();
        return success;
    }
    

    public static bool AddUserRoles(AppInfo appInfo, string userName, List<string> userRoles)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        foreach (var role in userRoles)
        {
            var sql = $"insert into UserRoles values ('{userName}', '{role}');";
            db.ExecNonQuery(sql, true);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
    }
    
    public static bool DeleteUserRoles(AppInfo appInfo, string userName, List<string> userRoles)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        foreach (var role in userRoles)
        {
            string sql = $"delete from UserRoles where `UserId`= '{userName}' and `RoleID` = '{role}';";
            db.ExecNonQuery(sql, true);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
    }

    public static List<string> AllAssignedRoles(string userid)
    {
        var assignedRoles = new List<string>();
        var appInfo = new AppInfo("NPDES", "WebUI", "DbProduction");
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select `RoleID` from `UserRoles` where `UserID` = '{userid} 'order by `RoleID`");
        if (!rdr!.HasRows) return assignedRoles;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None") continue;
            assignedRoles.Add((string) rdr["RoleID"]);
        }
        return assignedRoles;
    }

    public static bool CanUserUseApp(AppInfo appInfo, string? userid, string app)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select `App` from AppsByUserView where `User` = '{userid}' and `App` = '{app}'";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows == false) success = false;
        db.Close();
        return success;
    }

    public static IEnumerable<UserElement> GetUsers(AppInfo appInfo, string searchString)
    { 
        var ue = new List<UserElement>();
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select * from Users where `UserID` like '{searchString}'";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                if (rdr["UserID"].ToString() == "Init") continue;
                var rec = new UserElement
                {
                    UserId = (string) rdr["UserID"],
                    Password = (string) rdr["Password"],
                    Salt = (string) rdr["Salt"],
                    Enabled = (bool) rdr["Enabled"]
                };
                ue.Add(rec);
            }
        }
        db.Close();
        return ue;
    }

    public List<AppsByUser> GetAppsByUser(AppInfo appInfo, string userid)
    {
        var result = new List<AppsByUser>();
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select * from AppsByUserView where `User` = '{userid}'";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                if (rdr["User"].ToString() == "Init") continue;
                var rec = new AppsByUser
                {
                    User = (string) rdr["User"],
                    Role = (string) rdr["Role"],
                    ReadOnly = (bool) rdr["ReadOnly"],
                    RoleLevel = (int) rdr["Role Level"],
                    App = (string) rdr["App"],
                    Report = (bool) rdr["Report"],
                    Function = (string) rdr["Function"]
                };
                result.Add(rec);
            }
        }
        db.Close();
        return result;
    }
}

public class User
{
    public string? UserId { get; set; }
    public string? HighestRoleId { get; set; }
    public bool ValidUser { get; set; }
    public bool ValidPassword { get; set; }
    public bool Enabled { get; set; }
    
}

public class UserElement
{
    public UserElement()
    {
        UserId = string.Empty;
        Password = string.Empty;
        Salt = string.Empty;
        Enabled = true;
    }

    public string? UserId { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Enabled { get; set; }
        
}

public class AppsByUser
{
    public AppsByUser()
    {
        User = string.Empty;
        Role = string.Empty;
        ReadOnly = false;
        RoleLevel = 99;
        App = string.Empty;
        Report = true;
        Function = string.Empty;
    }
    
    public string User { get; set; }
    public string Role { get; set; }
    public bool ReadOnly { get; set; }
    public int RoleLevel { get; set; }
    public string App { get; set; }
    public bool Report { get; set; }
    public string Function { get; set; }
}