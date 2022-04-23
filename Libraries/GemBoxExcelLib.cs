namespace Libraries;

using GemBox.Spreadsheet;

public class Excel
{
    public readonly ExcelFile? Book;
    public readonly ExcelWorksheetCollection? Sheets;
    public ExcelWorksheet? ActiveSheet;
    
    public Excel()
    {
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
    }

    public Excel (AppInfo appInfo, string file)
    {
        Book = ExcelFile.Load(file);
        Sheets = Book.Worksheets;
        ActiveSheet = Sheets.ActiveWorksheet;
    }

    public Excel (AppInfo appInfo, string file, string sheetName)
    {
        Book = ExcelFile.Load(file);
        Sheets = Book.Worksheets;
        ActiveSheet = Sheets![sheetName];
    }
}