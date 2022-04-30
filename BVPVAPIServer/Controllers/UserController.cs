using Microsoft.AspNetCore.Mvc;
using Libraries;

namespace BVPVAPIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
 
    public static readonly AppInfo AppInf = new AppInfo("NPDES", "WebUI", "DbProduction");
    public static readonly MariaDb Db = new MariaDb(AppInf);

    [HttpGet("{userId}")]
    public UserRec GetUserInfo(string userId) 
    {
        Db.Open();
        var rdr = Db.ExecQueryAsync($"select * from `Admin-Users` where `UserID` = '{userId}' LIMIT 1").Result;
        var userInfo = new UserRec();
        if (rdr == null) return userInfo;
        while (rdr.Read())
        {
            userInfo = new UserRec
            {
                UserId = rdr["UserID"].ToString(),
                Password = rdr["Password"].ToString(),
                Salt = rdr["Salt"].ToString()
            };
        }
        Db.Close();
        
        return userInfo;
    }

    [HttpGet("/GetAll")]
    public List<UserRec> GetAllUsers()
    {
        Db.Open();
        var rdr = Db.ExecQueryAsync($"select * from `Admin-Users`").Result;
        List<UserRec> users = new List<UserRec>();
        if (rdr == null) return users;
        while (rdr.Read())
        {
            var userInfo = new UserRec
            {
                UserId = rdr["UserID"].ToString(),
                Password = rdr["Password"].ToString(),
                Salt = rdr["Salt"].ToString()
            };
            users.Add(userInfo);
        }
        Db.Close();

        return users;
    }
    
    [HttpGet("/GetAllViaWildCard/{wildcard}")]
    public List<UserRec> GetAllUsersWildCard(string wildcard)
    {
        Db.Open();
        var rdr = Db.ExecQueryAsync($"select * from `Admin-Users` where `UserID` like '{wildcard}'").Result;
        List<UserRec> users = new List<UserRec>();
        if (rdr == null) return users;
        while (rdr.Read())
        {
            var userInfo = new UserRec
            {
                UserId = rdr["UserID"].ToString(),
                Password = rdr["Password"].ToString(),
                Salt = rdr["Salt"].ToString()
            };
            users.Add(userInfo);
        }
        Db.Close();

        return users;
    }
    
    [HttpPut("Add/{userRec}")]
    public bool PutUserInfo(UserRec userRec) 
    {
        Db.Open();
        var sql = $"insert into `Admin-Users` values ('{userRec.UserId}', '{userRec.Password}', '{userRec.Salt}');";
        Db.ExecNonQuery(sql);
        var result = Db.Success;
        Db.Close();
        
        return result;
    }
    
    [HttpPut("Update/{userRec}")]
    public bool UpdateUserInfo(UserRec userRec) 
    {
        Db.Open();
        //var sql = $"insert into Users values ('{userRec!.UserId}', '{userRec!.Password}', '{userRec!.Salt}');";
        //Db.ExecNonQuery(sql);
        Db.Close();
        
        return true;
    }
    
    [HttpDelete("Delete/{userId}")]
    public bool DeleteUserInfo(string userId) 
    {
        Db.Open();
        var sql = $"delete from `Admin-Users` where `UserID` = '{userId}';";
        Db.ExecNonQuery(sql);
        var result = Db.Success;
        Db.Close();
        
        return result;
    }
    
}

public class UserRec
{
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
}