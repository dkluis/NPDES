using Libraries;
using Microsoft.AspNetCore.Mvc;

namespace BVPVWebServer.Services.General;

public class MarkdownService
{
    public static List<string> GetMarkDownFile(string fileName)
    {
        var fileContent = new List<string>(512);
        var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var helpFilePath = wwwRootPath + "/HelpFiles/" + fileName;
        Console.WriteLine(helpFilePath);
        if (!File.Exists(helpFilePath))
        {
            fileContent.Add("No Help exists (yet)");
        }
        else
        {
            var readLines = File.ReadAllLines(helpFilePath);
            foreach (var line in readLines)
            {
                fileContent.Add(line);
            }
        }
        return fileContent;
    }
}
