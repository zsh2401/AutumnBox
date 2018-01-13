/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/13 20:16:31
** filename: ZipFileObjHelper.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ZipMaker
{
    static class ExtMethod
    {
        public static void AddDirctories(this ZipFile zip, IEnumerable<string> dirs, string pathInZip)
        {
            dirs.ToList().ForEach((dir) =>
            {
                zip.AddDirectory(dir, $"{pathInZip}\\{new DirectoryInfo(dir).Name}");
            });
        }
        public static string Get(this string[] array, int index)
        {
            try
            {
                return array[index];
            }
            catch
            {
                return null;
            }
        }
    }
}
