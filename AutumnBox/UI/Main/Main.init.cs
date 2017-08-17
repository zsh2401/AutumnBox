using AutumnBox.Basic;
using AutumnBox.Basic.Devices;
using AutumnBox.Debug;
using AutumnBox.UI;
using AutumnBox.Util;
using System;
using System.Threading;

namespace AutumnBox
{
    public partial class Window1
    {
        Core core = new Core();
        RateBox rateBox;
        string TAG = "MainWindow";
        private string nowDev { get { return DevicesListBox.SelectedItem.ToString(); } }
        public void CustomInit() {
            InitEvents();//绑定各种事件
            ChangeButtonAndImageByStatus(DeviceStatus.NO_DEVICE);//将所有按钮设置成关闭状态

            Log.d("App Version", StaticData.nowVersion.version);
            webFlashHelper.Navigate(AppDomain.CurrentDomain.BaseDirectory + "HTML/flash_help.htm");
            webSaveDevice.Navigate(AppDomain.CurrentDomain.BaseDirectory + "HTML/save_fucking_device.htm");
            webFlashRecHelp.Navigate(AppDomain.CurrentDomain.BaseDirectory + "HTML/flash_recovery.htm");

            UpdateChecker updateChecker = new UpdateChecker();
            updateChecker.UpdateCheckFinish += UpdateChecker_UpdateCheckFinish;
            updateChecker.Check();

            Thread initNoticeThread = new Thread(InitNotice);
            initNoticeThread.Name = "InitNoticeThread";
            initNoticeThread.Start();

#if DEBUG
            bool isDebug = true;
#else
            bool isDebug = false;
#endif
            //这里其实不用这么麻烦的语法的...但是为了记下这个语法,我还是用一下吧...
            this.labelTitle.Content +=
                isDebug ? "  " + StaticData.nowVersion.version + "-Debug" : "  " + StaticData.nowVersion.version + "-Release";
        }
    }
}
