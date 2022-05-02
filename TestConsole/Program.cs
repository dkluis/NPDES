// See https://aka.ms/new-console-template for more information

using System.Data;
using Libraries;

Console.WriteLine("Hello, World!");

var adb = new AccessDb();
/*
var rdr = adb.GetFromTable("WASTEEMS.accdb", "Biennial_Report_Query");

while (rdr.Read())
{
    for (var x = 0; x < rdr.FieldCount; x++)
    {
        Console.Write($"Field {x}: {rdr[x]} -> ");
    }
    Console.WriteLine();
}
*/

const string accessDb = "WATEREMS.accdb";

var tables = adb.GetAllTables(accessDb);
var views = adb.GetAllTables(accessDb, "Views");

// Below Code is for extracting all schema info for selected accessDb
/*
var appInfo = new AppInfo("App", $"{accessDb.Replace(".accdb", "")} Schema Info", "DbCon");
var outPutFile = appInfo.TxtFile;
foreach (DataRow table in tables.Rows)
{
    var columns = adb.GetAllColumns(accessDb, (string) table["TABLE_NAME"]);
    foreach (DataRow column in columns.Rows)
    {
        outPutFile.WriteNoHead($"Table,{table["TABLE_NAME"]},Column,{column["COLUMN_NAME"]},{column["IS_KEY"]},{column["DATA_TYPE"]}");
    }
}

foreach (DataRow table in views.Rows)
{
    var columns = adb.GetAllColumns(accessDb, (string) table["TABLE_NAME"]);
    foreach (DataRow column in columns.Rows)
    {
        outPutFile.WriteNoHead($"View,{table["TABLE_NAME"]},Column,{column["COLUMN_NAME"]},{column["IS_KEY"]},{column["DATA_TYPE"]}");
    }
}
*/

// Below Code is for getting a record count for all Tables and Views for the accessDB selected
var appInfo = new AppInfo("App", $"{accessDb.Replace(".accdb", "")} Record Counts", "DbCon");
var outPutFile = appInfo.TxtFile;
foreach (DataRow table in tables.Rows)
{
    var tbl = (string) table["TABLE_NAME"];
    var rdr = adb.GetRecordCount(accessDb, tbl);
    outPutFile.WriteNoHead($"{table["TABLE_NAME"]},{rdr[0]}");
}

