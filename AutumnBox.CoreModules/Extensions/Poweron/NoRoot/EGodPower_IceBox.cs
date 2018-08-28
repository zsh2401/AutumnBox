/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:48:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Flows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Extension;
using AutumnBox.Basic.Executer;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("免ROOT激活冰箱")]
    [ExtAppProperty("com.catchingnow.icebox")]
    [ExtIcon("Icons.icebox.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EGodPower_IceBox : OfficialVisualExtension
    {
        private const string FMT_CMD =
            "CLASSPATH=" + GODPOWER_APK_ON_DEVICE +
            " app_process /system/bin " + INNER_CLASS + " {0}";
        private const string INNER_CLASS = "com.web1n.myapplication.test";
        private const string GODPOWER_APK_ON_DEVICE = "/data/local/tmp/godpower.apk";
        private const string ARG_RM_ALL_ACC = "-removeAllAccounts";
        private const string ARG_RM_ALL_USR = "-removeAllUsers";
        private const string ARG_SET_ICE_BOX = "-setUpIcebox";
        private const string ARG_SET_STP_APP = "-setUpStopapp";
        protected override int VisualMain()
        {
            var warnMsg = string.Format(Res("DPMWarningFmt"), Res("AppNameIceBox"));
            if (!Ux.Agree(warnMsg))
            {
                return ERR;
            }
            WriteLine("正在提取apk文件");
            FileInfo godpowerApkFile = new FileInfo(Path.Combine(Tmp.Path, "godpower.apk"));
            EmbFileManager.Get("Res.godpower.apk").ExtractTo(godpowerApkFile);
            WriteLine("已提取到" + godpowerApkFile.FullName);

            WriteLine("正在推送冲锋兵");
            godpowerApkFile.PushTo(TargetDevice, GODPOWER_APK_ON_DEVICE);
            WriteLine("推送文件完成");

            AndroidShellV2 shell = new AndroidShellV2(TargetDevice);
            shell.OutputReceived += (s, e) =>
            {
                WriteLine(e.Text);
            };
            string command = null;
            WriteLine("正在移除账号");
            command = string.Format(FMT_CMD, ARG_RM_ALL_ACC);
            Logger.Info("executing shell command:" + command);
            shell.Execute(command);

            WriteLine("正在移除用户");
            command = string.Format(FMT_CMD, ARG_RM_ALL_USR);
            Logger.Info("executing shell command:" + command);
            shell.Execute(command);

            WriteLine("正在设置设备管理员");
            command = string.Format(FMT_CMD, ARG_SET_ICE_BOX);
            Logger.Info("executing shell command:" + command);

            int exitCode = shell.Execute(command).GetExitCode();
            if (exitCode != 0)
            {
                WriteLine("设置设备管理员时出错,可能你已经设置了别的APP为设备管理员?");
            }
            return exitCode;
        }
    }
}
