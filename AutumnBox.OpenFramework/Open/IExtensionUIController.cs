/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 18:16:09 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 拓展模块运行中界面控制器
    /// </summary>
    public interface IExtensionUIController
    {
        /// <summary>
        /// 进度条
        /// </summary>
        int ProgressValue { set; }
        /// <summary>
        /// 简要状态
        /// </summary>
        string Tip { set; }
        /// <summary>
        /// 增加一行输出
        /// </summary>
        /// <param name="msg"></param>
        void AppendLine(string msg);
        /// <summary>
        /// warpper开始运行了
        /// </summary>
        void OnStart();
        /// <summary>
        /// warpper结束运行了
        /// </summary>
        void OnFinish(int returnCode,bool isForceStopped);
    }
}
