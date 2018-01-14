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
    public enum RebootOption
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
        public async static void RebootAsync(DeviceBasicInfo dev, RebootOption option = RebootOption.System, FinishedCallback callback = null)
        {
            await Task.Run(() =>
            {
                Reboot(dev, option);
            });
            callback?.Invoke();
        }
        public static void Reboot(DeviceBasicInfo dev, RebootOption option)
        {
            if (dev.State != DeviceState.Fastboot && (int)dev.State >= 1)
            {
                switch (option)
                {
                    case RebootOption.System:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot"));
                        break;
                    case RebootOption.Recovery:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot recovery"));
                        break;
                    case RebootOption.Fastboot:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot-bootloader"));
                        break;
                    case RebootOption.Snapdragon9008:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot edl"));
                        break;
                }
            }
            else if (dev.State == DeviceState.Fastboot)
            {
                switch (option)
                {
                    case RebootOption.System:
                        executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot"));
                        break;
                    case RebootOption.Fastboot:
                        executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot-bootloader"));
                        break;
                    case RebootOption.Snapdragon9008:
                        executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot-edl"));
                        break;
                }
            }
        }
        public static void Reboot(DeviceBasicInfo dev)
        {
            if (dev.State == DeviceState.Fastboot)
            {
                executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot"));
            }
            else
            {
                executer.Execute(Command.MakeForAdb(dev.Serial, "reboot"));
            }
        }
        public static void Reboot2Fastboot(DeviceBasicInfo dev)
        {
            if (dev.State == DeviceState.Fastboot)
            {
                executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot-bootloader"));
            }
            else
            {
                executer.Execute(Command.MakeForAdb(dev.Serial, "reboot-bootloader"));
            }
        }
        public static void Reboot2Recovery(DeviceBasicInfo dev)
        {
            if (dev.State != DeviceState.Fastboot)
            {
                executer.Execute(Command.MakeForAdb(dev.Serial, "reboot recovery"));
            }
        }
        public static void RebootTo9008(DeviceBasicInfo dev)
        {
            if (dev.State == DeviceState.Fastboot)
            {
                executer.Execute(Command.MakeForFastboot(dev.Serial, "reboot-edl"));
            }
            else
            {
                executer.Execute(Command.MakeForAdb(dev.Serial, "reboot edl"));
            }
        }
    }
}
