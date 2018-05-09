
/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/29 17:20:07 (UTC +8:00)
** desc： ...
*************************************************/
using System.Security.Cryptography;
using System.Text;

namespace AutumnBox.GUI.Util
{
    internal static class Md5
    {
        public static string ToMd5(this string src)
        {
            string str = "";
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(src);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(data);
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("x2");
            }
            return str;
        }
        public static bool IsThisMd5(this string src, string md5)
        {
            return src.ToMd5() == md5;
        }
    }
}
