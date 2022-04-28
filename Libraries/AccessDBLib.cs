using System.Data.CData.Access;


namespace Libraries;

public class AccessDb
{
    public AccessDataReader Try()
    {
        var con = new AccessConnection($"DataSource={BaseConfig.AccessData}/WASTEEMS.accdb; " +
                                       $"Logfile={BaseConfig.LogsPath}/CData.log; " +
                                       $"Verbosity=3;");
        var cmd = new AccessCommand("select * from bienprt;", con);
        var rdr = cmd.ExecuteReader();
        return rdr;
    }
}