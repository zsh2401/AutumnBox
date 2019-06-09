using System;
using System.IO;
using System.Security.Cryptography;

namespace AutumnBox.OpenFramework.Util
{
    /// <summary>
    ///     Md5工具
    /// </summary>
    public static class Md5
    {
        /// <summary>
        ///     获取文件的Md5值
        /// </summary>
        /// <param name="path">需要校验的文件路径</param>
        /// <returns>小写的Md5值</returns>
        public static string GetMd5(string path)
        {
            var oFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return GetMd5(oFileStream);
        }

        /// <summary>
        ///     获取文件流的Md5值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>小写的Md5值</returns>
        public static string GetMd5(Stream stream)
        {
            try
            {
                var arrayHashValue = new MD5CryptoServiceProvider().ComputeHash(stream);
                var strHashData = BitConverter.ToString(arrayHashValue);
                strHashData = strHashData.Replace("-", "");
                return strHashData.ToLower();
            }
            catch
            {
                return "no md5";
            }
        }

        /// <summary>
        ///     获取Byte[]的Md5值
        /// </summary>
        /// <param name="bytes">Byte[]</param>
        /// <returns>小写的Md5值</returns>
        public static string GetMd5(byte[] bytes)
        {
            try
            {
                var arrayHashValue = new MD5CryptoServiceProvider().ComputeHash(bytes);
                var strHashData = BitConverter.ToString(arrayHashValue);
                strHashData = strHashData.Replace("-", "");
                return strHashData.ToLower();
            }
            catch
            {
                return "no md5";
            }
        }

        /// <summary>
        ///     校验文件Md5值是否正确
        /// </summary>
        /// <param name="path">需要校验的文件路径</param>
        /// <param name="md5">需要校验的文件md5值</param>
        /// <returns></returns>
        public static bool CheckMd5(string path, string md5)
        {
            var fmd5 = GetMd5(path);
            if (fmd5.Equals(md5, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}