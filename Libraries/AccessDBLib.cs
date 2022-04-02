using System.Data.Odbc;


namespace Libraries;

public class AccessDb
{
    public void Try()
    {
        OdbcConnection con =
            new OdbcConnection(
                "Driver={Microsoft Access Driver(*.mdb, *.accdb)}; DBQ=/media/psf/Styropek/NPDES-System/WASTEEMS.accdb;");
        OdbcCommand command = new OdbcCommand("select * from table");

        command.Connection = con;
        con.Open();
        var reader = command.ExecuteReader();
    }
}