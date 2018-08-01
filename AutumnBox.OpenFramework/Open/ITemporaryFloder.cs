using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 临时文件夹管理器
    /// </summary>
   public interface ITemporaryFloder
    {
        /// <summary>
        /// 文件夹信息
        /// </summary>
        DirectoryInfo DirInfo { get; }
        /// <summary>
        /// 路径
        /// </summary>
        string Path { get; }
        /// <summary>
        /// 创建
        /// </summary>
        void Create();
        /// <summary>
        /// 清空
        /// </summary>
        void Clean();
    }
}
