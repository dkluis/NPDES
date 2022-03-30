using IronXL;

namespace Libraries;

public class ExcelControl : IDisposable
{
    private readonly WorkBook _wb;
    public readonly WorkSheet Ws;

    private static void ExcelLicense()
    {
        License.LicenseKey = 
            // ReSharper disable StringLiteralTypo
            "IRONXL.DICKKLUIS.19793-D9FD3E65ED-SVYK67CSWRVK7AS3-FQLCUIJE3SWH-4Q7QQCLBJBHE-VOYLPT6KUX5G-3UFNWH6DYZ74-FIQTJB-TKHO64N7CFSFEA-DEPLOYMENT.TRIAL-J3ST52.TRIAL.EXPIRES.17.APR.2022";
        // ReSharper restore StringLiteralTypo
        if (License.IsLicensed) return;
        Console.WriteLine("IronXL License not working");
        Environment.Exit(999);
    }

    public ExcelControl(string file, string worksheet, bool create = false)
    {
        ExcelLicense();
        _wb = LoadWorkBook(file);
        try
        {
            Ws = create ? _wb.CreateWorkSheet(worksheet) : _wb.GetWorkSheet(worksheet);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error with worksheet {worksheet}: {e.Message}");
            Environment.Exit(999);
        }

    }

    public ExcelControl(string file)
    {
        ExcelLicense();
        _wb = LoadWorkBook(file);
        Ws = _wb.WorkSheets.First();
    }

    public void Save()
    {
        _wb.Save();
    }

    public bool ValidateHeader(string filetype = "")
    {
        var valid = true;
        //ToDo replace with filetype headerList info from DB
        var headerList = new List<KeyValuePair<string, string>>
        {
            new ("A1", "Constituent"),
            new ("b1", "TRICHEM"),
            new ("c1", "TRIRPT"),
            new ("d1", "CASNUM"),
            new ("e1", "DEMIN"),
            new ("f1", "COMMENTS")
        };

        foreach (var (key, value) in headerList)
        {
            if (Ws[key].ToString().ToLower() == value.ToLower()) continue;
            Console.WriteLine($"Error:  Cell {key} does not contain {value} but {Ws[key].ToString()}");
            valid = false;
        }

        return valid;
    }

    public bool ValidateData(string filetype = "", string datatype = "")
    {
        if (!ValidateHeader(filetype)) return false;
        
        var valid = true;
        var dataList = new List<KeyValuePair<string, string>>
        {
            new ("A", "string"),
            new ("B", "int"),
            new ("E", "date"),
            new ("J", "float")
        };
        var start = 2;
        var end = 13;

        for (var i = start; i <= end; i++)
        {
            Console.WriteLine();
            Console.Write($"Row {i}: ");
            var rowValid = true;
            foreach (var (key, value) in dataList)
            {
                //Console.Write($"Cell: {key}{i} :=: ");
                switch (value)
                {
                    case "string":
                        break;
                    case "int":
                        var cellValue = Ws[$"{key}{i}"].IntValue;
                        if (cellValue.GetType().ToString() != "System.Int32")
                        {
                            Console.Write($"Cell {key}{i} does not contains an integer: {cellValue} :=: ");
                            rowValid = false;
                        }
                        break;
                    case "date":
                        var dateValue = Ws[$"{key}{i}"].StringValue;
                        if (!ValidateDateString(dateValue))
                        {
                            Console.Write($"Cell {key}{i} does not contains a date: {dateValue} :=: ");
                            rowValid = false;
                        }
                        break;
                    case "float":
                        var floatValue = Ws[$"{key}{i}"].DoubleValue;
                        Console.WriteLine(floatValue);
                        if (floatValue.GetType().ToString() != "System.Double")
                        {
                            Console.Write($"Cell {key}{i} does not contains an float: {floatValue} :=: ");
                            rowValid = false;
                        }
                        break;
                    default:
                        Console.Write($"No case is setup for {value} validation :=: ");
                        break;
                }
                //Console.Write($"{Ws[$"{key}{i}"]} :=: ");
            }
            Console.WriteLine();
            Console.WriteLine($"Row {i} validity is: {rowValid}");
            if (!rowValid)
            {
                valid = false;
            }
        }
        return valid;
    }

    private static bool ValidateDateString(string date)
    {
        try
        {
            DateTime.Parse(date);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static WorkBook LoadWorkBook(string file)
    {
        var workBook = new WorkBook();
        try
        {
            workBook = WorkBook.Load(file);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Loading the excel file: {e.Message} ");
            Environment.Exit(999);
        }

        return workBook;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}