namespace Libraries;

public class User
{
    public readonly string UserId;
    public readonly string RoleId;
    public readonly Dictionary<string, string> AppAndFunctionIds;
    public readonly bool ValidUser;
    public readonly bool ValidPassword;
    private readonly AppInfo _appInfo;

    public User(AppInfo appInfo)
    {
        UserId = "";
        RoleId = "";
        AppAndFunctionIds = new Dictionary<string, string>();
        ValidPassword = false;
        ValidUser = false;
        _appInfo = appInfo;
    }

    public User(AppInfo appInfo, string username, string unencryptedPassword)
    {
        ValidPassword = false;
        ValidUser = false;
        _appInfo = appInfo;
        RoleId = "";
        AppAndFunctionIds = new Dictionary<string, string>();
        var encryptedPassword = "";
        UserId = username;
        
        using var db = new MariaDb(_appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from Users where `UserID` = '{username}';");
        if (!rdr.HasRows)
        {
            RoleId = string.Empty;
            db.Close();
            return;
        }

        AppAndFunctionIds = new Dictionary<string, string>();
        while (rdr.Read())
        {
            encryptedPassword = rdr[1].ToString() ?? string.Empty;
            RoleId = rdr[2].ToString() ?? string.Empty;
        }

        db.Close();
        ValidUser = true;
        if (encryptedPassword != unencryptedPassword)
        {
            return;
        }
        else
        {
            ValidPassword = true;
        }

        db.Open();
        rdr = db.ExecQuery($"select * from AppsByUser where `User` = '{UserId}' and `Role` = '{RoleId}'");
        if (!rdr.HasRows) return;
        
        while (rdr.Read())
        {
            if (rdr[2].ToString() is null || rdr[3].ToString() is null) continue;
            AppAndFunctionIds.Add(rdr[2].ToString()!, rdr[3].ToString()!);
        }

        db.Close();
    }

    public bool CanUserUseApp(string? app)
    {
        return AppAndFunctionIds.TryGetValue(app!, out app);
    }

    public bool AddUser(string userName, string roleId)
    {
        var success = false;
        if (ValidUser)
        {
            _appInfo.TxtFile.Write($"Error adding user: {userName}", 
                "UserLib-Add", 0);
            return success;
        }
        
        using var db = new MariaDb(_appInfo);
        db.Open();
        string sql = $"insert into Users values ('{userName}', 'Reset', '{roleId}');";
        var rows = db.ExecNonQuery(sql);
        Console.WriteLine(rows);
        success = db.Success;
        db.Close();
        return success;
    }

    public List<string> ValidateAddUser(string userName, string roleId)
    {
         var errors = new List<string>();
         using var db = new MariaDb(_appInfo);
         db.Open();
         var rdr = db.ExecQuery($"select * from Roles where `RoleID` = '{roleId}';");
         if (!rdr.HasRows) errors.Add($"RoleID: '{roleId}' is not valid");
         return errors;
    }
}