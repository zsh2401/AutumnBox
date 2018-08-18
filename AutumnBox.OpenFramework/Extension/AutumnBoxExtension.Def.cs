/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 1:02:23 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Extension
{
    partial class AutumnBoxExtension
    {
        /// <summary>
        /// 所有状态
        /// </summary>
        public const DeviceState AllState = (DeviceState)255;
        /// <summary>
        /// 完全成功
        /// </summary>
        public const int OK = 0;
        /// <summary>
        /// 发生错误
        /// </summary>
        public const int ERR = 1;
        /// <summary>
        /// UX交互器
        /// </summary>
        public class UxController
        {
            /// <summary>
            /// 进度条
            /// </summary>
            public int ProgressValue { set => controller.ProgressValue = value; }
            /// <summary>
            /// 简要说明
            /// </summary>
            public string Tip { set => controller.Tip = value; }
            /// <summary>
            /// 写一行
            /// </summary>
            /// <param name="msg"></param>
            public void WriteLine(string msg)
            {
                controller.AppendLine(msg);
            }
            private IExtensionUIController controller;
            internal UxController(IExtensionUIController controller)
            {
                this.controller = controller;
            }
        }
    }
}
