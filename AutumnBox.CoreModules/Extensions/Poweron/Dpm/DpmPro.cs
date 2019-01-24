/*************************************************
** auth： zsh2401@163.com
** date:  2018/11/12 12:05:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Open;
using System;
using System.IO;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    internal class DpmPro
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

        private readonly CommandExecutor executor;
        private readonly Context context;
        private readonly IDevice device;

        public DpmPro(CommandExecutor executor,Context context, IDevice device) 
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.device = device ?? throw new ArgumentNullException(nameof(device));
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
        public int PushToDevice()
        {
            DirectoryInfo dirInfo = context.Tmp.DirInfo;
            string path = Path.Combine(dirInfo.FullName, PATH_OF_TMP_APK);
            string command = $"push \"{path}\" {PATH_OF_ATMP_APK}";
            return executor.Adb(device, command).ExitCode ;
        }
        public int RemoveUsers()
        {
            string command = string.Format(CMD_FORMAT, CMD_REMOVE_USER);
            return executor.AdbShell(device, command).ExitCode;
        }
        public int RemoveAccounts()
        {
            string command = string.Format(CMD_FORMAT, CMD_REMOVE_ACC);
            return executor.AdbShell(device, command).ExitCode;
        }
        public CommandExecutor.Result SetDeviceOwner(string componentName)
        {
            string setDeviceOwnerArg = $"{CMD_SET_DEVICE_OWNER} {componentName}";
            string command = string.Format(CMD_FORMAT, setDeviceOwnerArg);
            return executor.AdbShell(device, command);
        }
    }
}
