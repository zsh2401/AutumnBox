using System;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// MD5 API
    /// </summary>
    public interface IMd5
    {
        /// <summary>
        /// 获取某个文件的MD5
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetMd5(string path);

        /// <summary>
        /// 检查某个文件的MD5是否符合特定MD5
        /// </summary>
        /// <param name="path"></param>
        /// <param name="md5"></param>
        /// <returns></returns>
        bool CheckMd5(string path, string md5);
    }
}
