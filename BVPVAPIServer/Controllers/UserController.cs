using Microsoft.AspNetCore.Mvc;
using Libraries;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BVPVAPIServer.Controllers;

[ApiController]
[Route("[controller]/{user}")]
public class UserController : ControllerBase
{
 
    public static readonly AppInfo AppInf = new AppInfo("NPDES", "WebUI", "DbProduction");
    public static readonly MariaDb Db = new MariaDb(AppInf);

    [HttpGet(Name = "GetUser")]
    public string GetUserInfo(string user) 
    {
        Db.Open();
        var rdr = Db.ExecQueryAsync($"select * from `Users` where `UserID` = '{user}' LIMIT 1").Result;
        UserRec UserInfo = new UserRec();
        while (rdr.Read())
        {
            UserInfo = new UserRec
            {
                UserId = rdr["UserID"].ToString(),
                Password = rdr["Password"].ToString(),
                Salt = rdr["Salt"].ToString()
            };
        }

        return JsonSerializer.Serialize<UserRec>(UserInfo);
    }
}

[ApiController]
[Route("[controller]")]
public class AllUsers : ControllerBase
{
    public static readonly AppInfo AppInf = new AppInfo("NPDES", "WebUI", "DbProduction");
    public static readonly MariaDb Db = new MariaDb(AppInf);

    [HttpGet(Name = "Users")]
    public List<UserRec> GetAllUsers() 
    {
        Db.Open();
        var rdr = Db.ExecQueryAsync($"select * from `Users`").Result;
        List<UserRec> Users = new List<UserRec>();
        while (rdr.Read())
        {
            var UserInfo = new UserRec
            {
                UserId = rdr["UserID"].ToString(),
                Password = rdr["Password"].ToString(),
                Salt = rdr["Salt"].ToString()
            };
            var userrec = JsonConvert.SerializeObject(UserInfo);
            Users.Add(UserInfo);
        }
        Db.Close();

        return Users;
    }
}

public class UserRec
{
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
}