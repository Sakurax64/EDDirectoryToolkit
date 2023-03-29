using System.Diagnostics;

namespace EDDirectoryToolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Elite Dangerous Directory Toolkit - EDDT";
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Elite Dangerous Directory Toolkit\r\n");
            Console.WriteLine("Choose an option:\r\n");
            Console.WriteLine("1: Open bindings directory");
            Console.WriteLine("2: Open graphics directory");
            Console.WriteLine("3: Open journals directory");
            Console.WriteLine("4: Open EDMC plugins directory");
            Console.WriteLine("5: Open EDMC logs directory");
            Console.WriteLine("6: Open screenshots directory");
            Console.WriteLine("7: Backup keybinds to desktop");
            Console.WriteLine("8: Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    openFolder($"{Environment.GetEnvironmentVariable("LocalAppData")}\\Frontier Developments\\Elite Dangerous\\Options\\Bindings");
                    return true;
                case "2":
                    openFolder($"{Environment.GetEnvironmentVariable("LocalAppData")}\\Frontier Developments\\Elite Dangerous\\Options\\Graphics");
                    return true;
                case "3":
                    openFolder($"{Environment.GetEnvironmentVariable("userprofile")}\\Saved Games\\Frontier Developments\\Elite Dangerous");
                    return true;
                case "4":
                    openFolder($"{Environment.GetEnvironmentVariable("LocalAppData")}\\EDMarketConnector\\plugins");
                    return true;
                case "5":
                    openFolder($"{Environment.GetEnvironmentVariable("TEMP")}\\EDMarketConnector");
                    return true;
                case "6":
                    openFolder($"{Environment.GetEnvironmentVariable("userprofile")}\\Pictures\\Frontier Developments\\Elite Dangerous");
                    return true;
                case "7":
                    copyFolder($"{Environment.GetEnvironmentVariable("LocalAppData")}\\Frontier Developments\\Elite Dangerous\\Options\\Bindings", $"{Environment.GetEnvironmentVariable("userprofile")}\\Desktop\\Bindings", true);
                    return true;
                case "8":
                    return false;
                default:
                    return true;
            }
        }
        private static void openFolder(string path)
        {
            try
            {
                Process.Start("explorer.exe", path);
            }
            catch (Exception e)
            {
            }
        }

        private static void copyFolder(string sourceDir, string destinationDir, bool recursive)
        {
            try
            {
                var dir = new DirectoryInfo(sourceDir);

                DirectoryInfo[] dirs = dir.GetDirectories();

                Directory.CreateDirectory(destinationDir);

                foreach (FileInfo file in dir.GetFiles())
                {
                    string targetFilePath = Path.Combine(destinationDir, file.Name);
                    file.CopyTo(targetFilePath);
                }

                if (recursive)
                {
                    foreach (DirectoryInfo subDir in dirs)
                    {
                        string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                        copyFolder(subDir.FullName, newDestinationDir, true);
                    }
                }
            }
            catch (Exception e)
            {
                Directory.Delete(destinationDir, true);
                copyFolder(sourceDir, destinationDir, true);
            }
            
        }
    }
}