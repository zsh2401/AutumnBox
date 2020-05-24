using AutumnBox.Leafx.Enhancement.ClassTextKit;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ClassText(TIP_MAY_FAILED, "May successed", "zh-cn:可能失败")]
    [ClassText(MSG_MAYFAIL, "Maybe successful?Please check your app", "zh-cn:很有可能失败了!\n由于安卓DPM原因, 无法确定是否成功, 请自行查看手机进行判断")]
    [ClassText(TIP_FAILED, "FAILED!!", "zh-cn:失败!")]
    [ClassText(TIP_OK, "Success!!", "zh-cn:设置成功!")]
    [ClassText(MSG_OK,
     "Finally succeeded, please go to the mobile phone software to confirm, if you need to uninstall, be sure to go to the software to operate within its settings, otherwise it may cause DPM residue, then it will not be saved.",
     "zh-cn:终于成功了,请前往手机端软件进行确认,如果需要卸载,一定要前往该软件其设置内进行操作,否则可能导致DPM残留,那可就没得救了")]
    [ClassText(MSG_OWNER_SETTED,
     "Already have a device owner on the device, please remove it first",
     "zh-cn:设备管理员已经被设置过了!请先移除之前的设备管理员应用(冻结APP请前往该APP设置进行移除)")]
    [ClassText(MSG__UNKNOWN_ERR, "Unknown erroe!plz check the pro output!!",
     "zh-cn:奇怪的问题,请点击左上角复制按钮,并将其发送给你想咨询的人")]
    [ClassText(MSG_OTH_USER, "There are other users on the device, please delete the visitor and other users.",
     "zh-cn:设备上还有多余的用户!请尝试删除应用多开,访客模式等再试")]
    [ClassText(MSG_OTH_ACC,
     "There are other accounts on the device, please go to setttings->accounts/sync and remove all.",
     "zh-cn:设备上还有多余的账号!前往设置->同步/账号->删除,然后再试")]
    [ClassText(MSG_MIUI_SEC,
     "Please go to the developer settings and turn on MIUI USB Security Setting and turn off  MIUI Opt Setting"
     , "zh-cn:出现这个问题,请关闭MIUI优化并开启MIUI安全调试!(均在开发者选项中)然后再试!")]
    [ClassText(MSG_DPM_NTF,
     "dpm not found...",
     "zh-cn:你设备上的DPM命令被精简,无法设定")]
    [ClassText(MSG_OWNER_NTF,
     "Please make sure you have the relevant app installed",
     "zh-cn:找不到设备管理员程序!请确保你安装了相关应用!")]
    [ClassText(MSG_SEG_FAULT,
     "Because of some problems, the AutumnBox can't help you remove accounts and users, please manually remove and try again (seg fault)",
     "zh-cn:因为一些问题,秋之盒无法帮助你移除账号和用户,请手动移除后再试(seg fault)")]

    internal static class DpmFailedMessageParser
    {
        const string TIP_OK = "1";
        const string TIP_FAILED = "2";
        const string TIP_MAY_FAILED = "3";
        const string MSG_OK = "4";
        const string MSG_MAYFAIL = "5";
        const string MSG_OWNER_SETTED = "6";
        const string MSG__UNKNOWN_ERR = "7";
        const string MSG_OTH_USER = "8";
        const string MSG_OTH_ACC = "9";
        const string MSG_MIUI_SEC = "10";
        const string MSG_DPM_NTF = "11";
        const string MSG_OWNER_NTF = "12";
        const string MSG_SEG_FAULT = "13";

        static readonly ClassTextReader text
           = ClassTextReaderCache.Acquire(typeof(DpmFailedMessageParser));

        public static void Parse(int exitCode, string output, out string tip, out string message)
        {
            var outputString = output.ToString().ToLower();
            if (exitCode == 0 && outputString.Contains("success"))
            {
                tip = text[TIP_OK];
                message = text[MSG_OK];
            }
            else
            {
                HandleError(exitCode, output.ToString(), out tip, out message);
            }
        }
        private static void HandleError(int exitCode, string output, out string tip, out string msg)
        {
            output = output.ToLower();
            tip = text[TIP_FAILED];
            if (output.Contains("segmentation fault"))
            {
                msg = text[MSG_SEG_FAULT];
            }
            else if (output.Contains("already several accounts on the device"))
            {
                msg = text[MSG_OTH_ACC];
            }
            else if (output.Contains("already several users on the device"))
            {
                msg = text[MSG_OTH_USER];
            }
            else if (output.Contains("nor current process has android.permission.MANAGE_DEVICE_ADMINS"))
            {
                msg = text[MSG_MIUI_SEC];
            }
            else if (output.Contains("but device owner is already set"))
            {
                msg = text[MSG_OWNER_SETTED];
            }
            else if (exitCode == 127)
            {
                msg = text[MSG_DPM_NTF];
            }
            else if (output.Contains("unknown admin"))
            {
                msg = text[MSG_OWNER_NTF];
            }
            else
            {
                tip = text[TIP_MAY_FAILED];
                msg = text[MSG_MAYFAIL];
            }
        }
    }
}
