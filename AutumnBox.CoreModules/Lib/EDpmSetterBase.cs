/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Exceptions;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.CoreModules.Lib
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
    [ExtText("Extract", "Extracting dpmpro to device", "zh-cn:提取dpmpro")]
    [ExtText("Push", "Pushing dpmpro to device", "zh-cn:推送dpmpro")]
    [ExtText("RMAcc", "Removing accounts", "zh-cn:正在移除所有账号")]
    [ExtText("RMUser", "Removing users", "zh-cn:正在移除所有用户")]
    [ExtText("SettingDpm", "Setting device owner", "zh-cn:正在设置设备管理员")]
    [ExtText("UseDpmPro", "The dpm command not found,do you wanna try dpmpro?", "zh-cn:您的设备缺少DPM,使用DPMPRO吗?")]
    [ExtText("MayFailed", "May successed", "zh-cn:可能失败")]
    [ExtText("MayFailedAdvice", "Maybe successful?Please check your app", "zh-cn:由于安卓DPM原因, 无法确定是否成功, 请自行查看手机进行判断")]
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
    #endregion

    [ExtDesc("使用奇淫技巧暴力设置设备管理员", "en-us:Use the sneaky skills to set up the device administrator")]
    [ExtAuth("zsh2401")]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.nuclear.png")]
    [DpmReceiver(null)]
    internal abstract class EDpmSetterBase : LeafExtensionBase
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        [LMain]
        public void EntryPoint(IDevice device, TextAttrManager texts, ILeafUI ui, ILogger logger)
        {
            using (ui)
            {
                //显示ui
                ui.Show();

                //初始化ui
                InitUI(this, ui, logger);

                //做出警告
                if (!DoWarn(ui, texts))
                {
                    ui.Shutdown();
                    return;
                }

                //获取要设置的接收器组件名
                string componentName = GetDpmReceiverName(GetType());

                //构造一个命令执行器
                using (CommandExecutor executor = new CommandExecutor())
                {
                    //将命令执行器输出定向到界面
                    executor.To(e => ui.WriteOutput(e.Text));

                    //构造一个dpmpro的控制器
                    var dpmpro = new CstmDpmCommander(executor, CoreLib.Current, device);

                    //将dpmpro提取到临时目录
                    ui.Progress = 0;
                    ui.Tip = texts["Extract"];
                    ui.WriteLine(texts["Extract"]);
                    dpmpro.Extract();

                    //推送dpmpro到设备
                    ui.Progress = 20;
                    ui.Tip = texts["Push"];
                    ui.WriteLine(texts["Push"]);
                    dpmpro.PushToDevice();

                    //移除账户
                    ui.Progress = 40;
                    ui.Tip = texts["RMAcc"];
                    ui.WriteLine(texts["RMAcc"]);
                    dpmpro.RemoveAccounts();

                    //移除用户
                    ui.Progress = 60;
                    ui.Tip = texts["RMUser"];
                    ui.WriteLine(texts["RMUser"]);
                    dpmpro.RemoveUsers();

                    //使用设备自带dpm设置管理员,并记录结果
                    ui.Progress = 80;
                    ui.Tip = texts["SettingDpm"];
                    ui.WriteLine(texts["SettingDpm"]);
                    var resultOfSetDpm = executor.AdbShell(device, "dpm set-device-owner", componentName);

                    //如果返回值为127,也就是说这设备连dpm都阉割了,就询问用户是否用dpmpro来设置设备管理员
                    if (resultOfSetDpm.ExitCode == 127 && ui.DoYN(texts["UseDpmPro"]))
                    {
                        //用dpmpro设置设备管理员,并记录结果
                        resultOfSetDpm = dpmpro.SetDeviceOwner(componentName);
                    }
                    //使用输出解析器,对记录的输出进行解析
                    if (DpmFailedMessageParser.TryParse(resultOfSetDpm, out string tip, out string message))
                    {
                        //解析成功,在输出框写下简要信息与建议
                        ui.WriteLine(texts[message]);
                        ui.ShowMessage(texts[message]);
                        //ui流程结束
                        ui.Finish(texts[tip]);
                    }
                    else
                    {
                        //解析失败,告诉用户可能成功
                        ui.ShowMessage(texts["MayFailedAdvice"]);
                        ui.WriteLine(texts["MayFailedAdvice"]);
                        ui.Finish(texts["MayFailed"]);
                    }
                }
            }
        }

        #region 需要被外部解析器使用的文本键值
        public const string TIP_OK = "_OK";
        public const string TIP_FAIL = "_FAIL";
        public const string ERR_MSG_KEY_UNKNOWN = "_Unknown";
        public const string ERR_MSG_KEY_HAVE_USERS = "_HaveUsers";
        public const string ERR_MSG_KEY_HAVE_ACCOUNTS = "_HaveAccs";
        public const string ERR_MSG_KEY_MIUI_SEC = "_MiuiSec";
        public const string ERR_MSG_KEY_DO_ALREADY_SET = "_AlreadySet";
        public const string ERR_MSG_KEY_DPM_NOT_FOUND = "_DpmNotFound";
        public const string OK_MSG = "_Ok";
        #endregion

        #region 几个静态辅助函数
        /// <summary>
        /// 进行一系列警告,只要一条不同意便返回false
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        private static bool DoWarn(ILeafUI ui, TextAttrManager texts)
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
                bool yn = ui.DoYN(texts[key]);
                if (!yn) return false;
            }
            return true;
        }

        /// <summary>
        ///  初始化UI
        /// </summary>
        private static void InitUI(LeafExtensionBase leaf, ILeafUI ui, ILogger logger)
        {
            ui.Icon = leaf.GetIconBytes();
            ui.Title = leaf.GetName();
            ui.Height += 100;
            ui.Width += 100;
            ui.EnableHelpBtn(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start("http://www.atmb.top/go/help/dpmhelp?from=dpmext");
                }
                catch (Exception e)
                {
                    logger.Warn("cannot go to dpm setter's help", e);
                }
            });
        }

        /// <summary>
        /// 获取子类需要设置的DPM接收器组件名
        /// </summary>
        /// <returns></returns>
        private static string GetDpmReceiverName(Type classExtension)
        {
            ClassExtensionScanner scanner = new ClassExtensionScanner(classExtension);
            scanner.Scan(ClassExtensionScanner.ScanOption.Informations);
            var infos = scanner.Informations;
            return infos[DpmReceiverAttribute.KEY].Value as string;
        }
        #endregion
    }
}
