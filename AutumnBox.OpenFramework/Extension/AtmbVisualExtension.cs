/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 18:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using AutumnBox.OpenFramework.Open;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 视觉化秋之盒拓展模块
    /// </summary>
    public abstract class AtmbVisualExtension : AutumnBoxExtension
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            App.RunOnUIThread(() =>
            {
                UIController = CallingBus.BaseApi.GetUIController();
                UIController.OnStart(args.Wrapper.Info);
                UIController.Closing += OnUIControllerClosing;
            });
            Tip = App.GetPublicResouce<string>("RunningWindowStateRunning");
        }
        /// <summary>
        /// 主函数
        /// </summary>
        /// <returns></returns>
        protected sealed override int Main()
        {
            isRunning = true;
            int retCode = ERR;
            try
            {
                Logger.CDebug("Exeucting VisualMain()");
                retCode = VisualMain();
                Logger.CDebug("Executed VisualMain()");
            }
            catch (Exception ex)
            {
                retCode = ERR;
                Logger.Warn("Fatal exception on VisualMain()", ex);
                WriteLine(App.GetPublicResouce<string>("RunningWindowExceptionOnRunning"));
            }
            isRunning = false;
            return retCode;
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="args"></param>
        protected override void OnFinish(ExtensionFinishedArgs args)
        {
            base.OnFinish(args);
            isRunning = false;
            UIController.OnFinish();
            if (args.ExitCode == 0)
            {
                var sound = GetService<ISoundService>(ServicesNames.SOUND);
                sound.OK();
            }
            Tip = GetTipByExitCode(args.ExitCode);
        }
        /// <summary>
        /// 结束执行后，根据返回码获取Tip
        /// </summary>
        /// <param name="exitCode"></param>
        /// <returns></returns>
        protected string GetTipByExitCode(int exitCode)
        {
            switch (exitCode)
            {
                case OK:
                    return App.GetPublicResouce<string>("RunningWindowStateFinished");
                case ERR_CANCELED_BY_USER:
                    return App.GetPublicResouce<string>("RunningWindowStateCanceledByUser");
                default:
                    return App.GetPublicResouce<string>("RunningWindowStateError");
            }
        }
        /// <summary>
        /// 当停止时调用
        /// </summary>
        /// <returns></returns>
        protected sealed override bool OnStopCommand(ExtensionStopArgs args)
        {
            Logger.CDebug("StopCommand()");
            bool canStop = false;
            try
            {
                canStop = VisualStop();
            }
            catch (Exception ex)
            {
                Logger.Warn("Fatal error on VisualStop()", ex);
                App.RunOnUIThread(() =>
                {
                    WriteLine(App.GetPublicResouce<string>("RunningWindowExceptionOnStopping"));
                });
            }
            if (!canStop)
            {
                App.RunOnUIThread(() =>
                {
                    WriteLine(App.GetPublicResouce<string>("RunningWindowCantStop"));
                });
            }
            return canStop;
        }

        ///// <summary>
        ///// 完成后的Tip,不设置则默认根据返回码判断是否成功
        ///// </summary>
        //protected string FinishedTip { get; set; } = null;
        bool isRunning = true;
        internal void OnUIControllerClosing(object sender, UIControllerClosingEventArgs args)
        {
            args.Cancel = isRunning;
            if (isRunning)
            {
                try
                {
                    Task.Run(() =>
                    {
                        Args.CurrentProcess.Kill();
                    });
                }
                catch (Exceptions.ExtensionCantBeStoppedException)
                {
                }
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
            App.RunOnUIThread(() =>
            {
                UIController.AppendLine(message);
            });
            Logger.Info(message);
        }
        /// <summary>
        /// 进度
        /// </summary>
        protected double Progress
        {
            set
            {
                App.RunOnUIThread(() =>
                {
                    UIController.ProgressValue = value;
                });
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
