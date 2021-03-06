using Libraries;
using Libraries.Entities;
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace BVPVWebServer.Services.Admin;

public class UserService
{
    public static UserLoginRec LoadUser(AppInfo appInfo, string username, string unencryptedPassword, bool checkPw = true)
    {
        var user = new UserLoginRec
        {
            ValidPassword = false,
            ValidUser = false,
            UserId = username,
            Enabled = true
        };

        using var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from `NPDES`.`Admin-Users` where `UserID` = '{username}';");
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
       
        return user;
    }

    public static bool AddUser(AppInfo appInfo, string userName, string password, bool enabled = true)
    {
        var user = new UserLoginRec();
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
        var sql = $"insert into `NPDES`.`Admin-Users` values ('{userName}', '{encryptedPasswrd}', '{mySalt}', {enabled});";
        db.ExecNonQuery(sql);
        db.Close();
        db.Open();
        sql = $"insert into `NPDES`.`Admin-UserSystemState` value ('{userName}', false, '/');";
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
        var sql = $"update `NPDES`.`Admin-Users` set `Password` = '{encryptedPasswrd}', `Salt` ='{mySalt}' where `UserID` = '{userName}';";
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
        var sql = $"update `NPDES`.`Admin-Users` set `Enabled` = '{dbEnabled}' where `UserID` = '{userName}';";
        db.ExecNonQuery(sql);
        var success = db.Success;
        db.Close();
        return success;
    }
    
    public static void ChangeDarkTheme(AppInfo appInfo, string userName, bool darkTheme)
    {
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"update `NPDES`.`Admin-UserSystemState` set `DarkTheme` = {darkTheme} where `UserID` = '{userName}';";
        db.ExecNonQuery(sql);
        db.Close();
    }

    public static bool DeleteUser(AppInfo appInfo, string userName)
    {
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"delete from `NPDES`.`Admin-Users` where `UserID` = '{userName}';";
        db.ExecNonQuery(sql);
        var success = db.Success;
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
            var sql = $"insert into `NPDES`.`Admin-UserRoles` values ('{userName}', '{role}');";
            db.ExecNonQuery(sql);
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
            var sql = $"delete from `NPDES`.`Admin-UserRoles` where `UserId`= '{userName}' and `RoleID` = '{role}';";
            db.ExecNonQuery(sql);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
    }

    public static List<string> AllAssignedRoles(string userid)
    {
        var assignedRoles = new List<string>(32);
        var appInfo = new AppInfo("NPDES", "WebUI", "DbNPDES");
        var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select `RoleID` from `NPDES`.`Admin-UserRoles` where `UserID` = '{userid} 'order by `RoleID`");
        if (!rdr!.HasRows) return assignedRoles;
        while (rdr.Read())
        {
            if ((string) rdr["RoleId"] == "None" || (string) rdr["RoleId"] == "SuperAdmin") continue;
            assignedRoles.Add((string) rdr["RoleID"]);
        }
        return assignedRoles;
    }

    public static bool CanUserUseApp(AppInfo appInfo, string userid, string app)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select `App` from `NPDES`.`Admin-AppsByUserView` where `User` = '{userid}' and `App` = '{app}' and `Role Enabled` = true";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows == false) success = false;
        db.Close();
        return success;
    }

    public static IEnumerable<UserRec> GetUsers(AppInfo appInfo, string searchString)
    { 
        var ue = new List<UserRec>(512);
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select * from `NPDES`.`Admin-Users` where `UserID` like '{searchString}'";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                if ((string) rdr["UserID"] == "Init" || (string) rdr["UserID"] == "SuperAdmin") continue;
                var rec = new UserRec
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

    public static List<AppsByUserRec> GetAppsByUser(AppInfo appInfo, string userid)
    {
        var result = new List<AppsByUserRec>(512);
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select * from `NPDES`.`Admin-AppsByUserView` where `User` = '{userid}'";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows)
        {
            while (rdr.Read())
            {
                if ((string) rdr["User"] == "Init" || (string) rdr["User"] == "SuperAdmin") continue;
                var rec = new AppsByUserRec
                {
                    User = (string) rdr["User"],
                    Role = (string) rdr["Role"],
                    ReadOnly = (bool) rdr["ReadOnly"],
                    RoleLevel = (int) rdr["Role Level"],
                    RoleEnabled = (bool) rdr["Role Enabled"],
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

    public static bool IsUserSuperAdmin(AppInfo appInfo, string userid)
    {
        var superAdmin = false;
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select * from `NPDES`.`Admin-UserRoles` where `UserID` = '{userid}' and `RoleID` = 'SuperAdmin'";
        var rdr = db.ExecQuery(sql);
        if (rdr!.HasRows) superAdmin = true;
        db.Close();

        return superAdmin;
    }

    public static bool IsReadOnly(AppInfo appInfo, string userid, string appid)
    {
        var readOnly = false;
        using var db = new MariaDb(appInfo);
        db.Open();
        var sql = $"select * from `NPDES`.`Admin-AppsByUserView` where `User` = '{userid}' and `App` = '{appid}' and `ReadOnly` = false;";
        var rdr = db.ExecQuery(sql);
        if (!rdr!.HasRows) readOnly = true;
        db.Close();

        return readOnly;
    }
    
}

