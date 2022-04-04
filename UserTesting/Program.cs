using Libraries;

var applicationInfo = new AppInfo("NPDES", "User-Test", "DbProduction");
var logFile = applicationInfo.TxtFile;
logFile.Start();

logFile.Write("###");
TestAddUser("Dick", "password", applicationInfo);
TestAddUserRoles("Dick", new[] {"SuperAdmin", "Reporter"}, applicationInfo);
logFile.Write($"Created User is Dick: Test access below");
TestUser("dick", "password");
var result = TestApp("dick", "password", "wrong App");
logFile.Write($"User dick can use application: 'wrong App' is {result}");
result = TestApp("dick", "password", "Testing");
logFile.Write($"User dick can use application: 'Testing' is {result}");


logFile.Write("###");
logFile.Write($"Non Existing User Test >> User is dicky");
TestUser("dicky", "password");
TestAddUserRoles("Dicky", new[] {"GroupAdmin"}, applicationInfo);

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
TestAddUser("Dicky", "password", applicationInfo);
logFile.Write("###");
TestAddUser("Dicky", "password", applicationInfo);
logFile.Write("###");
TestAddUser("Dicky", "password", applicationInfo);
logFile.Write("###");
TestAddUser("WaterRep", "password", applicationInfo);
TestAddUserRoles("WaterRep", new[] {"Reporter"}, applicationInfo);


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
    foreach (var app in userInfo.AppsWithFunction)
    {
        logFile.Write($"App: {app.Key}, function: {app.Value}");
    }
}

bool TestApp(string username, string passwrd, string app)
{
    var userInfo = new User(applicationInfo!, username, passwrd);
    return userInfo.CanUserUseApp(app);
}

void TestAddUser(string username, string passwrd, AppInfo ap)
{
    var user = new User(ap);
    ap.TxtFile.Write($"Adding user {username} with result: {user.AddUser(username, passwrd)}");
}

void TestAddUserRoles(string username, string[] roles, AppInfo ap)
{
    var user = new User(ap);
    user.AddUserRoles(username, roles);
}