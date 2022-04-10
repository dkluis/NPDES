using Libraries;

namespace BVPVWebServer.Data;

public class UserService
{
    public class User
    {
        public readonly string UserId;
        public readonly List<string> RoleId;
        public readonly Dictionary<string, string> AppsWithFunction;
        public readonly bool ValidUser;
        public readonly bool ValidPassword;
        private readonly AppInfo _appInfo;

        public User(AppInfo appInfo)
        {
            UserId = string.Empty;
            RoleId = new List<string>();
            AppsWithFunction = new Dictionary<string, string>();
            ValidPassword = false;
            ValidUser = false;
            _appInfo = appInfo;
        }

        public User(AppInfo appInfo, string username, string unencryptedPassword)
        {
            ValidPassword = false;
            ValidUser = false;
            _appInfo = appInfo;
            RoleId = new List<string>();
            AppsWithFunction = new Dictionary<string, string>();
            UserId = username;

            using var db = new MariaDb(_appInfo);
            db.Open();
            var rdr = db.ExecQuery($"select * from Users where `UserID` = '{username}';");
            if (rdr is {HasRows: false})
            {
                ValidUser = false;
                UserId = string.Empty;
                db.Close();
                return;
            }

            var storedPassword = string.Empty;
            var storedSalt = string.Empty;
            while (rdr != null && rdr.Read())
            {
                storedPassword = rdr[1].ToString() ?? string.Empty;
                storedSalt = rdr[2].ToString() ?? string.Empty;
            }

            db.Close();
            ValidUser = true;
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(unencryptedPassword, storedSalt);
            if (encryptedPassword != storedPassword)
            {
                return;
            }
            else
            {
                ValidPassword = true;
            }

            db.Open();
            rdr = db.ExecQuery($"select * from UserRoles where `UserID` = '{UserId}';");
            if (rdr is {HasRows: true})
            {
                while (rdr.Read())
                {
                    RoleId.Add(rdr[1].ToString()!);
                }
            }

            db.Close();

            db.Open();
            rdr = db.ExecQuery($"select * from AppsByUser where `User` = '{UserId}';");
            if (rdr is {HasRows: false}) return;

            var lastApp = string.Empty;
            while (rdr != null && rdr.Read())
            {
                if (rdr[4].ToString() is null || rdr[6].ToString() is null) continue;
                if (rdr[4].ToString() == lastApp) continue;
                lastApp = rdr[4].ToString();
                AppsWithFunction.Add(rdr[4].ToString()!, rdr[6].ToString()!);
            }

            db.Close();
        }

        public bool CanUserUseApp(string app)
        {
            // ReSharper disable once NotAccessedVariable
            var result = string.Empty;
            return AppsWithFunction.TryGetValue(app, out result);
        }

        public bool AddUser(string userName, string password)
        {
            var success = false;
            if (ValidUser)
            {
                _appInfo.TxtFile.Write($"Error adding user: {userName}",
                    "UserLib-Add", 0);
                return success;
            }

            var mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            var encryptedPasswrd = BCrypt.Net.BCrypt.HashPassword(password, mySalt);
            using var db = new MariaDb(_appInfo);
            db.Open();
            string sql = $"insert into Users values ('{userName}', '{encryptedPasswrd}', '{mySalt}');";
            db.ExecNonQuery(sql, true);
            success = db.Success;
            db.Close();
            return success;
        }

        public bool AddUserRoles(string userName, string[] userRoles)
        {
            var success = true;
            using var db = new MariaDb(_appInfo);
            db.Open();
            foreach (var role in userRoles)
            {
                string sql = $"insert into UserRoles values ('{userName}', '{role}');";
                db.ExecNonQuery(sql, true);
                if (!db.Success) success = false;
            }

            db.Close();
            return success;
        }
    }
}