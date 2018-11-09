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
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.CoreModules.Lib
{
    [ExtDesc("使用奇淫技巧暴力设置设备管理员,\n注意:使用此模块前,必须先移除屏幕锁,指纹锁等,否则将可能导致不可预见的后果", "en-us:Use the sneaky skills to set up the device administrator, \n Note: Before using this module, you must first remove the screen lock, fingerprint lock, etc., otherwise it may lead to unforeseen consequences")]
    [UserAgree("EGodPowerWarning")]
    [ExtIcon("Icons.nuclear.png")]
    [DpmReceiver(null)]
    internal abstract class EDpmSetterBase : OfficialVisualExtension
    {
        protected GodPower GodPower { get; set; }
        private string _cn;

        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            ClassExtensionScanner scanner = new ClassExtensionScanner(GetType());
            scanner.Scan(ClassExtensionScanner.ScanOption.Informations);
            var infos = scanner.Informations;
            _cn = infos[DpmReceiverAttribute.KEY].Value as string;
        }
        protected sealed override int VisualMain()
        {
            ProcessBasedCommand command = null;
            IProcessBasedCommandResult result = null;
            WriteInitInfo();
            GodPower = new GodPower(this, TargetDevice);

            WriteLineAndSetTip(Res("EGodPowerExtractingApk"));
            GodPower.Extract();
            ThrowIfCanceled();

            WriteLineAndSetTip(Res("EGodPowerPushingApk"));
            command = GodPower.GetPushCommand();
            CmdStation.Register(command);
            result = command
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();

            WriteLineAndSetTip(Res("EGodPowerRmUser"));
            command = GodPower.GetRemoveUserCommand();
            CmdStation.Register(command);
            result = command
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();

            WriteLineAndSetTip(Res("EGodPowerRmAcc"));
            command = GodPower.GetRemoveAccountCommnad();
            CmdStation.Register(command);
            result = command
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();

            return SetDpm();
        }
        protected virtual int SetDpm()
        {
            WriteLineAndSetTip(Res("DPMSetting"));
            DevicePolicyManager dpm = GetDeviceCommander<DevicePolicyManager>();
            ThrowIfCanceled();
            try
            {
                dpm.SetDeviceOwner(_cn);
                return 0;
            }
            catch (AdbShellCommandFailedException ex)
            {
                WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
