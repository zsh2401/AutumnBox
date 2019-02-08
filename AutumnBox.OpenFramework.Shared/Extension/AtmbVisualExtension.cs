/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 18:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using System;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;
using AutumnBox.OpenFramework.Running;
using System.Drawing;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 视觉化秋之盒拓展模块
    /// </summary>
    [Obsolete]
    public abstract class AtmbVisualExtension : AutumnBoxExtension
    {
        /// <summary>
        ///拓展数据的key：完成时是否直接关闭窗体
        /// </summary>
        public const string KEY_CLOSE_FINISHED = "close_on_finished";

        /// <summary>
        /// 关闭或隐藏UI,仅在模块完成后可用
        /// </summary>
        protected void CloseUI()
        {
            App.RunOnUIThread(() =>
            {
                UIController.Close();
            }
            );
        }

        /// <summary>
        /// 开启帮助按钮
        /// </summary>
        /// <param name="action"></param>
        protected void EnableHelpButton(Action action)
        {
            RunOnUIThread(() =>
            {
                UIController.EnableHelp(action);
            });
        }
        /// <summary>
        /// 视图大小
        /// </summary>
        protected Size ViewSize { get => UIController.ViewSize; set => UIController.ViewSize = value; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            App.RunOnUIThread(() =>
            {
                UIController = BaseApi.GetUIController();
                UIController.OnStart(args.Wrapper.Info);
                UIController.Closing += OnUIControllerClosing;
            });
            Tip = App.GetPublicResouce<string>("RunningWindowStateRunning");
        }

        /// <summary>
        /// 主方法数据
        /// </summary>
        protected Dictionary<string, object> Data { get; private set; }

        /// <summary>
        /// 主函数
        /// </summary>
        /// <returns></returns>
        public sealed override int Main(Dictionary<string, object> data)
        {
            Data = data;
            return VisualMain();
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExcetpion(Exception e)
        {
            base.OnExcetpion(e);
            CloseUI();
            UIController = null;
            Data = null;
        }

        /// <summary>
        /// 摧毁
        /// </summary>
        /// <param name="args"></param>
        protected override void OnDestory(object args)
        {
            base.OnDestory(args);
            if (Args.CurrentThread.ExitCode == (int)ExtensionExitCodes.Exception)
            {
                return;
            }
            //Logger.Info(Args.CurrentThread.ExitCode.ToString());
            //Logger.Info(((int)ExtensionExitCodes.Exception).ToString());
            UIController.OnFinish();
            if (Args.CurrentThread.ExitCode == 0)
            {
                var sound = GetService<ISoundService>(ServicesNames.SOUND);
                sound.OK();
            }

            Tip = GetTipByExitCode(Args.CurrentThread.ExitCode);
            try
            {
                if ((bool)Data[KEY_CLOSE_FINISHED] == true)
                {
                    App.RunOnUIThread(() =>
                    {
                        CloseUI();
                    });
                }
            }
            catch (Exception e)
            {
                Logger.Warn("", e);
            }
            UIController = null;
            Data = null;
        }

        /// <summary>
        /// 结束执行后，根据返回码获取Tip
        /// </summary>
        /// <param name="exitCode"></param>
        /// <returns></returns>
        protected virtual string GetTipByExitCode(int exitCode)
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
        protected sealed override bool OnStopCommand(object args)
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

        internal void OnUIControllerClosing(object sender, UIControllerClosingEventArgs args)
        {
            if (Args.CurrentThread.IsRunning)
            {
                try
                {
                    Args.CurrentThread.Kill();
                }
                catch (Exceptions.ExtensionCantBeStoppedException)
                {
                }
                finally
                {
                    args.Cancel = true;
                }
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
