using Libraries;

var applicationInfo = new AppInfo("NPDES", "User-Test", "DbProduction");
var logFile = applicationInfo.TxtFile;


logFile.Start();

logFile.Write($"Existing User Test >> User is dick");
TestUser("dick", "password");

logFile.Write("###");
logFile.Write($"Non Existing User Test >> User is dicky");
TestUser("dicky", "password");

logFile.Write("###");
logFile.Write($"Wrong Password User Test >> User is dick");
TestUser("dick", "Password");

logFile.Write("###");
logFile.Write($"Testing if app can be used: >> User is Dick");
var userName = "Dick";
var password = "password";
var application = "Testing";
logFile.Write($"Requested app {application} can be used is: " +
              $"{TestApp(userName, password, application)}");
application = "Testing12";
logFile.Write($"Requested app {application} can be used is: " +
              $"{TestApp(userName, password, application)}");
logFile.Write("###");
TestAddUser("Dicky", "GroupAdmin", applicationInfo);
logFile.Write("###");
TestAddUser("Dicky", "GroupAdmin1", applicationInfo);
logFile.Write("###");
TestAddUser("Dicky", "GroupAdmin", applicationInfo);
logFile.Write("###");
TestAddUser("WaterRep", "Reporter", applicationInfo);


logFile.Stop();

Environment.Exit(0);

//#######################################################################

void TestUser(string username, string passwrd)
{
    var userInfo = new User(applicationInfo, username, passwrd);
    logFile.Write($"Is User valid: {userInfo.ValidUser} >> Is User Password Valid: {userInfo.ValidPassword}");
    foreach (var role in userInfo.RoleId)
    {
        logFile.Write($"RoleId: {role}");
    }
    logFile.Write($"Allowed Apps are: ");
    if (!userInfo.ValidPassword) return;
    foreach (var app in userInfo.AppAndFunctionIds)
    {
        logFile.Write($"App: {app.Key}, function: {app.Value}");
    }
}

bool TestApp(string username, string passwrd, string app)
{
    var userInfo = new User(applicationInfo, username, passwrd);
    return userInfo.CanUserUseApp(app);
}

void TestAddUser(string username, string roleid, AppInfo appinfo)
{
    var user = new User(appinfo);
    var errors = user.ValidateAddUser(username, roleid);
    if (errors.Count > 0)
    {
        appinfo.TxtFile.Write($"Errors found validating the User {username} with roleID {roleid}");
        foreach (var error in errors)
        {
            appinfo.TxtFile.Write(error, "", 0);
        }
    }
    else
    {
        appinfo.TxtFile.Write($"Adding user {username} with result: {user.AddUser(username, roleid)}");
    }
}