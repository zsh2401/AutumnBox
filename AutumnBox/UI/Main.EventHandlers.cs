using AutumnBox.Basic.Devices;
using System;
using System.Collections;

namespace AutumnBox
{
    public partial class Window1
    {
        private void InitEvents() {
            core.dl.DevicesChange += new DevicesListener.DevicesChangeHandler(DevicesChange);
        }
        private void DevicesChange(Object obj,DevicesHashtable devicesHashtable) {
            this.DevicesListBox.Dispatcher.Invoke(new Action(() =>
            {
                this.DevicesListBox.Items.Clear();
                foreach (DictionaryEntry entry in devicesHashtable)
                {
                    this.DevicesListBox.Items.Add(entry.Key);
                }
            }));
        }
    }
}
