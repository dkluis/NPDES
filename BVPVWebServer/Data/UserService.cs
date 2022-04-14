using Libraries;

namespace BVPVWebServer.Data;

public class UserService
{
    public User LoadUser(AppInfo appInfo, string username, string unencryptedPassword, bool checkPw = true)
    {
        var user = new User();
        user.ValidPassword = false;
        user.ValidUser = false;
        user.HighestRoleId = string.Empty;
        user.UserId = username;

        using var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from Users where `UserID` = '{username}';");
        if (rdr is {HasRows: false})
        {
            user.ValidUser = false;
            user.UserId = string.Empty;
            db.Close();
            return user;
        }

        var storedPassword = string.Empty;
        var storedSalt = string.Empty;
        while (rdr != null && rdr.Read())
        {
            storedPassword = rdr[1].ToString() ?? string.Empty;
            storedSalt = rdr[2].ToString() ?? string.Empty;
        }

        db.Close();
        user.ValidUser = true;
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(unencryptedPassword, storedSalt);
        if ((encryptedPassword != storedPassword) && !checkPw)
        {
            return user;
        }
        else
        {
            user.ValidPassword = true;
        }

        db.Open();
        rdr = db.ExecQuery(
            $"select * from UserRolesView where `User` = '{user.UserId}' order by `Role Level` Limit 1;");
        if (rdr is {HasRows: true})
        {
            while (rdr.Read())
            {
                user.HighestRoleId = (string) rdr!["Role"];
            }
        }

        db.Close();
        
        return user;
    }

    public bool AddUser(AppInfo appInfo, string userName, string password)
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
        string sql = $"insert into Users values ('{userName}', '{encryptedPasswrd}', '{mySalt}');";
        db.ExecNonQuery(sql, true);
        success = db.Success;
        db.Close();
        return success;
    }

    public bool AddUserRoles(AppInfo appInfo, string userName, string[] userRoles)
    {
        var success = true;
        using var db = new MariaDb(appInfo);
        db.Open();
        foreach (var role in userRoles)
        {
            string sql = $"insert into UserRoles values ('{userName}', '{role}');";
            db.ExecNonQuery(sql, true);
            if (!db.Success) success = false;
        }

        db.Close();
        return success;
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

    public UserElements GetUsers(string searchsting)
    {
        UserElements ue = new();

        return ue;
    }
}

public class User
{
    public string UserId { get; set; }
    public string HighestRoleId { get; set; }
    public bool ValidUser { get; set; }
    public bool ValidPassword { get; set; }
    
}

public class UserElement
{
    public UserElement()
    {
        UserId = string.Empty;
        Password = string.Empty;
        Salt = string.Empty;
    }

    public string UserId { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
        
}

public class UserElements
{
    public UserElements()
    {
        Users = new List<UserElement>();
    }

    public List<UserElement> Users { get; set; }
}