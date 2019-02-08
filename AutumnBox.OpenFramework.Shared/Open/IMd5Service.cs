using System;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// MD5 API
    /// </summary>
    [Obsolete("Use Md5 static class to instead")]
    public interface IMd5Service
    {
        /// <summary>
        /// 获取某个文件的MD5
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        string GetMd5(string _path);
        /// <summary>
        /// 检查某个文件的MD5是否符合特定MD5
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_md5"></param>
        /// <returns></returns>
        bool CheckMd5(string _path, string _md5);
    }
}
