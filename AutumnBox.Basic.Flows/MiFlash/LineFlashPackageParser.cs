/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 22:27:46
** filename: LineFlashPackageParser.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows.MiFlash
{
    public class LineFlashPackageParser
    {
        public Func<string, string> DescSetter { private get; set; }
        private readonly DirectoryInfo dirInfo;
        public LineFlashPackageParser(string path)
        {
            dirInfo = new DirectoryInfo(path);
        }
        public LineFlashPackageInfo Parse()
        {
            LineFlashPackageInfo result = new LineFlashPackageInfo
            {
                Bats = from file in dirInfo.GetFiles()
                       where file.Extension == ".bat"
                       select new BatInfo(
                             file.FullName,
                             DescSetter?.Invoke(file.Name) ?? file.Name
                       )
            };
            bool haveImageDir = dirInfo.GetDirectories().Select((dir) =>
            {
                return dir.Name == "images";
            }).Count() > 0;
            result.PathIsRight = (!dirInfo.FullName.Contains(" ")) && (!dirInfo.FullName.HasChinese());
            result.IsRight = result.PathIsRight && result.Bats.Count() > 0 && haveImageDir;
            return result;
        }
    }
    static class StrExtension
    {
        public static bool HasChinese(this string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
    }
}
