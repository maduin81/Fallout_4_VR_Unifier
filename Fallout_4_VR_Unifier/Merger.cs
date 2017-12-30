using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;

namespace Fallout_4_VR_Unifier
{
    public class Merger
    {
        private const string IniName = "Fallout_4_VR_Unifier.ini";
        public bool ShareSaves = true;
        public bool NmmCompatible = true;
        public string TargetPath = @"c:\program files (x86)\steam\steamapps\common\fallout 4 VR\data";
        public string Fo4SourcePath = @".\VR Unifier\FO4";
        public string Fo4VrSourcePath = @".\VR Unifier\FO4VR";
        public string Fo4InstallPath = @"c:\program files (x86)\steam\steamapps\common\fallout 4\";
        public string Fo4VrInstallPath = @"c:\program files (x86)\steam\steamapps\common\fallout 4 vr\";
        public string SteamPath = @"c:\program files (x86)\steam";
        public string Fo4AppId = "377160";
        public string Fo4VrAppId = "611660";
        public string Fo4ExeName = "Fallout4.exe";
        public string Fo4VrExeName = "Fallout4Vr.exe";
        public string Fo4LaunchOptions = "";
        public string Fo4VrLaunchOptions = "";

        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(
        string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        public enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }

        public Merger()
        {
            if (!File.Exists(IniName))
            {
                Console.WriteLine("No INI Found, creating default INI...");
                _WriteDefaultIni();
            }
        }

        public void RunMerge(string mode = "")
        {
            var merger = new Merger();
            var exePath = string.Empty;
            var options = string.Empty;

            try
            {
                Console.WriteLine("Reading INI...");
                merger.ReadIni();

                Console.WriteLine("INI Loaded, creating Forward SymLinks...");


                var args = Environment.GetCommandLineArgs().ToList();

                if (args.Any() || !string.IsNullOrWhiteSpace(mode))
                {
                    var arg = args.FirstOrDefault(s => s.ToLower().Contains("/m="))?.Split('=')[1];
                    if ((arg != null && arg.ToLower() == "vr") || mode == "vr")
                    {
                        merger.CreateForwardSymLinks(merger.Fo4VrSourcePath);
                        exePath = Path.Combine(merger.Fo4VrInstallPath, merger.Fo4VrExeName);
                        options = merger.Fo4VrLaunchOptions;
                    }
                    else if ((arg != null && arg.ToLower() == "flat") || mode == "flat")
                    {
                        merger.CreateForwardSymLinks(merger.Fo4SourcePath);
                        exePath = Path.Combine(merger.Fo4InstallPath, merger.Fo4ExeName);
                        options = merger.Fo4LaunchOptions;
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameter, exitting... ");
                    }
                }

                if (merger.NmmCompatible)
                {
                    Console.WriteLine("NMM Compatibility, creating Backward SymLinks and Launcher Batch file...");
                    merger.CreateBackwardSymLinks();
                }

                if (merger.ShareSaves)
                {
                    Console.WriteLine("Sharing saves, redirecting save folder...");
                    merger.RedirectSaves();
                }

                Console.WriteLine("Merging complete, application will now launch game and exit.");

                if (!string.IsNullOrWhiteSpace(exePath))
                {
                    System.Diagnostics.Process.Start(exePath, options);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }            
        }

        public void ReadIni()
        {
            IniData data;

            if (!File.Exists(IniName))
            {
                Console.WriteLine("No INI Found, creating default INI...");
                data = _WriteDefaultIni();
            }
            else
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile(IniName);
            }

            ShareSaves = bool.Parse(data["General"]["ShareSaves"]);
            NmmCompatible = bool.Parse(data["General"]["NMMCompatible"]);
            TargetPath = data["General"]["DataPath"];
            Fo4InstallPath = data["General"]["Fallout4Path"];
            Fo4VrInstallPath = data["General"]["Fallout4VRPath"];
            Fo4ExeName = data["General"]["Fallout4Exe"];
            Fo4VrExeName = data["General"]["Fallout4VRExe"];
            Fo4LaunchOptions = data["General"]["Fallout4LaunchOptions"];
            Fo4VrLaunchOptions = data["General"]["Fallout4VRLaunchOptions"];
            SteamPath = data["General"]["SteamPath"];
            Fo4AppId = data["General"]["Fallout4AppId"];
            Fo4VrAppId = data["General"]["Fallout4VRAppId"];
        }

        private IniData _WriteDefaultIni()
        {
            var parser = new FileIniDataParser();
            var data = new IniData();

            data.Sections.AddSection("General");
            var sectionKeys = data.Sections.GetSectionData("General").Keys;

            var key = new KeyData("ShareSaves")
            { Value = ShareSaves.ToString() };
            sectionKeys.AddKey(key);

            key = new KeyData("NMMCompatible") { Value = NmmCompatible.ToString() };
            sectionKeys.AddKey(key);

            var regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Bethesda Softworks\\Fallout4");

            var path = regKey?.GetValue("InstalledPath");
            if (path != null)
            {
                Fo4InstallPath = path.ToString();
            }

            Registry.LocalMachine.Close();
            regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Bethesda Softworks\\Fallout 4 VR");

            path = regKey?.GetValue("InstalledPath");
            if (path != null)
            {
                Fo4VrInstallPath = path.ToString();
                TargetPath = Path.Combine(path.ToString(), "Data");
            }

            Registry.LocalMachine.Close();

            key = new KeyData("DataPath") { Value = TargetPath };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4Path") { Value = Fo4InstallPath };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4VRPath") { Value = Fo4VrInstallPath };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4Exe") { Value = Fo4ExeName };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4LaunchOptions") { Value = Fo4LaunchOptions };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4VRExe") { Value = Fo4VrExeName };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4VRLaunchOptions") { Value = Fo4VrLaunchOptions };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4AppId") { Value = Fo4AppId };
            sectionKeys.AddKey(key);
            key = new KeyData("Fallout4VrAppId") { Value = Fo4VrAppId };
            sectionKeys.AddKey(key);
            key = new KeyData("SteamPath") { Value = SteamPath };
            sectionKeys.AddKey(key);

            parser.WriteFile(IniName, data);

            data = parser.ReadFile(IniName);
            Console.WriteLine("Default INI Created, proceeding to next step...");
            return data;
        }

        public void CreateForwardSymLinks(string sourcePath)
        {
            var directories = Directory.GetDirectories(Path.Combine(TargetPath, sourcePath));

            foreach (var dir in directories)
            {
                var sourceDirName = Path.GetFileName(dir);
                if (sourceDirName != null)
                {
                    var targetDirName = Path.Combine(TargetPath, sourceDirName);

                    if (Directory.Exists(targetDirName))
                    {
                        DeleteSymbolicLink(targetDirName, SymbolicLink.Directory);
                    }

                    Console.WriteLine($"Creating New Symlink: {dir} ==> {targetDirName}");
                    CreateSymbolicLink(targetDirName, dir, SymbolicLink.Directory);
                }
            }

            var files = Directory.GetFiles(Path.Combine(TargetPath, sourcePath)).ToList();

            foreach (var file in files)
            {
                var sourceFileName = Path.GetFileName(file);
                if (sourceFileName != null)
                {
                    var targetFileName = Path.Combine(TargetPath, sourceFileName);

                    if (File.Exists(targetFileName) && File.GetAttributes(targetFileName).HasFlag(FileAttributes.ReparsePoint))
                    {
                        DeleteSymbolicLink(targetFileName);
                    }

                    if (file.ToLower().Contains(".esm") || file.ToLower().Contains(".esp") ||
                        file.ToLower().Contains(".esl"))
                    {
                        Console.WriteLine($"Copying plugin: {file} ==> {targetFileName}");
                        File.Copy(file, targetFileName, true);
                    }
                    else
                    {
                        Console.WriteLine($"Creating New Symlink: {file} ==> {targetFileName}");
                        CreateSymbolicLink(targetFileName, file, SymbolicLink.File);
                    }
                }
            }

            var fo4DataPath = Path.Combine(Fo4InstallPath, "Data");
            var fo4VrDataPath = Path.Combine(Fo4VrInstallPath, "Data");

            if (Directory.Exists(fo4DataPath) && !File.GetAttributes(fo4DataPath).HasFlag(FileAttributes.ReparsePoint))
            {
                var newTargetDirName = $"{fo4DataPath}_{DateTime.Now:yyyyMMddHHmmss}";
                Console.WriteLine($"Save folder detected, renaming to {newTargetDirName}...");
                Directory.Move(fo4DataPath, newTargetDirName);

            }
            else if (Directory.Exists(fo4DataPath) && File.GetAttributes(fo4DataPath).HasFlag(FileAttributes.ReparsePoint))
            {
                DeleteSymbolicLink(fo4DataPath, SymbolicLink.Directory);
            }

            if (Directory.Exists(fo4VrDataPath))
            {
                Console.WriteLine($"Creating New Symlink: {fo4VrDataPath} ==> {fo4DataPath}");
                CreateSymbolicLink(fo4DataPath, fo4VrDataPath, SymbolicLink.Directory);
            }
        }

        public void CreateBackwardSymLinks()
        {
            var sourceDirName =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Fallout4");
            var targetDirName =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Fallout4VR");

            if (Directory.Exists(targetDirName) && !File.GetAttributes(targetDirName).HasFlag(FileAttributes.ReparsePoint))
            {
                var newTargetDirName = $"{targetDirName}_{DateTime.Now:yyyyMMddHHmmss}";
                Console.WriteLine($"LocalAppData folder detected, renaming to {newTargetDirName}...");
                Directory.Move(targetDirName, newTargetDirName);

            }
            else if (Directory.Exists(targetDirName) && File.GetAttributes(targetDirName).HasFlag(FileAttributes.ReparsePoint))
            {
                DeleteSymbolicLink(targetDirName, SymbolicLink.Directory);
            }

            if (Directory.Exists(sourceDirName))
            {
                Console.WriteLine($"Creating New Symlink: {sourceDirName} ==> {targetDirName}");
                CreateSymbolicLink(targetDirName, sourceDirName, SymbolicLink.Directory);
            }
        }

        public void RedirectSaves()
        {
            var gamesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games");
            const string fo4 = "Fallout4";
            const string fo4Vr = "Fallout4VR";
            const string saves = @"Saves\";
            var savesBak = $"Saves_{DateTime.Now:yyyyMMddHHmmss}";


            if (Directory.Exists(Path.Combine(gamesFolder, fo4Vr, saves)) && !File.GetAttributes(Path.Combine(gamesFolder, fo4Vr, saves)).HasFlag(FileAttributes.ReparsePoint))
            {
                Console.WriteLine($"Save folder detected, renaming to {savesBak}...");
                Directory.Move(Path.Combine(gamesFolder, fo4Vr, saves), Path.Combine(gamesFolder, fo4Vr, savesBak));
            }
            else if (Directory.Exists(Path.Combine(gamesFolder, fo4Vr, saves)) && File.GetAttributes(Path.Combine(gamesFolder, fo4Vr, saves)).HasFlag(FileAttributes.ReparsePoint))
            {
                DeleteSymbolicLink(Path.Combine(gamesFolder, fo4Vr, saves), SymbolicLink.Directory);
            }

            if (Directory.Exists(Path.Combine(gamesFolder, fo4, saves)))
            {
                Console.WriteLine($"Creating New Symlink: {Path.Combine(gamesFolder, fo4, saves)} ==> {Path.Combine(gamesFolder, fo4Vr, saves)}");
                CreateSymbolicLink(Path.Combine(gamesFolder, fo4Vr, saves),
                    Path.Combine(gamesFolder, fo4, saves), SymbolicLink.Directory);
            }
        }

        public void DeleteSymbolicLink(string file, SymbolicLink type = SymbolicLink.File)
        {
            if (File.GetAttributes(file).HasFlag(FileAttributes.ReparsePoint))
            {
                Console.WriteLine("Existing SymLink detected, deleting...");
                if (type == SymbolicLink.Directory)
                {
                    Directory.Delete(file);
                }
                else
                {
                    File.Delete(file);
                }
            }
        }

        public void Unmerge()
        {
            var gamesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games");
            
            const string fo4Vr = "Fallout4VR";
            const string saves = @"Saves\";

            if (Directory.Exists(Path.Combine(gamesFolder, fo4Vr, saves)) && File.GetAttributes(Path.Combine(gamesFolder, fo4Vr, saves)).HasFlag(FileAttributes.ReparsePoint))
            {
                DeleteSymbolicLink(Path.Combine(gamesFolder, fo4Vr, saves), SymbolicLink.Directory);
            }

            var lastSaveFolder = Directory.GetDirectories(Path.Combine(gamesFolder, fo4Vr))
                .Where(s => s.Contains("Saves_"))
                .OrderByDescending(s => s).FirstOrDefault()?.ToString();

            if (!string.IsNullOrWhiteSpace(lastSaveFolder) && !Directory.Exists(Path.Combine(gamesFolder, fo4Vr, saves)))
            {
                Directory.Move(lastSaveFolder, Path.Combine(gamesFolder, fo4Vr, saves));
            }

            var targetDirName =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Fallout4VR");

            if (Directory.Exists(targetDirName) && File.GetAttributes(targetDirName).HasFlag(FileAttributes.ReparsePoint))
            {
                DeleteSymbolicLink(targetDirName, SymbolicLink.Directory);
            }

            var lastAppdataFolder = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))
                .Where(s => s.Contains("Fallout4VR_"))
                .OrderByDescending(s => s).FirstOrDefault()?.ToString();

            if (!string.IsNullOrWhiteSpace(lastAppdataFolder) && !Directory.Exists(targetDirName))
            {
                Directory.Move(lastAppdataFolder, targetDirName);
            }

            var fo4DataPath = Path.Combine(Fo4InstallPath, "Data");

            if (Directory.Exists(fo4DataPath) && File.GetAttributes(fo4DataPath).HasFlag(FileAttributes.ReparsePoint))
            {
                DeleteSymbolicLink(fo4DataPath, SymbolicLink.Directory);
            }

            var lastDataFolder = Directory.GetDirectories(Fo4InstallPath)
                .Where(s => s.Contains("Data_"))
                .OrderByDescending(s => s).FirstOrDefault()?.ToString();

            if (!string.IsNullOrWhiteSpace(lastDataFolder) && !Directory.Exists(fo4DataPath))
            {
                Directory.Move(lastDataFolder, fo4DataPath);
            }

            CleanupDataFolder(Path.Combine(TargetPath, Fo4VrSourcePath));
            CleanupDataFolder(Path.Combine(TargetPath, Fo4SourcePath));

            ReturnFiles();
        }

        public void CleanupDataFolder(string sourcePath)
        {
            var directories = Directory.GetDirectories(Path.Combine(TargetPath, sourcePath));

            foreach (var dir in directories)
            {
                var sourceDirName = Path.GetFileName(dir);
                if (sourceDirName != null)
                {
                    var targetDirName = Path.Combine(TargetPath, sourceDirName);

                    if (Directory.Exists(targetDirName))
                    {
                        DeleteSymbolicLink(targetDirName, SymbolicLink.Directory);
                    }
                }
            }

            var files = Directory.GetFiles(Path.Combine(TargetPath, sourcePath)).ToList();

            foreach (var file in files)
            {
                var sourceFileName = Path.GetFileName(file);
                if (sourceFileName != null)
                {
                    var targetFileName = Path.Combine(TargetPath, sourceFileName);

                    if (File.Exists(targetFileName) && File.GetAttributes(targetFileName).HasFlag(FileAttributes.ReparsePoint))
                    {
                        DeleteSymbolicLink(targetFileName);
                    }                    
                }
            }            
        }

        public void ReturnFiles()
        {            
            foreach (string dirPath in Directory.GetDirectories(Path.Combine(TargetPath, Fo4VrSourcePath), "*",
                SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(Path.Combine(TargetPath, Fo4VrSourcePath), TargetPath));
            }
            
            foreach (string newPath in Directory.GetFiles(Path.Combine(TargetPath, Fo4VrSourcePath), "*.*",
                SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(Path.Combine(TargetPath, Fo4VrSourcePath), TargetPath), true);
            }
        }
    }
}
