/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:07:25 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.Impl
{
    /// <summary>
    /// MonoLogueChi编写的简单MD5类
    /// </summary>
    class Md5Impl :IMd5
    {
        /// <summary>
        /// 会返回一个Md5值
        /// </summary>
        /// <param name="_path">需要校验的文件路径</param>
        /// <returns></returns>
        public string GetMd5(string _path)
        {
            var strResult = "no md5";
            var strHashData = "";

            byte[] arrbytHashValue;
            FileStream oFileStream = null;

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                oFileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream); //计算指定Stream 对象的哈希值
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                strHashData = BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData.ToLower();
            }
            catch (Exception) { }

            return strResult;
        }

        /// <summary>
        /// 校验Md5值是否一致
        /// </summary>
        /// <param name="_path">需要校验的文件路径</param>
        /// <param name="_md5">需要校验的文件md5值</param>
        /// <returns></returns>
        public bool CheckMd5(string _path, string _md5)
        {
            var _fmd5 = GetMd5(_path);
            if (_fmd5 == _md5 || _fmd5.ToLower() == _md5) return true;
            return false;
        }
    }
}
