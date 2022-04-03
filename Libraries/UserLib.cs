using System.Diagnostics;
using BCrypt.Net;

namespace Libraries;

public class User
{
    public readonly string UserId;
    private readonly string _encryptedPassword;
    public readonly string RoleId;
    public readonly List<KeyValuePair<string, string>> AppAndFunctionIds;
    public readonly bool ValidUser;
    public readonly bool ValidPassword;

    public User(AppInfo appInfo, string username, string unencryptedPassword)
    {
        ValidPassword = false;
        ValidUser = false;
        using var db = new MariaDb(appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from Users where `UserID` = '{username}';");
        if (!rdr.HasRows)
        {
            _encryptedPassword = string.Empty;
            RoleId = string.Empty;
            db.Close();
            return;
        }

        UserId = username;
        AppAndFunctionIds = new List<KeyValuePair<string, string>>();
        while (rdr.Read())
        {
            _encryptedPassword = rdr[1].ToString() ?? string.Empty;
            RoleId = rdr[2].ToString() ?? string.Empty;
        }

        db.Close();
        ValidUser = true;
        if (_encryptedPassword != unencryptedPassword)
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
            AppAndFunctionIds!.Add(new KeyValuePair<string, string>
                (rdr[2].ToString() ?? string.Empty, rdr[3].ToString() ?? string.Empty));
        }

        db.Close();
    }
}