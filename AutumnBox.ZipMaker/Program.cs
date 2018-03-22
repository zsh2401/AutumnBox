using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AutumnBox.ZipMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string suffix;
            if (args.Length > 0)
            {
                suffix = args[1];
            }
            else
            {
                suffix = Input("Please Input suffix(default: Debug)") ?? "Debug";
            }
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(@"..\AutumnBox_Output\AutumnBox\file\AutumnBox.GUI.exe");
            using (ZipFile zip = new ZipFile(Encoding.UTF8))
            {
                zip.AddDirectory(@"..\out");
                zip.Save($"..\\HistoryRelease\\{info.ProductName}-{info.ProductVersion}-{suffix}.zip");
            }
        }
        public static string Input(string desc)
        {
            Console.WriteLine(desc);
            var input = Console.ReadLine();
            return input == String.Empty ? null : input;
        }
        private static IEnumerable<string> GetDirs(string dir)
        {
            var dirs = from _dir in new DirectoryInfo(dir).GetDirectories()
                       select _dir.FullName;
            return dirs;
        }
        private static IEnumerable<string> GetAllFiles(string dir)
        {
            var files = from file in new DirectoryInfo(dir).GetFiles()
                        select file.FullName;
            return files;
        }
    }
}
