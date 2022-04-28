using Libraries;

namespace BVPVWebServer.Services.General;

public class MarkdownService
{
    public static List<string> GetMarkDownFile(AppInfo appInfo, string fileName)
    {
        var fileContent = new List<string>();
        var helpFilePath = BaseConfig.HelpFilesPath + "/" + fileName;
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