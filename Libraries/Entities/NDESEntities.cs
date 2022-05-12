namespace Libraries.Entities;

public class AppRec
{
    public AppRec()
    {
        AppId = string.Empty;
        FunctionId = string.Empty;
        ReportApp = false;
    }

    public string AppId { get; init; }
    public string FunctionId { get; init; }
    public bool ReportApp { get; init; }
}

public class AppRoleRec
{
    public AppRoleRec()
    {
        AppId = string.Empty;
        RoleId = string.Empty;
    }
    
    public string AppId { get; init; }
    public string RoleId { get; init; }
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

    public string UserId { get; init; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Enabled { get; init; }
        
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
    
    public string User { get; init; }
    public string Role { get; init; }
    public bool ReadOnly { get; set; }
    public int RoleLevel { get; set; }
    public bool RoleEnabled { get; init; }
    public string App { get; init; }
    public bool Report { get; init; }
    public string Function { get; init; }
}

public class UserLoginRec
{
    public string UserId { get; set; }  = string.Empty;
    public bool ValidUser { get; set; }
    public bool ValidPassword { get; set; }
    public bool Enabled { get; set; }
    
}

/// <summary>
/// Record actions done with and too external files
/// Like the excel files to import or generated excel files to download
/// or Reports, etc.
/// </summary>
public class ExternalFilesAuditRec
{
    public string User { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string OriginalFileName { get; init; } = string.Empty;
    public string Function { get; init; } = string.Empty;
    public DateTime DownloadDateTime { get; init; }
    public DateTime ValidateDateTime { get; init; }
    public string ValidateUser { get; init; } = string.Empty;
    public DateTime ProcessDateTime { get; init; }
    public string ProcessUser { get; init; } = string.Empty;
    public DateTime ArchiveDateTime { get; init; }
    public string ArchiveUser { get; init; } = string.Empty;
}