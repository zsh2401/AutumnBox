using System;
using System.IO;
using System.Reflection;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 内嵌资源包装类
    /// 无法直接通过Lake获取
    /// </summary>
    public interface IEmbeddedFile
    {
        /// <summary>
        /// 获取流
        /// </summary>
        /// <returns></returns>
        Stream GetStream();

        /// <summary>
        /// 提取到指定文件
        /// </summary>
        /// <param name="targetFile"></param>
        void ExtractTo(FileInfo targetFile);

        /// <summary>
        /// 复制内容到指定流
        /// </summary>
        /// <param name="stream"></param>
        void CopyTo(Stream stream);

        /// <summary>
        /// 写入到流
        /// </summary>
        /// <param name="fs"></param>
        void WriteTo(FileStream fs);
    }
    /// <summary>
    /// 内嵌资源管理器
    /// </summary>
    public interface IEmbeddedFileManager
    {
        /// <summary>
        /// 获取内嵌资源的抽象对象
        /// </summary>
        /// <param name="assembly">内嵌资源所在程序集</param>
        /// <param name="innerResPath"></param>
        /// <exception cref="Exception">无法获取资源</exception>
        /// <returns></returns>
        IEmbeddedFile Get(Assembly assembly, string innerResPath);
    }
}
