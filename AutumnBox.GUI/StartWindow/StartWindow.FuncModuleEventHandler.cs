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
namespace AutumnBox.GUI
{
    using System;
    using System.Windows;
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Basic.Function;
    using AutumnBox.Basic.Function.Modules;
    using AutumnBox.Shared.CstmDebug;
    using AutumnBox.GUI.Helper;
    using AutumnBox.GUI.Windows;

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
                else if (sender is ApkInstaller)
                {
                    MMessageBox.ShowDialog("Finished", "Install successful");
                }
                else if (sender is ScreenShoter)
                {
                    MMessageBox.ShowDialog(App.Current.Resources["Success"].ToString(), App.Current.Resources["msgSaveSuccessful"].ToString());
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
            Logger.D(this, e.OutputData.Error.ToString());
            Logger.D(this, e.OutputData.Out.ToString());
            Logger.D(this, "Enter the ActivatedBrevent Handler in the GUI");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
            if (e.Result.Level != ResultLevel.Successful)
            {
                e.Result.Message = App.Current.Resources["errormsgBrventActivtedUnsuccess"].ToString();
                e.Result.Advise = App.Current.Resources["advsBrventActivtedUnsuccess"].ToString();
            }
            this.Dispatcher.Invoke(() =>
            {
                ModuleResultWindow.FastShow(e.Result);
            });
        }

        /// <summary>
        /// 解锁小米系统完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void UnlockMiSystemFinish(object sender, FinishEventArgs e)
        {
            Logger.D(this, "Enter the Unlock Mi  System Finish Handler in the GUI");
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
            Logger.D(this, "Enter the Relock Mi Finish Handler in the GUI");
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
            Logger.D(this, "Enter the Push Finish Handler in the GUI");
            if (e.Result.Level == ResultLevel.Successful)
            {
                MMessageBox.ShowDialog(Application.Current.Resources["Notice"].ToString(), Application.Current.FindResource("msgPushOK").ToString());
            }
            else
            {
                if (e.Result.WasForcblyStop) { Logger.T(this, "File send was force stoped by user"); return; };
                e.Result.Message = Application.Current.FindResource("msgPushFail").ToString();
                e.Result.Advise = Application.Current.FindResource("advsFileSendUnsuccess").ToString();
                ModuleResultWindow.FastShow(e.Result);
            }
        }

        /// <summary>
        /// 刷入自定义Recovery完成时发生的事件
        /// </summary>
        /// <param name="outputData">操作时的数据数据</param>
        private void FlashCustomRecFinish(object sender, FinishEventArgs e)
        {
            Logger.D(mweTag, "Flash Custom Recovery Finish");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
            MMessageBox.ShowDialog(Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("msgFlashOK").ToString());
        }
    }
}
