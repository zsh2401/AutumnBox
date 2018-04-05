/************************
** auth： zsh2401@163.com
** date:  2018/1/15 1:48:42
** desc： ...
************************/
using AutumnBox.Basic.Executer;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 重启目标
    /// </summary>
    public enum RebootOptions
    {
        /// <summary>
        /// 重启到系统
        /// </summary>
        System,
        /// <summary>
        /// 重启到恢复模式
        /// </summary>
        Recovery,
        /// <summary>
        /// 重启到bootloader模式
        /// </summary>
        Fastboot,
        /// <summary>
        /// 高通9008模式
        /// </summary>
        Snapdragon9008,
        /// <summary>
        /// Sideload模式
        /// </summary>
        Sideload
    }
    /// <summary>
    /// 设备重启器
    /// </summary>
    public static class DeviceRebooter
    {
        private static CommandExecuter executer;
        static DeviceRebooter()
        {
            executer = new CommandExecuter();
        }
        /// <summary>
        /// 异步重启(如果ADB或手机卡到爆,那么这个函数就有作用了)
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="option"></param>
        /// <param name="callback"></param>
        public async static void RebootAsync(DeviceBasicInfo dev, RebootOptions option = RebootOptions.System, Action callback = null)
        {
            await Task.Run(() =>
            {
                Reboot(dev, option);
            });
            callback?.Invoke();
        }
        /// <summary>
        /// 重启手机
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="option"></param>
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
                    case RebootOptions.Sideload:
                        executer.Execute(Command.MakeForAdb(dev.Serial, "reboot sideload"));
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
