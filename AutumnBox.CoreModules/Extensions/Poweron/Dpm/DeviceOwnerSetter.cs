/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    #region 激活前的警告文本
    [ExtText("Warning",
        "This module will violently delete your account and users, and set this app as the device owner. Are you sure you want to do this?",
        "zh-cn:本模块将会暴力删除你的账户与用户,并设置这个应用为设备管理员(免ROOT)\n\n看!点击右上角问号可以浏览在线说明书")]
    [ExtText("RiskWarning",
        "This module has some risks. Although I will try my best to ensure its safety, it may still cause some software failures on your device. If you decide to continue, you will be at your own risk.",
        "zh-cn:本模块具有一定的风险,虽然我会尽力确保其安全,但如果造成可能的设备损坏,我不进行负责\n\n如果你决定继续,则视为你已了解并自负后果\n如果你决定继续,则视为你已了解并自负后果\n如果你决定继续,则视为你已了解并自负后果")]
    [ExtText("WarningRemoveLock",
        "there is only one thing you need to do:\n\ndelete screen lock and fingerprint lock\ndelete screen lock and fingerprint lock\ndelete screen lock and fingerprint lock\n\ngo to next step after you finished this step",
        "zh-cn:在开始前,你必须:\n\n删除屏幕锁和指纹锁\n删除屏幕锁和指纹锁\n删除屏幕锁和指纹锁\n\n完成后继续")]
    [ExtText("DomesticRomWarning",
        "In addition, China mobile phone users pay attention:\n\n1. Xiaomi mobile phone (MIUI) needs to enable MIUI security setting and close MIUI optimization in the developer settings\n2. Huawei mobile phone needs to manually exit Huawei account \n3. After activation, many mobile phones will show that your device has been taken over in notification bar, not beautiful but harmless\n4.Dual App may not be available",
        "zh-cn:另外,国产手机用户注意:\n\n1.小米手机(MIUI)需要在开发者选项开启MIUI安全设置,关闭MIUI优化\n2.华为手机需要手动退出华为账号\n3.在激活后,许多手机会显示您的设备被接管,不美观但无害\n4.应用双开将可能无法使用\n\n点击右上角问号可以浏览在线说明书")]
    [ExtText("SumsungSonyWarning",
        "Samsung and Sony users should also pay attention: \n\nSamsung devices may be locked after activation of DPM\n Device owner on Sony devices cannot be activated by adb after android 9.0, please use QR code\nYou can view detail of above two points in the manual",
        "zh-cn:三星和索尼用户也要注意:\n\n三星设备在激活DPM后可能会被锁住\n索尼设备在9.0后无法使用adb方式激活,请使用二维码方式\n\n以上两点在说明书中有详细说到\n\n点击右上角问号可以浏览在线说明书")]
    [ExtText("WarningLastChance",
        "This is the last chance to cancel,are u sure to continue?",
        "zh-cn:这是你最后一次取消的机会,一旦开始操作将不可停止")]
    #endregion

    #region 激活途中要用的文本
    [ExtText("HaveNotInstallApp", "You have not install the relative application!", "zh-cn:秋之盒检测到你没有安装相关应用!")]
    [ExtText("BtnIgnore", "Continue!", "zh-cn:强行继续")]
    [ExtText("BtnOk", "Okay", "zh-cn:确认")]
    [ExtText("BtnCancel", "Extracting dpmpro to device", "zh-cn:取消")]
    [ExtText("Checking", "Checking", "zh-cn:正在准备")]
    [ExtText("Extract", "Extracting dpmpro to device", "zh-cn:提取dpmpro")]
    [ExtText("Push", "Pushing dpmpro to device", "zh-cn:推送dpmpro")]
    [ExtText("RMAcc", "Removing accounts", "zh-cn:正在移除所有账号")]
    [ExtText("RMUser", "Removing users", "zh-cn:正在移除所有用户")]
    [ExtText("SettingDpm", "Setting device owner", "zh-cn:正在设置设备管理员")]
    [ExtText("UseDpmPro", "The dpm command not found,do you wanna try dpmpro?", "zh-cn:您的设备缺少DPM,使用DPMPRO吗?")]
    #endregion

    [ExtDesc("使用奇淫技巧暴力设置设备管理员", "en-us:Use the sneaky skills to set up the device administrator")]
    [ExtAuth("zsh2401")]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.nuclear.png")]
    public abstract class DeviceOwnerSetter : LeafExtensionBase
    {
        /// <summary>
        /// 必须是完整的组件名,包括包名
        /// </summary>
        protected abstract string ComponentName { get; }

        /// <summary>
        /// 依赖的App包名,用于在开始正式流程前对该应用安装情况进行检测,如果为null,则不检测
        /// </summary>
        protected abstract string PackageName { get; }

        /// <summary>
        /// LeafUI
        /// </summary>
        [LProperty]
        public ILeafUI UI { get; set; }

        /// <summary>
        /// TextManager
        /// </summary>
        [LProperty]
        public TextAttrManager TextManager { get; set; }

        /// <summary>
        /// 日志器
        /// </summary>
        [LProperty]
        private ILogger Logger { get; set; }

        /// <summary>
        /// Device
        /// </summary>
        [LProperty]
        public IDevice Device { get; set; }

        /// <summary>
        /// 入口函数
        /// </summary>
        [LMain]
        public void EntryPoint()
        {
            using (UI)
            {
                UI.Show();    //显示ui
                InitUI();   //初始化ui


                ////执行前的一些检查与提示
                //SetProgress("Checking", 10);
                //if (!DoAppCheck()) return;//进行APP安装检查
                //if (!DoWarn()) return;//进行一系列提示与警告

                //做出一系列警告与提示,只要一个不被同意,立刻再见
                if (!(DoAppCheck() && DoWarn()))
                {
                    UI.Shutdown();//直接关闭UI
                    return;//退出函数
                }

                /* 正式开始流程 */

                //构造一个命令执行器
                using (CommandExecutor executor = new CommandExecutor())
                {
                    //创建一个OutputBuilder
                    OutputBuilder outputBuilder = new OutputBuilder();

                    //接收并记录所有executor的输出
                    outputBuilder.Register(executor);

                    //将命令执行器输出定向到界面
                    executor.To(e => UI.WriteOutput(e.Text));

                    //构造一个dpmpro的控制器
                    var dpmpro = new DpmPro(executor, CoreLib.Current, Device);

                    //将dpmpro提取到临时目录
                    SetProgress("Extract", 0);
                    dpmpro.Extract();

                    //推送dpmpro到设备
                    SetProgress("Push", 20);
                    dpmpro.PushToDevice();

                    //移除账户
                    SetProgress("RMAcc", 40);
                    dpmpro.RemoveAccounts();

                    //移除用户
                    SetProgress("RMUser", 60);
                    dpmpro.RemoveUsers();

                    //使用可能的方式设置管理员,并记录结果
                    SetProgress("SettingDpm", 80);
                    var codeOfSetDeviceOwner = SetDeviceOwner(executor, dpmpro).ExitCode;

                    if (codeOfSetDeviceOwner == 0 && //如果设置成功并且
                        (PackageName == "com.catchingnow.icebox" //目标包名是冰箱
                        || PackageName == "web1n.stopapp")) //小黑屋
                    {
                        //给予APPOPS权限
                        executor.AdbShell(Device, $"pm grant {PackageName} android.permission.GET_APP_OPS_STATS");
                    }

                    //使用输出解析器,对记录的输出进行解析
                    DpmFailedMessageParser.Parse(codeOfSetDeviceOwner, outputBuilder.ToString(), out string tip, out string message);



                    //在输出框写下简要信息与建议
                    UI.WriteLine(message);
                    UI.ShowMessage(message);

                    //去除输出信息事件注册
                    outputBuilder.Unregister(executor);

                    //ui流程结束
                    UI.Finish(tip);
                }
            }
        }

        /// <summary>
        /// 用所有可能的方法设置设备管理员,并返回结果
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        /// <param name="dpmpro"></param>
        /// <returns></returns>
        private ICommandResult SetDeviceOwner(CommandExecutor executor, DpmPro dpmpro)
        {
            ICommandResult result = null;
            //先用自带dpm进行设置
            result = executor.AdbShell(Device, "dpm set-device-owner", ComponentName);
            //如果返回值为127,也就是说这设备连dpm都阉割了,就询问用户是否用dpmpro来设置设备管理员
            if (result.ExitCode == 127 && UI.DoYN(TextManager["UseDpmPro"]))
            {
                //用dpmpro设置设备管理员,并记录结果(覆盖普通dpm设置器的记录)
                result = dpmpro.SetDeviceOwner(ComponentName);
            }
            return result;
        }

        /// <summary>
        /// 设置进度信息,与核心代码无关
        /// </summary>
        /// <param name="keyOfTip"></param>
        /// <param name="progress"></param>
        private void SetProgress(string keyOfTip, int progress)
        {
            string tip = TextManager[keyOfTip] ?? keyOfTip;
            UI.Tip = tip;
            UI.Progress = progress;
            UI.WriteLine(tip);
        }

        /// <summary>
        /// 接收摧毁消息,将重要字段置null
        /// </summary>
        [LSignalReceive(Signals.COMMAND_DESTORY)]
        private void OnDestory()
        {
            UI = null;
            TextManager = null;
            Logger = null;
            Device = null;
        }

        #region 几个辅助函数
        /// <summary>
        /// 进行一系列警告,只要一条不同意便返回false
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        private bool DoWarn()
        {
            string[] warnMessageKeys = new string[]
            {
                "Warning", //第一次警告
                "RiskWarning",//风险自负
                "WarningRemoveLock",   //提示用户移除屏幕锁等
                "DomesticRomWarning",//国产ROM注意事项
                "SumsungSonyWarning",//三星索尼注意事项
                "WarningLastChance",   //最后一次机会取消
            };
            foreach (string key in warnMessageKeys)
            {
                bool yn = UI.DoYN(TextManager[key]);
                if (!yn) return false;
            }
            return true;
        }

        /// <summary>
        ///  初始化UI
        /// </summary>
        private void InitUI()
        {
            UI.Icon = this.GetIconBytes();
            UI.Title = this.GetName();
            UI.Height += 100;
            UI.Width += 100;
            UI.EnableHelpBtn(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start("http://www.atmb.top/go/help/dpmhelp?from=dpmext");
                }
                catch (Exception e)
                {
                    Logger.Warn("cannot go to dpm setter's help", e);
                }
            });
        }

        /// <summary>
        /// 对相关APP进行检查
        /// </summary>
        /// <returns></returns>
        private bool DoAppCheck()
        {
            if (PackageName == null) return true;
#pragma warning disable CS0618 // 类型或成员已过时
            var pm = new PackageManager(Device);
#pragma warning restore CS0618 // 类型或成员已过时
            var isInstall = pm.IsInstall(PackageName);
            return isInstall || UI.DoChoice(TextManager["HaveNotInstallApp"],
                btnYes: TextManager["BtnOkay"], btnNo: TextManager["BtnIgnore"],
               btnCancel: TextManager["BtnCancel"]) == false;
        }
        #endregion
    }
}
