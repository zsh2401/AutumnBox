/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 22:27:46
** filename: LineFlashPackageParser.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Flows.MiFlash
{
    /// <summary>
    /// 线刷包文件夹解析器
    /// </summary>
    public class LineFlashPackageParser
    {
        /// <summary>
        /// 解释解析器
        /// </summary>
        public Func<string, string> DescSetter { private get; set; }
        private readonly DirectoryInfo dirInfo;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="path"></param>
        public LineFlashPackageParser(string path)
        {
            dirInfo = new DirectoryInfo(path);
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns></returns>
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
