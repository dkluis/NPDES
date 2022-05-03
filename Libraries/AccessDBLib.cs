using System.Data;
using System.Data.CData.Access;

namespace Libraries;

public class AccessDb
{

    public AccessDataReader GetFromTable(string accessDb, string table)
    {
        var con = new AccessConnection($"DataSource={BaseConfig.AccessData}/{accessDb}; " +
                                       $"Logfile={BaseConfig.LogsPath}/CData.log; " +
                                       $"Verbosity=3;");

        con.Open();
        var cmd = new AccessCommand($"select * from `{table}`;", con);
        var rdr = cmd.ExecuteReader();
        var result = rdr;
        //con.Close();
        return result;
    }

    public DataTable GetAllColumns(string accessDb, string table)
    {
        var con = new AccessConnection($"DataSource={BaseConfig.AccessData}/{accessDb}; " +
                                       $"Logfile={BaseConfig.LogsPath}/CData.log; " +
                                       $"Verbosity=3;");
        con.Open();
        var result = con.GetSchema("Columns", new string[] {table} );
        con.Close();
        return result;
    }

    public DataTable GetAllTables(string accessDb, string schemaType = "Tables")
    {
        var con = new AccessConnection($"DataSource={BaseConfig.AccessData}/{accessDb}; " +
                                       $"Logfile={BaseConfig.LogsPath}/CData.log; " +
                                       $"Verbosity=3;");
        con.Open();
        var result = con.GetSchema(schemaType);
        con.Close();
        return result;
    }

    public AccessDataReader GetRecordCount(string accessDb, string table)
    {
        var con = new AccessConnection($"DataSource={BaseConfig.AccessData}/{accessDb}; " +
                                       $"Logfile={BaseConfig.LogsPath}/CData.log; " +
                                       $"Verbosity=3;");
        con.Open();
        Console.WriteLine($"Working on Table: {table}");
        var cmd = new AccessCommand($"select count(*) from `{table}`;", con);
        var rdr = cmd.ExecuteReader();
        return rdr;
    }

    public void TransferTableData(string accessDb, string aTable, string mariaDb, string mTable)
    {
        
    }

}