/*************************************************
** auth： zsh2401@163.com
** date:  2018/11/12 12:05:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Open;
using System;
using System.IO;

namespace AutumnBox.CoreModules.Lib
{
    internal class CstmDpmCommander : DeviceCommander, IReceiveOutputByTo<CstmDpmCommander>
    {
        public const string PATH_OF_EMB_APK = "Res.dpmpro";
        public const string PATH_OF_TMP_APK = "dpmpro";
        public const string PATH_OF_COMMAND_CLASS = "com.android.commands.dpm.Dpm";
        public const string PATH_OF_ATMP_APK = "/data/local/tmp/dpmpro";
        public const string CMD_REMOVE_USER = "remove-all-users";
        public const string CMD_REMOVE_ACC = "remove-all-accounts";
        public const string CMD_SET_DEVICE_OWNER = "set-device-owner";
        public const string CMD_FORMAT =
            "CLASSPATH=" + PATH_OF_ATMP_APK + " app_process /system/bin "
            + PATH_OF_COMMAND_CLASS + " {0}";
        public const int OKAY = 0;
        public const int ERR = 1;
        public const int ERR_EXIST_OTHER_USER = 0b100;
        public const int ERR_EXIST_OTHER_ACC = 0b1000;
        public const int ERR_MIUI_SEC = 0b10000;
        private readonly Context context;

        public CstmDpmCommander(Context context, IDevice device) : base(device)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CstmDpmCommander To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
        public void Extract()
        {
            DirectoryInfo dirInfo = context.Tmp.DirInfo;
            string path = Path.Combine(dirInfo.FullName, PATH_OF_TMP_APK);
            IEmbeddedFile embFile = context.EmbeddedManager.Get(PATH_OF_EMB_APK);
            using (FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                embFile.WriteTo(fs);
            }
        }
        public string ShowUsage()
        {
            string command = string.Format(CMD_FORMAT, "");
            return CmdStation
                  .GetShellCommand(Device, command)
                  .To(RaiseOutput)
                  .Execute()
                  .ThrowIfExitCodeNotEqualsZero()
                  .Output.ToString();
        }
        public void PushToDevice()
        {
            DirectoryInfo dirInfo = context.Tmp.DirInfo;
            string path = Path.Combine(dirInfo.FullName, PATH_OF_TMP_APK);
            CmdStation
                .GetAdbCommand($"push \"{path}\" {PATH_OF_ATMP_APK}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
        public void RemoveUsers()
        {
            string command = string.Format(CMD_FORMAT, CMD_REMOVE_USER);
            CmdStation
                .GetShellCommand(Device, command)
                .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
        public void RemoveAccounts()
        {
            string command = string.Format(CMD_FORMAT, CMD_REMOVE_ACC);
            CmdStation
                .GetShellCommand(Device, command)
                .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
        public void SetDeviceOwner(string componentName)
        {
            string setDeviceOwnerArg = $"{CMD_SET_DEVICE_OWNER} {componentName}";
            string command = string.Format(CMD_FORMAT, setDeviceOwnerArg);
            CmdStation
                .GetShellCommand(Device, command)
                .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
