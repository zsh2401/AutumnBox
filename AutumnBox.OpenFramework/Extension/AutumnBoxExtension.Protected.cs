/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:19:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open;
using System;
using System.Net.Mail;

namespace AutumnBox.OpenFramework.Extension
{

    /// <summary>
    /// AutumnBox 拓展模块抽象类
    /// </summary>
    public abstract partial class AutumnBoxExtension : Context, IAutumnBoxExtension
    {
        /// <summary>
        /// 标签,用于打LOG
        /// </summary>
        public override string Tag => Name;
        /// <summary>
        /// 拓展名,强制要求覆写此属性
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 开发者信息,覆写此属性以自定义
        /// </summary>
        public virtual string Auth
        {
            get
            {
                switch (OpenApi.Gui.CurrentLanguageCode)
                {
                    case "zh-CN":
                    case "zh-SG":
                    case "zh-HK":
                    case "zh-TW":
                        return "佚名";
                    case "en-US":
                    default:
                        return "Anonymous";
                }
            }
        }
        /// <summary>
        /// 联系邮箱,覆写此属性以自定义
        /// </summary>
        public virtual MailAddress ContactMail { get; }
        /// <summary>
        /// 说明,覆写此属性以自定义
        /// </summary>
        public virtual string Description
        {
            get
            {
                switch (OpenApi.Gui.CurrentLanguageCode)
                {
                    case "zh-CN":
                    case "zh-SG":
                    case "zh-HK":
                    case "zh-TW":
                        return "这是一个拓展模块";
                    case "en-US":
                    default:
                        return "This is a extension module";
                }
            }
        }
        /// <summary>
        /// 所需设备状态,覆写此属性以自定义
        /// </summary>
        public virtual DeviceState RequiredDeviceState { get; } = DeviceState.Poweron;
        /// <summary>
        /// 版本号,覆写此属性以自定义版本号
        /// </summary>
        public virtual Version Version { get; } = new Version("1.0.0.0");
        /// <summary>
        /// 目标SDK,覆写此属性以自定义
        /// </summary>
        public virtual int? TargetSdk => null;
        /// <summary>
        /// 最低SDK,覆写此属性以自定义
        /// </summary>
        public virtual int? MinSdk => null;
        /// <summary>
        /// 打LOG
        /// </summary>
        /// <param name="message"></param>
        protected void Log(string message)
        {
            OpenApi.Log.Info(this, message);
        }
        /// <summary>
        /// 在UI线程运行代码
        /// </summary>
        /// <param name="act"></param>
        protected void RunOnUIThread(Action act)
        {
            OpenApi.Gui.RunOnUIThread(this, act);
        }
        /// <summary>
        /// 模块初始化时调用
        /// </summary>
        /// <param name="args"></param>
        public virtual bool InitAndCheck(InitArgs args) { return true; }
        /// <summary>
        /// 当用户点击运行按钮时将调用此方法
        /// </summary>
        /// <param name="args"></param>
        public abstract void OnStartCommand(StartArgs args);
        /// <summary>
        /// 当用户发出停止请求时调用
        /// </summary>
        public virtual bool OnStopCommand(StopArgs args) { return true; }
        /// <summary>
        /// 当OnStartCommand()函数运行完成时调用
        /// </summary>
        public virtual void OnFinished() { }
        /// <summary>
        /// 当模块被管理器释放时调用
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnDestory(DestoryArgs args) { }
        /// <summary>
        /// 当用户请求清理时调用
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnClean(CleanArgs args) { }
    }
}
