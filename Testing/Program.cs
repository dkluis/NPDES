using Libraries;
using GemBox.Spreadsheet;

var applicationInfo = new AppInfo("NPDES", "Testing", "DbProduction");

var logFile = applicationInfo.TxtFile;

logFile.Start();
logFile.Write($"Running Testing app", "Main Function");
using var prodDb = new MariaDb(applicationInfo);
{
    prodDb.Open();
    Console.WriteLine($"DB Open success: {prodDb.Success}");
    prodDb.Close();
    Console.WriteLine($"DB Close success {prodDb.Success}");
}

SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
var excelfile = new Excel(applicationInfo, "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/ExcelData/Background_Files_Test.xlsx");
Console.WriteLine($"Sheet Name is {excelfile!.ActiveSheet!.Name}");
foreach (var sheet in excelfile!.Sheets!)
{
    Console.WriteLine(sheet.Name);
}



logFile.Stop();


/*
var con = new AccessConnection("Data Source=/home/dick/NPDES-System/AccessData/WASTEEMS.accdb");
var a = con.Database;
var cmd = new AccessCommand("select * from `BIENPRT`", con);
var rdr = cmd.ExecuteReader(); 
*/


/*
 var accessString = new OdbcConnectionStringBuilder
{
    Driver = "Actual Access"
};
accessString.Add("Dbq", "/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/AccessData/WATERDAT.accdb");
var Access = new OdbcConnection("Driver={Actual Access};Dbq=/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/AccessData/WATERDAT.accdb; Uid=; Pwd=;");



var userInfo = new User("dkluis", "AAA123");
Console.WriteLine($"The user is: {userInfo.firstName} {userInfo.lastName}, has superAdmin privs: {userInfo.superAdmin}");
Console.WriteLine($"His roles are {userInfo.userRoles} and special functions are {userInfo.userApps}");
Console.WriteLine();

var excelInfo = new IronSoftExcelControl("/media/psf/BVPV/BVPV/Data/Background_Files.xlsx", "Constituents");
//var excelInfo = new IronSoftExcelControl("X:/BVPV/Data/Background_Files.xlsx", "Cost_Centers");

//Validate that this worksheet still has the right header info and that nothing has changed.
Console.WriteLine($"The Data Validation result is: {excelInfo.ValidateHeader()}");

//Output Text to the console for a selection of the worksheet
Console.WriteLine($"Header Fields are: {excelInfo.Ws["a1:f1"]}");

Console.WriteLine($"Data Content is: {excelInfo.Ws["a2:f2"]}");


foreach (var cell in excelInfo.Ws["a2:f3"])
{
    // This is where code would go to validate the info and insert into a DB record during import
    Console.WriteLine(cell);
}

Console.WriteLine("Hit Enter: ");
Console.ReadLine();

*/


//  **************** Comment Section for Tryout Code

//Open Excel file and its default worksheet
//var excelInfo = new IronSoftExcelControl("X:/BVPV/NPDES Supporting Docs/NPDES - 201118, 180-113797-1 (NPDES).xlsx");

//Open an Excel and a specific Worksheet
//var excelInfo = new ExcelLib.IronSoftExcelControl("z:/Menu/Audiobook Lib.xlsx", "Audiobook 1 Lib");

//Open an Excel and create a new worksheet
//var excelInfo = new ExcelLib.IronSoftExcelControl("z:/Menu/Audiobook Lib.xlsx", "New 1 Tab", true);
//excelInfo.Save();

//Validate the HeaderLine of a specific excel
//Console.WriteLine($"The Data Validation result is: {excelInfo.ValidateData()}");