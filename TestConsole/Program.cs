// See https://aka.ms/new-console-template for more information

using Libraries;

Console.WriteLine("Hello, World!");


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

var appInfo = new AppInfo("NPDES", "SampInfo", "DbWaterDat");
var adb = new AccessDb(appInfo);
var sampInfoRecs = adb.GetAllSampInfoRecs();
var mdb = new MariaDb(appInfo);
mdb.Open();
foreach (var rec in sampInfoRecs)
{
    var values = $"'{rec.HLALABID}', '{rec.OBJID}', '{rec.PERMNUM}', " +
                 $"'{rec.ORDERNUM}', '{rec.SAMPLEID}', '{rec.SAMPTYPE}', " +
                 $"'{rec.SAMPBY}', '{rec.COLLDATE.ToString("yyyy/MM/dd hh:mm:ss")}', '{rec.COLLTIME.ToString("yyyy/MM/dd hh:mm:ss")}', " +
                 $"'{rec.SAMPDATE.ToString("yyyy/MM/dd hh:mm:ss")}', '{rec.LABNAME}', '{rec.RECDATE.ToString("yyyy/MM/dd hh:mm:ss")}', " +
                 $"'{rec.COMMENT}', '{rec.ENTERDATE.ToString("yyyy/MM/dd hh:mm:ss")}', '{rec.SOURCE}'";
    mdb.ExecNonQuery($"insert into ARCOSampInfo values ({values});");
}

/*
var tables = adb.GetAllTables(accessDb);
var views = adb.GetAllTables(accessDb, "Views");
*/

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

/*
// Below Code is for getting a record count for all Tables and Views for the accessDB selected
var appInfo = new AppInfo("App", $"{accessDb.Replace(".accdb", "")} Record Counts", "DbCon");
var outPutFile = appInfo.TxtFile;
foreach (DataRow table in tables.Rows)
{
    var tbl = (string) table["TABLE_NAME"];
    var rdr = adb.GetRecordCount(accessDb, tbl);
    outPutFile.WriteNoHead($"{table["TABLE_NAME"]},{rdr[0]}");
}
*/
