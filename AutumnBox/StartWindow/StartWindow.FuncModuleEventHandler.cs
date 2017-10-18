/* =============================================================================*\
*
* Filename: Main.FuncFinishedHandler.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:31:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox
{
    using System;
    using System.Windows;
    using AutumnBox.Util;
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Basic.Function;
    using AutumnBox.Helper;
    using AutumnBox.Windows;
    using AutumnBox.Basic.Function.Modules;

    /// <summary>
    /// 各种界面事件
    /// </summary>
    public partial class StartWindow
    {
        string mweTag = "MainWindowEvent";
        /// <summary>
        /// 通用事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FuncFinish(object sender, FinishEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                UIHelper.CloseRateBox();
                if (sender is FileSender)
                {
                    PushFinish(sender, e);
                }
                else if (sender is BreventServiceActivator)
                {
                    ActivatedBrvent(sender, e);
                }
                else if (sender is ActivityLauncher)
                {
                    //TODO
                }
                else if (sender is CustomRecoveryFlasher)
                {
                    FlashCustomRecFinish(sender, e);
                    //TODO
                }
                else if (sender is RebootOperator)
                {
                    //TODO
                }
                else if (sender is XiaomiSystemUnlocker)
                {
                    UnlockMiSystemFinish(sender, e);
                }
                else if (sender is XiaomiBootloaderRelocker)
                {
                    RelockMiFinish(sender, e);
                }
            });
        }

        /// <summary>
        /// 黑域启动完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivatedBrvent(object sender, FinishEventArgs e)
        {
            App.LogD(TAG, e.OutputData.Error.ToString());
            App.LogD(TAG, e.OutputData.Out.ToString());
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
            if (e.Result.Level != ResultLevel.Successful)
            {
                e.Result.Message = App.Current.Resources["errormsgBrventActivtedUnsuccess"].ToString();
                e.Result.Advise = App.Current.Resources["advsBrventActivtedUnsuccess"].ToString();
                this.Dispatcher.Invoke(() =>
                {
                    ModuleResultWindow.FastShow(e.Result);
                });
            }
        }

        /// <summary>
        /// 解锁小米系统完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void UnlockMiSystemFinish(object sender, FinishEventArgs e)
        {
            App.LogD(mweTag, "UnlockMiSystemFinish Event ");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
        }

        /// <summary>
        /// 重新给小米手机上锁完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void RelockMiFinish(object sender, FinishEventArgs e)
        {
            App.LogD(mweTag, "Relock Mi Finish");
            this.Dispatcher.Invoke(new Action(() =>
            {
                //this.core.Reboot(nowDev, Basic.Arg.RebootOptions.System);
            }));
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
        }

        /// <summary>
        /// 推送文件到SDCARD完成的事件
        /// </summary>
        /// <param name="outputData">操作时的输出数据</param>
        private void PushFinish(object sender, FinishEventArgs e)
        {
            App.LogD(mweTag, "Push finish");
            if (e.Result.Level == ResultLevel.Successful)
            {
                MMessageBox.ShowDialog(Application.Current.Resources["Notice"].ToString(), Application.Current.FindResource("msgPushOK").ToString());
            }
            else
            {
                MMessageBox.ShowDialog(Application.Current.Resources["Notice"].ToString(), "Push_Failed 0x123123121232");
            }
        }

        /// <summary>
        /// 刷入自定义Recovery完成时发生的事件
        /// </summary>
        /// <param name="outputData">操作时的数据数据</param>
        private void FlashCustomRecFinish(object sender, FinishEventArgs e)
        {
            App.LogD(mweTag, "Flash Custom Recovery Finish");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
            MMessageBox.ShowDialog(Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("msgFlashOK").ToString());
        }
    }
}
