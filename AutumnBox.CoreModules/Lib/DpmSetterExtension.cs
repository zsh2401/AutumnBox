/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
namespace AutumnBox.CoreModules.Lib
{
    public abstract class DpmSetterExtension : OfficialVisualExtension
    {
        public abstract string ReceiverClassName { get; }
        public abstract string DpmAppPackageName { get; }
        protected GodPower GodPower { get; set; }
        protected virtual bool OnWarnUser()
        {
            string warnMsg = string.Format(Res("DPMWarningFmt"), Res("AppNameThis"));
            return Ux.Agree(warnMsg);
        }

        protected virtual int SetReciverAsDpm()
        {
            WriteLine("设置设备管理员");
            Tip = "设置设备管理员";
            TargetDevice
                .GetShellCommand($"dpm set-device-owner {DpmAppPackageName}/{ReceiverClassName}")
                .To(OutputPrinter)
                .Execute();
            return 0;
        }

        protected sealed override int VisualMain()
        {
            if (OnWarnUser())
            {
                return ERR;
            }
            WriteLine("初始化");
            Tip = "初始化";
            GodPower = new GodPower(this, TargetDevice);

            WriteLine("提取APK");
            Tip = "提取APK";
            GodPower.Extract();

            WriteLine("推送APK");
            Tip = "推送APK";
            GodPower.GetPushCommand().To(OutputPrinter).Execute();

            WriteLine("移除用户");
            Tip = "移除用户";
            GodPower.GetRemoveUserCommand().To(OutputPrinter).Execute();

            WriteLine("移除账号");
            Tip = "移除账号";

            GodPower.GetRemoveAccountCommnad().To(OutputPrinter).Execute();
            return SetReciverAsDpm();
        }
    }
}
