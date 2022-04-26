namespace Libraries;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class DownloadRec
{
    public string? User { get; set; }
    public string? FileName { get; set; }
    public string? OriginalFileName { get; set; }
    public string? Function { get; set; }
    public DateTime DownloadDateTime { get; set; }
    public DateTime ValidateDateTime { get; set; }
    public string? ValidateUser { get; set; }
    public DateTime ProcessDateTime { get; set; }
    public string? ProcessUser { get; set; }
}