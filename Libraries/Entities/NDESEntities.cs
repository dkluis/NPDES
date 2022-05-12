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