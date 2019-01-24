/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
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
        "zh-cn:本模块具有一定的风险,虽然我会尽力确保其安全,但仍然可能导致您的设备出现一些软件故障\n\n如果你决定继续,则视为你已了解并自负后果")]
    [ExtText("WarningRemoveLock",
        "there is only one thing you need to do:\n\ndelete screen lock and fingerprint lock\ndelete screen lock and fingerprint lock\ndelete screen lock and fingerprint lock\n\ngo to next step after you finished this step",
        "zh-cn:在开始前,你必须:\n\n删除屏幕锁和指纹锁\n删除屏幕锁和指纹锁\n删除屏幕锁和指纹锁\n\n完成后继续")]
    [ExtText("DomesticRomWarning",
        "In addition, China mobile phone users pay attention:\n\n1. Xiaomi mobile phone (MIUI) needs to enable MIUI security setting and close MIUI optimization in the developer settings\n2. Huawei mobile phone needs to manually exit Huawei account \n3. After activation, many mobile phones will show that your device has been taken over in notification bar, not beautiful but harmless\n4.Dual App may not be available",
        "zh-cn:另外,国产手机用户注意:\n\n1.小米手机(MIUI)需要在开发者选项开启MIUI安全设置,关闭MIUI优化\n2.华为手机需要手动退出华为账号\n3.在激活后,许多手机会显示您的设备被接管,不美观但无害\n4.应用双开将可能无法使用")]
    [ExtText("SumsungSonyWarning",
        "Samsung and Sony users should also pay attention: \n\nSamsung devices may be locked after activation of DPM\n Device owner on Sony devices cannot be activated by adb after android 9.0, please use QR code\nYou can view detail of above two points in the manual",
        "zh-cn:三星和索尼用户也要注意:\n\n三星设备在激活DPM后可能会被锁住\n索尼设备在9.0后无法使用adb方式激活,请使用二维码方式\n\n以上两点在说明书中有详细说到")]
    [ExtText("WarningLastChance",
        "This is the last chance to cancel,are u sure to continue?",
        "zh-cn:这是你最后一次取消的机会,一旦开始操作将不可停止")]
    #endregion

    #region 激活途中要用的文本
    [ExtText("HaveNotInstallApp", "You have not install the relative application!", "zh-cn:秋之盒检测到你没有安装相关应用!")]
    [ExtText("BtnIgnore", "Continue!", "zh-cn:强行继续")]
    [ExtText("BtnOk", "Okay", "zh-cn:好吧")]
    [ExtText("BtnCancel", "Extracting dpmpro to device", "zh-cn:取消")]
    [ExtText("Checking", "Checking", "zh-cn:正在准备")]
    [ExtText("Extract", "Extracting dpmpro to device", "zh-cn:提取dpmpro")]
    [ExtText("Push", "Pushing dpmpro to device", "zh-cn:推送dpmpro")]
    [ExtText("RMAcc", "Removing accounts", "zh-cn:正在移除所有账号")]
    [ExtText("RMUser", "Removing users", "zh-cn:正在移除所有用户")]
    [ExtText("SettingDpm", "Setting device owner", "zh-cn:正在设置设备管理员")]
    [ExtText("UseDpmPro", "The dpm command not found,do you wanna try dpmpro?", "zh-cn:您的设备缺少DPM,使用DPMPRO吗?")]
    [ExtText("MayFailed", "May successed", "zh-cn:可能失败")]
    [ExtText("MayFailedAdvice", "Maybe successful?Please check your app", "zh-cn:很有可能失败了!\n由于安卓DPM原因, 无法确定是否成功, 请自行查看手机进行判断")]
    [ExtText(TIP_OK, "Success!!", "zh-cn:设置成功!")]
    [ExtText(TIP_FAIL, "Fail!!", "zh-cn:失败!")]
    [ExtText(OK_MSG,
        "Finally succeeded, please go to the mobile phone software to confirm, if you need to uninstall, be sure to go to the software to operate within its settings, otherwise it may cause DPM residue, then it will not be saved.",
        "zh-cn:终于成功了,请前往手机端软件进行确认,如果需要卸载,一定要前往该软件其设置内进行操作,否则可能导致DPM残留,那可就没得救了")]
    [ExtText(ERR_MSG_KEY_DO_ALREADY_SET,
        "Already have a device owner on the device, please remove it first",
        "zh-cn:设备管理员已经被设置过了!请先移除之前的设备管理员应用(冻结APP请前往该APP设置进行移除)")]
    [ExtText(ERR_MSG_KEY_UNKNOWN, "Unknown erroe!plz check the pro output!!",
        "zh-cn:奇怪的问题,请点击左上角复制按钮,并将其发送给你想咨询的人")]
    [ExtText(ERR_MSG_KEY_HAVE_USERS, "There are other users on the device, please delete the visitor and other users.",
        "zh-cn:设备上还有多余的用户!请尝试删除应用多开,访客模式等再试")]
    [ExtText(ERR_MSG_KEY_HAVE_ACCOUNTS,
        "There are other accounts on the device, please go to setttings->accounts/sync and remove all.",
        "zh-cn:设备上还有多余的账号!前往设置->同步/账号->删除,然后再试")]
    [ExtText(ERR_MSG_KEY_MIUI_SEC,
        "Please go to the developer settings and turn on MIUI USB Security Setting and turn off  MIUI Opt Setting"
        , "zh-cn:出现这个问题,请关闭MIUI优化并开启MIUI安全调试!(均在开发者选项中)然后再试!")]
    [ExtText(ERR_MSG_KEY_DPM_NOT_FOUND,
        "dpm not found...",
        "zh-cn:在你的设备上,dpmpro似乎被阉割了,蛋疼啊...")]
    [ExtText(ERR_MSG_KEY_UNKNOWN_ADMIN,
        "Please make sure you have the relevant app installed",
        "zh-cn:找不到设备管理员程序!请确保你安装了相关应用!")]
    
    #endregion

    [ExtDesc("使用奇淫技巧暴力设置设备管理员", "en-us:Use the sneaky skills to set up the device administrator")]
    [ExtAuth("zsh2401")]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.nuclear.png")]
    internal abstract class DeviceOwnerSetter : LeafExtensionBase
    {
        #region 需要被外部解析器使用的文本键值
        public const string TIP_OK = "_OK";
        public const string TIP_FAIL = "_FAIL";
        public const string ERR_MSG_KEY_UNKNOWN = "_Unknown";
        public const string ERR_MSG_KEY_HAVE_USERS = "_HaveUsers";
        public const string ERR_MSG_KEY_HAVE_ACCOUNTS = "_HaveAccs";
        public const string ERR_MSG_KEY_MIUI_SEC = "_MiuiSec";
        public const string ERR_MSG_KEY_DO_ALREADY_SET = "_AlreadySet";
        public const string ERR_MSG_KEY_DPM_NOT_FOUND = "_DpmNotFound";
        public const string ERR_MSG_KEY_UNKNOWN_ADMIN = "_UnknownAdmin";
        public const string OK_MSG = "_Ok";
        #endregion

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
                    var result = SetDeviceOwner(executor, dpmpro);

                    //使用输出解析器,对记录的输出进行解析
                    if (DpmFailedMessageParser.TryParse(result, out string keyOfTip, out string keyOfmessage))
                    {
                        //解析成功,在输出框写下简要信息与建议
                        UI.WriteLine(TextManager[keyOfmessage]);
                        UI.ShowMessage(TextManager[keyOfmessage]);
                        //ui流程结束
                        UI.Finish(TextManager[keyOfTip]);
                    }
                    else
                    {
                        //解析失败,告诉用户可能失败
                        UI.ShowMessage(TextManager["MayFailedAdvice"]);
                        UI.WriteLine(TextManager["MayFailedAdvice"]);
                        //ui流程结束
                        UI.Finish(TextManager["MayFailed"]);
                    }
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
        private CommandExecutor.Result SetDeviceOwner(CommandExecutor executor, DpmPro dpmpro)
        {
            CommandExecutor.Result result = null;
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
