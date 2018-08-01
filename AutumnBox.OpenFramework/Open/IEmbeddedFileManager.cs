using System;
using System.IO;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 内嵌资源管理器
    /// </summary>
    public interface IEmbeddedFileManager
    {
        /// <summary>
        /// 获取流
        /// </summary>
        /// <param name="innerResPath"></param>
        /// <returns></returns>
        Stream GetStream(string innerResPath);
    }
}
