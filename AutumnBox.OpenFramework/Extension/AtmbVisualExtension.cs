/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 18:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Warpper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 视觉化秋之盒拓展模块
    /// </summary>
    public abstract class AtmbVisualExtension : AutumnBoxExtension
    { 
        private class VisualUxManagerAttribute : ExtMainAsceptAttribute
        {
            /// <summary>
            /// 在Main之前
            /// </summary>
            /// <param name="args"></param>
            public override void Before(BeforeArgs args)
            {
                args.Extension.Logger.Debug("visual before");
                base.Before(args);
                var visualExt = (AtmbVisualExtension)args.Extension;
                args.Extension.App.RunOnUIThread(() =>
                {
                    visualExt.UIController = AutumnBoxGuiApi.Main.GetUIControllerOf(args.ExtWarpper);
                    visualExt.UIController.OnStart();
                    visualExt.MyWarpper = args.ExtWarpper;
                    visualExt.UIController.Closing += visualExt.OnUIControllerClosing;
                });
            }
            /// <summary>
            /// 在Main之后
            /// </summary>
            /// <param name="args"></param>
            public override void After(AfterArgs args)
            {
                base.After(args);
                var visualExt = (AtmbVisualExtension)args.Extension;
                args.Extension.App.RunOnUIThread(() =>
                {
                    visualExt.UIController.Closing -= visualExt.OnUIControllerClosing;
                    visualExt.UIController = null;
                    visualExt.MyWarpper = null;
                });
            }
        }
        /// <summary>
        /// 完成后的Tip,不设置则默认根据返回码判断是否成功
        /// </summary>
        protected string FinishedTip { get; set; } = null;
        /// <summary>
        /// 主函数
        /// </summary>
        /// <returns></returns>
        [VisualUxManager]
        public override int Main()
        {
            isRunning = true;
            int retCode = ERR;
            try
            {
                Logger.Debug("Exeucting VisualMain()");
                retCode = VisualMain();
            }
            catch (Exception ex)
            {
                retCode = ERR;
                Logger.Warn("Fatal exception on VisualMain()", ex);
                WriteLine(App.GetPublicResouce<string>("RunningWindowExceptionOnRunning"));
            }
            if (!isForceStopped)
            {
                App.RunOnUIThread(() =>
                {
                    UIController.OnFinish();
                });
                if (FinishedTip != null)
                {
                    Tip = FinishedTip;
                }
                else {
                    switch (retCode) {
                        case OK:
                            Tip = "RunningWindowStateFinished";
                            break;
                        case ERR_CANCLLED_BY_USER:
                            Tip = "RunningWindowStateCanclledByUser";
                            break;
                        default:
                           Tip=  "RunningWindowStateError";
                            break;

                    }
                }
                Tip = FinishedTip ?? (retCode == 0 ? "RunningWindowStateFinished" : "RunningWindowStateError");
            }
            isRunning = false;
            return retCode;
        }
        bool isForceStopped = false;
        /// <summary>
        /// 当停止时调用
        /// </summary>
        /// <returns></returns>
        public override sealed bool OnStopCommand()
        {
            try
            {
                isForceStopped = VisualStop();
            }
            catch (Exception ex)
            {
                Logger.Warn("Fatal error on VisualStop()", ex);
                WriteLine(App.GetPublicResouce<string>("RunningWindowExceptionOnStopping"));
                isForceStopped = false;
            }
            if (isForceStopped)
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
            isRunning = !isForceStopped;
            return isForceStopped;
        }

        bool isRunning = true;
        internal void OnUIControllerClosing(object sender, UIControllerClosingEventArgs args)
        {
            if (isRunning)
            {
                MyWarpper.Stop();
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
        internal IExtensionWarpper MyWarpper { get; set; }
        internal IExtensionUIController UIController { get; set; }
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
