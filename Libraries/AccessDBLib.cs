using System.Data;
using System.Data.CData.Access;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using Org.BouncyCastle.Cms;

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
        var cmd = new AccessCommand($"select count(*) from `{table}`;", con);
        var rdr = cmd.ExecuteReader();
        return rdr;
    }
    
    public IEnumerable<ARCOSampInfo> GetAllSampInfoRecs()
    {
        var recsFound = new List<ARCOSampInfo>(35840);
        var con = new AccessConnection($"DataSource={BaseConfig.AccessData}/WaterDAT2.accdb; " +
                                       $"Logfile={BaseConfig.LogsPath}/CData.log; " +
                                       $"Verbosity=3;");
        con.Open();
        var cmd = new AccessCommand($"select * from `ARCOSampInfo`;", con);
        var rdr = cmd.ExecuteReader();
        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                var rec = new ARCOSampInfo()
                {
                    HLALABID = rdr["HLALABID"].ToString() != "" ? (string) rdr["HlALABID"] : null,
                    OBJID = rdr["OBJID"].ToString() != "" ? (string) rdr["OBJID"] : null,
                    PERMNUM = rdr["PERMNUM"].ToString() != "" ? (string) rdr["PERMNUM"] : null,
                    ORDERNUM = rdr["ORDERNUM"].ToString() != "" ? (string) rdr["ORDERNUM"] : null,
                    SAMPLEID = rdr["SAMPLEID"].ToString() != "" ? (string) rdr["SAMPLEID"] : null,
                    SAMPTYPE = rdr["SAMPTYPE"].ToString() != "" ? (string) rdr["SAMPTYPE"] : null,
                    SAMPBY = rdr["SAMPBY"].ToString() != "" ? (string) rdr["SAMPBY"] : null,
                    COLLDATE = rdr["COLLDATE"].ToString() != "" ? (DateTime) rdr["COLLDATE"] : new DateTime(1990,01,01,00,00,00),
                    COLLTIME = rdr["COLLTIME"].ToString() != "" ? (DateTime) rdr["COLLTIME"] : new DateTime(1990,01,01,00,00,00),
                    SAMPDATE = rdr["SAMPDATE"].ToString() != "" ? (DateTime) rdr["SAMPDATE"] : new DateTime(1990,01,01,00,00,00),
                    LABNAME = rdr["LABNAME"].ToString() != "" ? (string) rdr["LABNAME"] : null,
                    RECDATE = rdr["RECDATE"].ToString() != "" ? (DateTime) rdr["RECDATE"] : new DateTime(1990,01,01,00,00,00),
                    COMMENT = rdr["COMMENT"].ToString() != "" ? (string) rdr["COMMENT"] : null,
                    ENTERDATE = rdr["ENTERDATE"].ToString() != "" ? (DateTime) rdr["ENTERDATE"] : new DateTime(1990,01,01,00,00,00),
                    SOURCE = rdr["SOURCE"].ToString() != "" ? (string) rdr["SOURCE"] : null
                };
                recsFound.Add(rec);
            }
        }
        return recsFound;
    }

    public void TransferTableData(string accessDb, string aTable, string mariaDb, string mTable)
    {
        
    }

}