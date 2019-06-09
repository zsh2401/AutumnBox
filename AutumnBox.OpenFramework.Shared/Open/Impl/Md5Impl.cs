using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Impl
{
    [Obsolete("Use Md5 static class to instead")]
    class Md5Impl : IMd5Service
    {
        /// <summary>
        /// 会返回一个Md5值
        /// </summary>
        /// <param name="path">需要校验的文件路径</param>
        /// <returns></returns>
        public string GetMd5(string path)
        {
            var strResult = "no md5";

            System.Security.Cryptography.MD5CryptoServiceProvider oMd5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                var oFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var arrbytHashValue = oMd5Hasher.ComputeHash(oFileStream);
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                var strHashData = BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData.ToLower();
            }
            catch
            {
                // ignored
            }

            return strResult;
        }

        /// <summary>
        /// 校验Md5值是否一致
        /// </summary>
        /// <param name="path">需要校验的文件路径</param>
        /// <param name="md5">需要校验的文件md5值</param>
        /// <returns></returns>
        public bool CheckMd5(string path, string md5)
        {
            var fmd5 = GetMd5(path);
            if (fmd5.Equals(md5, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}
