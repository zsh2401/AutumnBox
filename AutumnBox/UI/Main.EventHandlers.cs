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
        private void InitEvents()
        {
            core.dl.DevicesChange += new DevicesListener.DevicesChangeHandler(DevicesChange);
            core.PushFinish += new Basic.Core.FinishEventHandler(PushFinish);
            core.FlashRecoveryFinish += new Basic.Core.FinishEventHandler(FuckFinish);
            this.SetUIFinish += new NormalEventHandler(()=> {
                this.rateBox.Dispatcher.Invoke(new Action(() => {
                    this.rateBox.Close();
                }));
            });
        }
        private void DevicesChange(Object obj, DevicesHashtable devicesHashtable)
        {
            this.DevicesListBox.Dispatcher.Invoke(new Action(() =>
            {
                this.DevicesListBox.Items.Clear();
                foreach (DictionaryEntry entry in devicesHashtable)
                {
                    this.DevicesListBox.Items.Add(entry.Key);
                }
                if (devicesHashtable.Count == 1) {
                    this.DevicesListBox.SelectedIndex = 0;
                }
            }));
        }
        private void PushFinish(OutputData outputData) {
            this.rateBox.Dispatcher.Invoke(new Action(() =>
            {
                this.rateBox.Close();
                MMessageBox.ShowDialog(this, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("PushOK").ToString());
            }));
        }
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
