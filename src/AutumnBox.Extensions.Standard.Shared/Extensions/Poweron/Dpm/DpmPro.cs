/*************************************************
** auth： zsh2401@163.com
** date:  2018/11/12 12:05:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open;
using System;
using System.IO;
using System.Reflection;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    internal class DpmPro
    {
        public const string PATH_OF_EMB_APK = "Resources.dpmpro.file";
        public const string PATH_OF_TMP_APK = "dpmpro";
        public const string PATH_OF_COMMAND_CLASS = "com.android.commands.dpm.Dpm";
        public const string PATH_OF_ATMP_APK = "/data/local/tmp/dpmpro";
        public const string CMD_REMOVE_USER = "remove-all-users";
        public const string CMD_REMOVE_ACC = "remove-all-accounts";
        public const string CMD_SET_DEVICE_OWNER = "set-device-owner";
        public const string CMD_FORMAT =
            "CLASSPATH=" + PATH_OF_ATMP_APK + " app_process /system/bin "
            + PATH_OF_COMMAND_CLASS + " {0}";

        private readonly ICommandExecutor executor;
        private readonly IEmbeddedFileManager emb;
        private readonly IStorage storage;
        private readonly IDevice device;

        public DpmPro(ICommandExecutor executor, IEmbeddedFileManager emb, IStorage storage, IDevice device)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
            this.emb = emb ?? throw new ArgumentNullException(nameof(emb));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
            this.device = device ?? throw new ArgumentNullException(nameof(device));
        }
        public void Extract()
        {
            DirectoryInfo dirInfo = storage.CacheDirectory;
            string path = Path.Combine(dirInfo.FullName, PATH_OF_TMP_APK);
            IEmbeddedFile embFile = emb.Get(Assembly.GetExecutingAssembly(), PATH_OF_EMB_APK);
            using FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            embFile.WriteTo(fs);
        }
        public int PushToDevice()
        {
            DirectoryInfo dirInfo = storage.CacheDirectory;
            string path = Path.Combine(dirInfo.FullName, PATH_OF_TMP_APK);
            string command = $"push \"{path}\" {PATH_OF_ATMP_APK}";
            return executor.Adb(device, command).ExitCode;
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
        public CommandResult SetDeviceOwner(string componentName)
        {
            string setDeviceOwnerArg = $"{CMD_SET_DEVICE_OWNER} {componentName}";
            string command = string.Format(CMD_FORMAT, setDeviceOwnerArg);
            return executor.AdbShell(device, command);
        }
    }
}
