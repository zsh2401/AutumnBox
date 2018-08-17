/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 1:04:50 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Extension
{
    partial class AutumnBoxExtension
    {
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => ExtName;
        /// <summary>
        /// UI控制器
        /// </summary>
        internal IExtensionUIController ExtensionUIController
        {
            set
            {
                Ux = new UxController(value);
            }
        }
    }
}
