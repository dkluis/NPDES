using BCrypt.Net;

namespace Libraries;

public class User
{
    public readonly string userName;
    public readonly string firstName;
    public readonly string lastName;
    private readonly string _encryptedPassword;
    public readonly string[] userRoles;
    public readonly string[] userApps;
    public readonly bool superAdmin;

    public User(string username, string unencryptedPassword)
    {
        userName = username;
        firstName = "Dick";
        lastName = "Kluis";
        // encrypt the password and validate with the DB encrypted password
        _encryptedPassword = "AAA123";
        userRoles = new string[] {"Admin"};
        userApps = new string[] {"App1", "App2"};
        superAdmin = true;
    }
}