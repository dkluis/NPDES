namespace Libraries;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class DownloadRec
{
    public string User { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string Function { get; set; } = string.Empty;
    public DateTime DownloadDateTime { get; set; }
    public DateTime ValidateDateTime { get; set; }
    public string ValidateUser { get; set; } = string.Empty;
    public DateTime ProcessDateTime { get; set; }
    public string ProcessUser { get; set; } = string.Empty;
    public DateTime ArchiveDateTime { get; set; }
    public string ArchiveUser { get; set; } = string.Empty;
}