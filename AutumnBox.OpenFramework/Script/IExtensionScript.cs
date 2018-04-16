using System;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 标准拓展脚本接口
    /// </summary>
    public interface IExtensionScript:IExtension,IDisposable
    {
        /// <summary>
        /// 脚本说明
        /// </summary>
        string Desc { get; }
        /// <summary>
        /// 脚本所有者
        /// </summary>
        string Auth { get; }
        /// <summary>
        /// 版本号
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// 联系信息
        /// </summary>
        string ContactInfo { get; }
        /// <summary>
        /// 脚本真实路径
        /// </summary>
        string FilePath { get; }
    }
}
