namespace AutumnBox
{
    using AutumnBox.Basic;
    using AutumnBox.Basic.Devices;
    using AutumnBox.Debug;
    using AutumnBox.NetUtil;
    using AutumnBox.UI;
    using AutumnBox.Util;
    using System;
    using System.Threading;
    public partial class Window1
    {
        RateBox rateBox;
        string TAG = "MainWindow";
        //现在选取的设备
        //private string nowDev { get { return DevicesListBox.SelectedItem.ToString(); } }
        void CustomInit()
        {
            InitEvents();//绑定各种事件
            ChangeButtonAndImageByStatus(DeviceStatus.NO_DEVICE);//将所有按钮设置成关闭状态
#if DEBUG
            this.labelTitle.Content += "  " + StaticData.nowVersion.version + "-Debug";
#else
            this.labelTitle.Content += "  " + StaticData.nowVersion.version + "-Release";
#endif
        }
        void GetNotice()
        {
            new MOTDGetter().Run((s, e) =>
            {
                textBoxGG.Dispatcher.Invoke(() =>
                {
                    textBoxGG.Text = e.Header + " : " + e.Message;
                });
            });
        }
        void UpdateCheck()
        {
            new NetUtil.UpdateChecker().Run((s, e) =>
            {
                if (e.NeedUpdate)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        new UpdateNoticeWindow(this, e).ShowDialog();
                    });
                }
            });
        }
        void InitWebPage()
        {
            new Thread(() =>
            {
                Guider guider = new Guider();
                if (guider.isOk)
                {
                    try
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            webFlashHelper.Navigate(guider["urls"]["flashhelp"].ToString());
                            webSaveDevice.Navigate(guider["urls"]["savedevicehelp"].ToString());
                            webFlashRecHelp.Navigate(guider["urls"]["flashrecoveryhelp"].ToString());
                        }));
                    }
                    catch (Exception e)
                    {
                        Log.d(TAG, "web browser set fail");
                        Log.d(TAG, e.Message);
                    }
                }
                else
                {
                    Log.d(TAG, "web browser set fail because guider is not ok");
                }
            }).Start();
        }
    }
}
