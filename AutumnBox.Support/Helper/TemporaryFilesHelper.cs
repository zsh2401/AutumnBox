/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/19 20:10:49 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.Helper
{
    public static class TemporaryFilesHelper
    {
        public const string TemporaryFloderName = "tmp";
        public static FileStream GetTempFileStream(string fileName)
        {
            InitFloder();
            return new FileStream($"{TemporaryFloderName}\\{fileName}",FileMode.OpenOrCreate,FileAccess.ReadWrite);
        }
        public static void InitFloder()
        {
            if (!Directory.Exists(TemporaryFloderName))
            {
                Directory.CreateDirectory(TemporaryFloderName);
            }
        }
    }
}
