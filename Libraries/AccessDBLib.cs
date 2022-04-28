using System.Data.CData.Access;


namespace Libraries;

public class AccessDb
{
    public AccessDataReader Try()
    {
        var con = new AccessConnection("DataSource=/Volumes/HD-Data-CA-Server/BVPV/NPDES-System/AccessData/WASTEEMS.accdb; " +
                                       "Logfile=C:/Users/Dick/Documents/Development/Data/CData.log; Verbosity=3;");
        var cmd = new AccessCommand("select * from bienprt;", con);
        var rdr = cmd.ExecuteReader();
        return rdr;
    }
}