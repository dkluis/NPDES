
using System.Runtime.InteropServices;
using Libraries;

var applicationInfo = new AppInfo("NPDES", "User-Test", "DbProduction");
var logFile = applicationInfo.TxtFile;


logFile.Start();

logFile.Write($"Existing User Test >> System User is {applicationInfo.SystemUserName}");
TestUser("dick", "password");

logFile.Write("###");
logFile.Write($"Non Existing User Test >> System User is {applicationInfo.SystemUserName}");
TestUser("dicky", "password");

logFile.Write("###");
logFile.Write($"Wrong Password User Test >> System User is {applicationInfo.SystemUserName}");
TestUser("dick", "Password");

logFile.Write("###");
logFile.Write($"Testing if app can be used: >> System User is {applicationInfo.SystemUserName}");
var userName = "Dick";
var password = "password";
var application = "Testing12";
logFile.Write($"Requested app {application} can be used is: " +
              $"{TestApp(userName, password, application)}");

logFile.Stop();

void TestUser(string username, string password)
{
    
    var userInfo = new User(applicationInfo, username, password);
    logFile.Write($"Is User valid: {userInfo.ValidUser} >> Is User Password Valid: {userInfo.ValidPassword}");
    logFile.Write($"Is User: {userInfo.UserId}, Role: {userInfo.RoleId} ");
    logFile.Write($"Allowed Apps are: ");
    if (!userInfo.ValidPassword) return;
    foreach (var app in userInfo.AppAndFunctionIds)
    {
        logFile.Write($"App: {app.Key}, function: {app.Value}");
    }


}

bool TestApp(string username, string password, string app)
{
    var userInfo = new User(applicationInfo, username, password);
    return userInfo.CanUserUseApp(app);
}