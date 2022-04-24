using MySqlConnector;

namespace Libraries;

public class MariaDb : IDisposable
    {
        private readonly MySqlConnection _conn;
        private readonly TextFileHandler _mDbLog;
        private MySqlCommand _cmd;
        private bool _connOpen;
        private MySqlDataReader? _rdr;

        public Task<int>? TaskRows;
        public int Rows;
        public bool Success;
        public string ErrorMessage;

        public MariaDb(AppInfo appInfo)
        {
            _mDbLog = appInfo.TxtFile;
            _conn = new MySqlConnection();
            _cmd = new MySqlCommand();
            ErrorMessage = string.Empty;

            Success = false;
            try
            {
                _conn = new MySqlConnection(appInfo.ActiveDbConn);
                Success = true;
            }
            catch (Exception e)
            {
                _mDbLog.Write($"MariaDB Class Connection Error: {e.Message}", "", 0);
                ErrorMessage = $"MariaDB Class Connection Error: {e.Message}";
            }
        }

        void IDisposable.Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        public void Open()
        {
            Success = true;
            try
            {
                _conn.Open();
                _connOpen = true;
            }
            catch (Exception e)
            {
                _mDbLog.Write($"MariaDB Class Open Error: {e.Message}", "", 0);
                ErrorMessage = $"MariaDB Class Open Error: {e.Message}";
                Success = false;
            }
        }

        public void Close()
        {
            Success = true;
            try
            {
                _conn.Close();
                _connOpen = false;
            }
            catch (Exception e)
            {
                _mDbLog.Write($"MariaDB Class Close Error: {e.Message}", "", 0);
                ErrorMessage = $"MariaDB Class Close Error: {e.Message}";
                Success = false;
            }
        }

        public MySqlCommand Command(string sql)
        {
            Success = true;
            try
            {
                if (!_connOpen) Open();
                _cmd = new MySqlCommand(sql, _conn);
                return _cmd;
            }
            catch (Exception e)
            {
                _mDbLog.Write($"MariaDB Class Command Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class Command Error: {e.Message}";
                Success = false;
                return _cmd;
            }
        }

        public MySqlDataReader? ExecQuery(string sql)
        {
            _cmd = Command(sql);
            Success = true;
            try
            {
                if (!_connOpen) Open();
                _rdr = _cmd.ExecuteReader();
                return _rdr;
            }
            catch (Exception e)
            {
                _mDbLog.Write($"MariaDB Class ExecQuery Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class Query Error: {e.Message}";
                Success = false;
                return _rdr;
            }
        }
        
        public async Task<MySqlDataReader?> ExecQueryAsync(string sql)
        {
            _cmd = Command(sql);
            Success = true;
            try
            {
                if (!_connOpen) Open();
                _rdr =  await _cmd.ExecuteReaderAsync();
                return _rdr;
            }
            catch (Exception e)
            {
                _mDbLog.Write($"MariaDB Class ExecQuery Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class Query Async Error: {e.Message}";
                Success = false;
                return _rdr;
            }
        }

        public int ExecNonQuery(string sql, bool ignore = false)
        {
            _cmd = Command(sql);
            Success = true;
            try
            {
                if (!_connOpen) Open();
                Rows = _cmd.ExecuteNonQuery();
                if (Rows == 0) Success = false;
                return Rows;
            }
            catch (Exception e)
            {
                if (!ignore) _mDbLog.Write($"MariaDB Class ExecNonQuery Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class NonQuery Error: {e.Message}";
                Success = false;
                return Rows;
            }
        }
    }