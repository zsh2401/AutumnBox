/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 18:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 视觉化秋之盒拓展模块
    /// </summary>
    public abstract class AtmbVisualExtension : AutumnBoxExtension
    {
        public override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            App.RunOnUIThread(() =>
            {
                UIController = AutumnBoxGuiApi.Main.GetUIController();
                UIController.OnStart(args.Wrapper.Info);
                UIController.Closing += OnUIControllerClosing;
            });
        }
        /// <summary>
        /// 主函数
        /// </summary>
        /// <returns></returns>
        public override int Main()
        {
            isRunning = true;
            int retCode = ERR;
            try
            {
                Logger.CDebug("Exeucting VisualMain()");
                retCode = VisualMain();
            }
            catch (Exception ex)
            {
                retCode = ERR;
                Logger.Warn("Fatal exception on VisualMain()", ex);
                WriteLine(App.GetPublicResouce<string>("RunningWindowExceptionOnRunning"));
            }
            if (FinishedTip != null)
            {
                Tip = FinishedTip;
            }
            else
            {
                switch (retCode)
                {
                    case OK:
                        Tip = App.GetPublicResouce<string>("RunningWindowStateFinished");
                        break;
                    case ERR_CANCELED_BY_USER:
                        Tip = App.GetPublicResouce<string>("RunningWindowStateCanceledByUser");
                        break;
                    default:
                        Tip = App.GetPublicResouce<string>("RunningWindowStateError");
                        break;
                }
            }
            UIController.OnFinish();
            isRunning = false;
            return retCode;
        }
        /// <summary>
        /// 当停止时调用
        /// </summary>
        /// <returns></returns>
        public override bool OnStopCommand()
        {
            bool canStop = false;
            try
            {
                canStop = VisualStop();
            }
            catch (Exception ex)
            {
                Logger.Warn("Fatal error on VisualStop()", ex);
                WriteLine(App.GetPublicResouce<string>("RunningWindowExceptionOnStopping"));
            }
            if (canStop)
            {
                Tip = "RunningWindowStateForceStopped";
                WriteLine(App.GetPublicResouce<string>("RunningWindowStopped"));
                App.RunOnUIThread(() =>
                {
                    UIController.OnFinish();
                });
            }
            else
            {
                WriteLine(App.GetPublicResouce<string>("RunningWindowCantStop"));
            }
            isRunning = !canStop;
            return canStop;
        }

        /// <summary>
        /// 完成后的Tip,不设置则默认根据返回码判断是否成功
        /// </summary>
        protected string FinishedTip { get; set; } = null;
        bool isRunning = true;
        internal void OnUIControllerClosing(object sender, UIControllerClosingEventArgs args)
        {
            if (isRunning)
            {
                OnStopCommand();
                args.Cancel = true;
            }
            else
            {
                args.Cancel = false;
            }
        }

        /// <summary>
        /// 可视化主函数
        /// </summary>
        /// <returns></returns>
        protected abstract int VisualMain();
        /// <summary>
        /// 可视化停止
        /// </summary>
        /// <returns></returns>
        protected virtual bool VisualStop()
        {
            return false;
        }

        private IExtensionUIController UIController { get; set; }
        /// <summary>
        /// 写一行数据
        /// </summary>
        /// <param name="message"></param>
        protected void WriteLine(string message)
        {
            UIController.AppendLine(message);
            Logger.Info(message);
        }
        /// <summary>
        /// 进度
        /// </summary>
        protected double Progress
        {
            set
            {
                {
                    App.RunOnUIThread(() =>
                    {
                        UIController.ProgressValue = value;
                    });
                }
            }
        }
        /// <summary>
        /// 设置简要信息
        /// </summary>
        protected string Tip
        {
            set
            {
                App.RunOnUIThread(() =>
                {
                    UIController.Tip = value;
                });
            }
        }
    }
}
