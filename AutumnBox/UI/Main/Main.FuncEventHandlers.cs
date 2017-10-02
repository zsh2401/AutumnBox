namespace AutumnBox
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.UI;
    using System;
    using System.Windows;
    using AutumnBox.Debug;
    using AutumnBox.Util;
    using AutumnBox.Basic.Functions.Event;
    using AutumnBox.Basic.Functions;
    /// <summary>
    /// 各种界面事件
    /// </summary>
    public partial class Window1
    {
        string mweTag = "MainWindowEvent";

        /// <summary>
        /// 初始化各种事件
        /// </summary>
        private void InitEvents()
        {
            //设备列表发生改变时的事件
            App.devicesListener.DevicesChanged += (s, e) => {
                /*
             * 由于是从设备监听器线程发生的事件
             * 并且需要操作主界面,因此要用匿名函数来进行操作
             */
                Log.d(TAG, "Devices change handing.....");
                this.Dispatcher.Invoke(() =>
                {
                    DevicesListBox.Items.Clear();
                    Log.d(TAG, "Clear");
                    e.DevicesList.ForEach((info) => { DevicesListBox.Items.Add(info); });
                    DevicesListBox.DisplayMemberPath = "Id";
                    if (e.DevicesList.Count == 1)
                    {
                        DevicesListBox.SelectedIndex = 0;
                    }
                });
            };
        }

        #region 功能事件
        /// <summary>
        /// 通用事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuncFinish(object sender, FinishEventArgs e)
        {
            HideRateBox();
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
        }

        /// <summary>
        /// 黑域启动完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivatedBrvent(object sender, FinishEventArgs e)
        {
            Log.d(TAG, e.OutputData.Error.ToString());
            Log.d(TAG, e.OutputData.Out.ToString());
            this.Dispatcher.Invoke(new Action(() =>
            {
                HideRateBox();
            }));
            if (!e.Result.IsSuccessful) {
                MMessageBox.ShowDialog(this,"Fail",e.Result.OutputData.ToString());
            }
        }

        /// <summary>
        /// 解锁小米系统完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void UnlockMiSystemFinish(object sender, FinishEventArgs e)
        {
            Log.d(mweTag, "UnlockMiSystemFinish Event ");
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.HideRateBox();
            }));
        }

        /// <summary>
        /// 重新给小米手机上锁完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void RelockMiFinish(object sender, FinishEventArgs e)
        {
            Log.d(mweTag, "Relock Mi Finish");
            this.Dispatcher.Invoke(new Action(() =>
            {
                //this.core.Reboot(nowDev, Basic.Arg.RebootOptions.System);
            }));
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.HideRateBox();
            }));
        }

        /// <summary>
        /// 推送文件到SDCARD完成的事件
        /// </summary>
        /// <param name="outputData">操作时的输出数据</param>
        private void PushFinish(object sender, FinishEventArgs e)
        {
            Log.d(mweTag, "Push finish");
            if (e.Result.IsSuccessful)
            {
                MMessageBox.ShowDialog(this, Application.Current.Resources["Notice"].ToString(), Application.Current.FindResource("PushOK").ToString());
            }
            else {
                MMessageBox.ShowDialog(this, Application.Current.Resources["Notice"].ToString(), "Push_Failed 0x123123121232");
            }
        }

        /// <summary>
        /// 刷入自定义Recovery完成时发生的事件
        /// </summary>
        /// <param name="outputData">操作时的数据数据</param>
        private void FlashCustomRecFinish(object sender, FinishEventArgs e)
        {
            Log.d(mweTag, "Flash Custom Recovery Finish");
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.HideRateBox();
            }));
            MMessageBox.ShowDialog(this, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("FlashOK").ToString());
        }
        #endregion
    }
}
