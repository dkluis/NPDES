using MySqlConnector;

namespace Libraries;

public class MariaDb : IDisposable
    {
        private MySqlConnection Conn { get; }
        private TextFileHandler MDbLog { get; }
        private MySqlCommand Cmd { get; set; }
        private bool ConnOpen { get; set; }
        private MySqlDataReader? Rdr { get; set; }
        
        private int Rows { get; set; }
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }

        public MariaDb(AppInfo appInfo)
        {
            MDbLog = appInfo.TxtFile;
            Conn = new MySqlConnection();
            Cmd = new MySqlCommand();
            ErrorMessage = string.Empty;
            Success = false;
            try
            {
                Conn = new MySqlConnection(appInfo.ActiveDbConn);
                Success = true;
            }
            catch (Exception e)
            {
                MDbLog.Write($"MariaDB Class Connection Error: {e.Message}", "", 0);
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
                Conn.Open();
                ConnOpen = true;
            }
            catch (Exception e)
            {
                MDbLog.Write($"MariaDB Class Open Error: {e.Message}", "", 0);
                ErrorMessage = $"MariaDB Class Open Error: {e.Message}";
                Success = false;
            }
        }

        public void Close()
        {
            Success = true;
            try
            {
                Conn.Close();
                ConnOpen = false;
            }
            catch (Exception e)
            {
                MDbLog.Write($"MariaDB Class Close Error: {e.Message}", "", 0);
                ErrorMessage = $"MariaDB Class Close Error: {e.Message}";
                Success = false;
            }
        }

        private MySqlCommand Command(string sql)
        {
            Success = true;
            try
            {
                if (!ConnOpen) Open();
                Cmd = new MySqlCommand(sql, Conn);
                return Cmd;
            }
            catch (Exception e)
            {
                MDbLog.Write($"MariaDB Class Command Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class Command Error: {e.Message}";
                Success = false;
                return Cmd;
            }
        }

        public MySqlDataReader? ExecQuery(string sql)
        {
            Cmd = Command(sql);
            Success = true;
            try
            {
                if (!ConnOpen) Open();
                Rdr = Cmd.ExecuteReader();
                return Rdr;
            }
            catch (Exception e)
            {
                MDbLog.Write($"MariaDB Class ExecQuery Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class Query Error: {e.Message}";
                Success = false;
                return Rdr;
            }
        }
        
        public async Task<MySqlDataReader?> ExecQueryAsync(string sql)
        {
            Cmd = Command(sql);
            Success = true;
            try
            {
                if (!ConnOpen) Open();
                Rdr =  await Cmd.ExecuteReaderAsync();
                return Rdr;
            }
            catch (Exception e)
            {
                MDbLog.Write($"MariaDB Class ExecQuery Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class Query Async Error: {e.Message}";
                Success = false;
                return Rdr;
            }
        }

        public int ExecNonQuery(string sql, bool ignore = false)
        {
            Cmd = Command(sql);
            Success = true;
            try
            {
                if (!ConnOpen) Open();
                Rows = Cmd.ExecuteNonQuery();
                if (Rows == 0) Success = false;
                return Rows;
            }
            catch (Exception e)
            {
                if (!ignore) MDbLog.Write($"MariaDB Class ExecNonQuery Error: {e.Message} for {sql}", "", 0);
                ErrorMessage = $"MariaDB Class NonQuery Error: {e.Message}";
                Success = false;
                return Rows;
            }
        }
    }