/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 21:42:35 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Open;
using System;
using System.Text;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 所有脚本的抽象类
    /// </summary>
    public abstract class ScriptBase : Context, IExtensionScript
    {
        /// <summary>
        /// 脚本名
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 说明
        /// </summary>
        public virtual string Desc { get; } = null;
        /// <summary>
        /// 所有者
        /// </summary>
        public virtual string Auth { get; } = null;
        /// <summary>
        /// 版本
        /// </summary>
        public virtual Version Version { get; } = new Version(1, 0, 0, 0);
        /// <summary>
        /// 联系信息
        /// </summary>
        public virtual string ContactInfo { get; } = null;
        /// <summary>
        /// 信息
        /// </summary>
        public virtual string Infomation
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbVersion")}:\t{Version}");
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbAuth")}:\t{Auth}");
                    if (ContactInfo != null)
                    {
                        sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbContactEmail")}:\t{ContactInfo}");
                    }
                    sb.AppendLine(); sb.AppendLine();
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbDescription")}:");
                    sb.AppendLine($"{Desc}");
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "exception on building infomation text..", ex);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public abstract string FilePath { get; }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool Run(ExtensionStartArgs args);
        /// <summary>
        /// 运行检查
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool RunCheck(ExtensionRunCheckArgs args);
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool Stop(ExtensionStopArgs args);
        /// <summary>
        /// 析构
        /// </summary>
        public abstract void Dispose();
    }
}
