﻿namespace Libraries;

public class User
{
    public readonly string UserId;
    public readonly List<string> RoleId;
    public readonly Dictionary<string, string> AppAndFunctionIds;
    public readonly bool ValidUser;
    public readonly bool ValidPassword;
    private readonly AppInfo _appInfo;

    public User(AppInfo appInfo)
    {
        UserId = "";
        RoleId = new List<string>();
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
        RoleId = new List<string>();
        AppAndFunctionIds = new Dictionary<string, string>();
        UserId = username;
        
        using var db = new MariaDb(_appInfo);
        db.Open();
        var rdr = db.ExecQuery($"select * from Users where `UserID` = '{username}';");
        if (!rdr.HasRows)
        {
            ValidUser = false;
            UserId = string.Empty;
            db.Close();
            return;
        }

        var storedPassword = string.Empty;
        var storedSalt = string.Empty;
        while (rdr.Read())
        {
            storedPassword = rdr[1].ToString() ?? string.Empty;
            storedSalt = rdr[2].ToString() ?? string.Empty;
        }
        db.Close();
        ValidUser = true;
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(unencryptedPassword, storedSalt);
        if (encryptedPassword != storedPassword)
        {
            return;
        }
        else
        {
            ValidPassword = true;
        }
        
        db.Open();
        rdr = db.ExecQuery($"select * from UserRoles where `UserID` = '{UserId}';");
        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                RoleId.Add(rdr[1].ToString()!);
            }
        }
        db.Close();
        
        db.Open();
        rdr = db.ExecQuery($"select * from AppsByUser where `User` = '{UserId}';");
        if (!rdr.HasRows) return;
        
        while (rdr.Read())
        {
            if (rdr[3].ToString() is null || rdr[5].ToString() is null) continue;
            AppAndFunctionIds.Add(rdr[3].ToString()!, rdr[5].ToString()!);
        }

        db.Close();
    }

    public bool CanUserUseApp(string? app)
    {
        return AppAndFunctionIds.TryGetValue(app!, out app);
    }

    public bool AddUser(string userName, string password)
    {
        var success = false;
        if (ValidUser)
        {
            _appInfo.TxtFile.Write($"Error adding user: {userName}", 
                "UserLib-Add", 0);
            return success;
        }

        var mysalt = BCrypt.Net.BCrypt.GenerateSalt();
        var encryptedpasswrd = BCrypt.Net.BCrypt.HashPassword(password, mysalt);
        using var db = new MariaDb(_appInfo);
        db.Open();
        string sql = $"insert into Users values ('{userName}', '{encryptedpasswrd}', '{mysalt}');";
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