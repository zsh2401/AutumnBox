/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 18:20:04 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl.NpdaterImpl
{
    class NpdaterDiffer : IDiffer
    {
        public IEnumerable<IFile> Diff(IEnumerable<IFile> remoteFiles, IEnumerable<FileInfo> localFiles)
        {
            List<IFile> data = new List<IFile>();
            foreach (var f in remoteFiles)
            {
                if (!File.Exists(f.LocalPath))
                {
                    data.Add(f);
                    continue;
                }
                try
                {
                    FileInfo fInfo = new FileInfo(f.LocalPath);
                    var localFileMd5 = fInfo.GetMd5();
                    if (localFileMd5 != f.Md5)
                    {
                        data.Add(f);
                    }
                }
                catch (Exception)
                {
                    data.Add(f);
                }
            }
            return data;
        }
    }
    static class Extension
    {
        public static string GetMd5(this FileInfo file)
        {
            var strResult = "no md5";
            var strHashData = "";
            byte[] arrbytHashValue;
            MD5CryptoServiceProvider oMD5Hasher = new MD5CryptoServiceProvider();
            try
            {
                using (var fs = file.OpenRead())
                {
                    arrbytHashValue = oMD5Hasher.ComputeHash(fs); //计算指定Stream 对象的哈希值
                }
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                strHashData = BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData.ToLower();
            }
            catch (Exception) { }

            return strResult;
        }
    }
}
