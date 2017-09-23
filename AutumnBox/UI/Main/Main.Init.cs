namespace AutumnBox
{
    using AutumnBox.Basic;
    using AutumnBox.Basic.Devices;
    using AutumnBox.Debug;
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
            GetNotice();//开始获取公告
            UpdateCheck();//更新检测
            InitWebPage();//初始化浏览器
#if DEBUG
            this.labelTitle.Content += "  " + StaticData.nowVersion.version + "-Debug";
#else
            this.labelTitle.Content += "  " + StaticData.nowVersion.version + "-Release";
#endif
        }
        void GetNotice() {
            //公告获取器
            NoticeGetter noticeGetter = new NoticeGetter();
            noticeGetter.NoticeGetFinish += NoticeGetter_NoticeGetFinish;
            noticeGetter.Get();
        }
        void UpdateCheck()
        {
            //更新检测器
            UpdateChecker updateChecker = new UpdateChecker();
            updateChecker.UpdateCheckFinish += UpdateChecker_UpdateCheckFinish;
            updateChecker.Check();
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
