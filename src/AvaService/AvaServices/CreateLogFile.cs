namespace Sample.AvaServices;

public static class CreateLogFile
{
    public static void AddToTxtFile(string content)
    {
        string path = Config.logPath;
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine(content);
            }
        }
        else if (File.Exists(path))
        {
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(content);
            }
        }
    }

    public static void RemoveTxtFile()
    {
        string path = Config.logPath;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
