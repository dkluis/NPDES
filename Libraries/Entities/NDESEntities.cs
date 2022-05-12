namespace Libraries.Entities;

public class AppRec
{
    public AppRec()
    {
        AppId = string.Empty;
        FunctionId = string.Empty;
        ReportApp = false;
    }

    public string AppId { get; set; }
    public string FunctionId { get; set; }
    public bool ReportApp { get; set; }
}

public class AppRoleRec
{
    public AppRoleRec()
    {
        AppId = string.Empty;
        RoleId = string.Empty;
    }
    
    public string AppId { get; set; }
    public string RoleId { get; set; }
}

public class RoleRec
{
    public string RoleId { get; set; }  = string.Empty;
    public int RoleLevel { get; set; }
    public bool ReadOnly { get; set; }
    public bool Enabled { get; set; }
}

public class UserRec
{
    public UserRec()
    {
        UserId = string.Empty;
        Password = string.Empty;
        Salt = string.Empty;
        Enabled = true;
    }

    public string UserId { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Enabled { get; set; }
        
}

public class AppsByUserRec
{
    public AppsByUserRec()
    {
        User = string.Empty;
        Role = string.Empty;
        ReadOnly = false;
        RoleLevel = 99;
        RoleEnabled = true;
        App = string.Empty;
        Report = true;
        Function = string.Empty;
    }
    
    public string User { get; set; }
    public string Role { get; set; }
    public bool ReadOnly { get; set; }
    public int RoleLevel { get; set; }
    public bool RoleEnabled { get; set; }
    public string App { get; set; }
    public bool Report { get; set; }
    public string Function { get; set; }
}

public class UserLoginRec
{
    public string UserId { get; set; }  = string.Empty;
    public bool ValidUser { get; set; }
    public bool ValidPassword { get; set; }
    public bool Enabled { get; set; }
    
}