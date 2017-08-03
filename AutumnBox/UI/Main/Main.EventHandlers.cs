/*本文件中的代码均为主界面中的一些自定义的事件*/

using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using AutumnBox.UI;
using System;
using System.Collections;
using System.Windows;

namespace AutumnBox
{
    public partial class Window1
    {
        /// <summary>
        /// 初始化各种事件
        /// </summary>
        private void InitEvents()
        {
            //设备列表发生改变时的事件
            core.dl.DevicesChange += new DevicesListener.DevicesChangeHandler(DevicesChange);
            //推送文件到手机完成时的事件
            core.PushFinish += new Basic.Core.FinishEventHandler(PushFinish);
            //刷入Recovery完成时的事件
            core.FlashRecoveryFinish += new Basic.Core.FinishEventHandler(FuckFinish);
            this.SetUIFinish += new NormalEventHandler(() =>
            {
                this.rateBox.Dispatcher.Invoke(new Action(() =>
                {
                    this.rateBox.Close();
                }));
            });
            //重新上锁小米手机完成时的事件
            core.RelockMiFinish += new Basic.Core.FinishEventHandler(RelockMiFinish);
            //解锁小米系统时的事件
            core.UnlockMiSystemFinish += new Basic.Core.FinishEventHandler(UnlockMiSystemFinish);
            //重启完成时的事件
            core.RebootFinish += new Basic.Core.FinishEventHandler((o) =>
            {
                MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("RebootOK").ToString());
            });
        }

        private void UnlockMiSystemFinish(OutputData o)
        {
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.rateBox.Close();
                MMessageBox.ShowDialog(this, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("IfAllOK").ToString());
            }));
        }

        private void RelockMiFinish(OutputData o)
        {
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                try { this.rateBox.Close(); } catch { }
                MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("IfAllOK").ToString());
            }));
        }

        /// <summary>
        /// 设变发生改变时的事件
        /// </summary>
        /// <param name="obj">发生事件的"地方"</param>
        /// <param name="devicesHashtable">当前设备列表</param>
        private void DevicesChange(Object obj, DevicesHashtable devicesHashtable)
        {
            /*
             * 由于是从设备监听器线程发生的事件
             * 并且需要操作主界面,因此要用匿名函数来进行操作
             */
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
        private void PushFinish(OutputData outputData)
        {
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.rateBox.Close();
                MMessageBox.ShowDialog(this, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("PushOK").ToString());
            }));
        }
        /// <summary>
        /// 刷入自定义Recovery完成时发生的事件
        /// </summary>
        /// <param name="outputData">操作时的数据数据</param>
        private void FuckFinish(OutputData outputData)
        {
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.rateBox.Close();
                MMessageBox.ShowDialog(this, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("FlashOK").ToString());
            }));
        }
    }
}
