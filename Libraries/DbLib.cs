using MySqlConnector;

namespace Libraries;

public class DbLib : IDisposable
{
    private readonly MySqlConnection _conn;
    //private readonly TextFileHandler _mDbLog;
    private MySqlCommand _cmd;
    private bool _connOpen;
    private MySqlDataReader _rdr;

    private int _rows;
    public bool Success;

    public DbLib()
    {
        Success = false;
        try
        {
            //_conn = new MySqlConnection(appInfo.ActiveDbConn);
            Success = true;
        }
        catch (Exception e)
        {
            //_mDbLog.Write($"MariaDB Class Connection Error: {e.Message}", null, 0);
        }
    }
    
    void IDisposable.Dispose()
    {
        //Close();
        GC.SuppressFinalize(this);
    }
}