/************************
** auth： zsh2401@163.com
** date:  2018/1/15 1:48:42
** desc： ...
************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public enum RebootOptions
    {
        System,
        Recovery,
        Fastboot,
        Snapdragon9008,
    }
    public static class DeviceRebooter
    {
        public delegate void FinishedCallback();
        private static CommandExecuter executer;
        static DeviceRebooter()
        {
            executer = new CommandExecuter();
        }
        public async static void RebootAsync(DeviceBasicInfo dev, RebootOptions option = RebootOptions.System, FinishedCallback callback = null)
        {
            await Task.Run(() =>
            {
                Reboot(dev, option);
            });
            callback?.Invoke();
        }
        public static void Reboot(DeviceBasicInfo dev, RebootOptions option)
        {
            if (dev.State != DeviceState.Fastboot && (int)dev.State >= 1)
            {
                switch (option)
                {
                    case RebootOptions.System:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot"));
                        break;
                    case RebootOptions.Recovery:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot recovery"));
                        break;
                    case RebootOptions.Fastboot:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot-bootloader"));
                        break;
                    case RebootOptions.Snapdragon9008:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot edl"));
                        break;
                }
            }
            else if (dev.State == DeviceState.Fastboot)
            {
                switch (option)
                {
                    case RebootOptions.System:
                        executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot"));
                        break;
                    case RebootOptions.Fastboot:
                        executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot-bootloader"));
                        break;
                    case RebootOptions.Snapdragon9008:
                        executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot-edl"));
                        break;
                }
            }
        }
    }
}
