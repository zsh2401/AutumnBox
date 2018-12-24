using System;
using System.IO;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 内嵌资源包装类
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
        /// <param name="innerResPath"></param>
        /// <returns></returns>
        IEmbeddedFile Get(string innerResPath);
    }
}
