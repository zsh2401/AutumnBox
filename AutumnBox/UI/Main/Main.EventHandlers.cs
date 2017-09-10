using AutumnBox.Basic.Devices;
using AutumnBox.UI;
using System;
using System.Collections;
using System.Windows;
using AutumnBox.Debug;
using AutumnBox.Util;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.Functions.Event;

namespace AutumnBox
{
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
            App.devicesListener.DevicesChange += (s, devicesHashtable) =>
            {
                Log.d(TAG, "Devices change handing.....");
                this.Dispatcher.Invoke(() =>
                {
                    DevicesListBox.Items.Clear();
                    Log.d(TAG, "Clear");
                    foreach (DictionaryEntry i in devicesHashtable)
                    {
                        Log.d(TAG, "Adding");
                        DevicesListBox.Items.Add(i.Key);
                    }
                    if (devicesHashtable.Count == 1)
                    {
                        DevicesListBox.SelectedIndex = 0;
                    }
                });
            };

            //推送文件到手机完成时的事件
            //core.SendFileFinish += new Basic.EventsHandlers.SimpleFinishEventHandler(PushFinish);
            //设置UI完成时的事件
            this.SetUIFinish += new NormalEventHandler(() =>
            {
                this.rateBox.Dispatcher.Invoke(new Action(() =>
                {
                    this.HideRateBox();
                }));
                Log.d(mweTag, "SetUIFinish");
            });
        }


        #region 界面的事件
        /// <summary>
        /// 获取公告完成的事件处理
        /// </summary>
        /// <param name="notice">获取到的公告</param>
        private void NoticeGetter_NoticeGetFinish(Notice notice)
        {
            textBoxGG.Dispatcher.Invoke(new Action(() =>
            {
                textBoxGG.Text = FindResource("Notice_") + " : " + notice.content;
            }));
        }

        /// <summary>
        /// 检测更新完成后的处理
        /// </summary>
        /// <param name="haveUpdate">是否有更新</param>
        /// <param name="updateVersionInfo">更新信息</param>
        private void UpdateChecker_UpdateCheckFinish(bool haveUpdate, VersionInfo updateVersionInfo)
        {
            if (haveUpdate)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    new UpdateNoticeWindow(this, updateVersionInfo).ShowDialog();
                }));
            }
        }
        #endregion

        #region 功能事件
        /// <summary>
        /// 通用事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuncFinish(object sender, FinishEventArgs e)
        {
            HideRateBox();
            if (sender is Basic.Functions.FileSender)
            {
                PushFinish(sender, e);
            }
            else if (sender is Basic.Functions.BreventServiceActivator)
            {
                ActivatedBrvent(sender, e);
            }
            else if (sender is Basic.Functions.ActivityLauncher)
            {
                //TODO
            }
            else if (sender is Basic.Functions.CustomRecoveryFlasher)
            {
                //TODO
            }
            else if (sender is Basic.Functions.RebootOperator)
            {
                //TODO
            }
            else if (sender is Basic.Functions.XiaomiSystemUnlocker)
            {
                UnlockMiSystemFinish(sender, e);
            }
            else if (sender is Basic.Functions.XiaomiBootloaderRelocker)
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
            Log.d(TAG, e.OutputData.error.ToString());
            Log.d(TAG, e.OutputData.nOutPut);
            this.Dispatcher.Invoke(new Action(() =>
            {
                HideRateBox();
            }));
            BreventShOutputHandler handler = new BreventShOutputHandler(e.OutputData);
            if (handler.isOk)
            {
                MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("StartBreventServiceSuc").ToString() + handler.output);
            }
            else
            {
                MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("StartBreventServiceFail").ToString() + handler.output);
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
        /// 设变发生改变时的事件
        /// </summary>
        /// <param name="obj">发生事件的"地方"</param>
        /// <param name="devicesHashtable">当前设备列表</param>
        private void DevicesChange(Object sender, DevicesHashtable devicesHashtable)
        {
            /*
             * 由于是从设备监听器线程发生的事件
             * 并且需要操作主界面,因此要用匿名函数来进行操作
             */
            Log.d(mweTag, "Device Change");
            this.DevicesListBox.Dispatcher.Invoke(new Action(() =>
            {
                //清空主界面listbox列表
                this.DevicesListBox.Items.Clear();
                //添加设备列表到主界面设备列表listbox
                foreach (DictionaryEntry entry in devicesHashtable)
                {
                    this.DevicesListBox.Items.Add(entry.Key);
                }
                //如果只有一个设备,那么帮用户选中它
                if (devicesHashtable.Count == 1)
                {
                    this.DevicesListBox.SelectedIndex = 0;
                }
            }));
        }

        /// <summary>
        /// 推送文件到SDCARD完成的事件
        /// </summary>
        /// <param name="outputData">操作时的输出数据</param>
        private void PushFinish(object sender, FinishEventArgs e)
        {
            Log.d(mweTag, "Push finish");
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.HideRateBox();
            }));
            MMessageBox.ShowDialog(this, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("PushOK").ToString());
        }

        /// <summary>
        /// 刷入自定义Recovery完成时发生的事件
        /// </summary>
        /// <param name="outputData">操作时的数据数据</param>
        private void FuckFinish()
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
