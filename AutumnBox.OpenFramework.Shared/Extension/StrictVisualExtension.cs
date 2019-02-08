/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/30 0:35:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 严格化拓展的诞生只有一个目标:让拓展模块可以被完美的停止
    /// </summary>
    [Obsolete]
    public abstract class StrictVisualExtension : AtmbVisualExtension
    {
        /// <summary>
        /// 命令分配站
        /// </summary>
        protected CommandStation CmdStation { get; private set; }
        /// <summary>
        /// 当创建时,严格化拓展在此处初始化了一些工厂类
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            InitLazyFactory();
            CmdStation = new CommandStation();
        }
        /// <summary>
        /// VisualStop,在此处,命令分配器将释放所有命令,并不可再创建新的命令
        /// </summary>
        /// <returns></returns>
        protected override bool VisualStop()
        {
            CmdStation.Free();
            CmdStation.Lock();
            return true;
        }

        #region Command Getter
        /// <summary>
        /// 获取托管的shell命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected ShellCommand GetDevcieShellCommand(string cmd)
        {
            return CmdStation.GetShellCommand(DeviceSelectedOnCreating, cmd);
        }
        /// <summary>
        /// 获取托管的adb命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected AdbCommand GetDeviceAdbCommand(string cmd)
        {
            return CmdStation.GetAdbCommand(DeviceSelectedOnCreating, cmd);
        }
        /// <summary>
        /// 获取托管的设备无关adb命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected AdbCommand GetNoDeviceAdbCommand(string cmd)
        {
           
            return CmdStation.GetAdbCommand(cmd);
        }
        /// <summary>
        /// 获取托管的fastboot命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected FastbootCommand GetDeviceFastbootCommand(string cmd)
        {
           
            return CmdStation.GetFastbootCommand(DeviceSelectedOnCreating, cmd);
        }
        /// <summary>
        /// 获取托管的设备无关fastboot命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected FastbootCommand GetNoDeviceFastbootCommand(string cmd)
        {
           
            return CmdStation.GetFastbootCommand(cmd);
        }
        /// <summary>
        /// 获取windows cmd命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected WindowsCmdCommand GetWindowsCmdCommnad(string cmd)
        {
           
            return CmdStation.GetCmdCommand(cmd);
        }
        #endregion

        #region Device Commander Getter
        /// <summary>
        /// 获得统一并被托管的设备命令执行器
        /// </summary>
        /// <typeparam name="TDevCommander"></typeparam>
        /// <returns></returns>
        protected TDevCommander GetDeviceCommander<TDevCommander>()
            where TDevCommander : DeviceCommander
        {
            return DevCmderFcty.GetDeviceCommander<TDevCommander>();
        }

        private DeviceCommanderFactory DevCmderFcty => _deviceCommanderFactory.Value;
        private Lazy<DeviceCommanderFactory> _deviceCommanderFactory;
        private void InitLazyFactory()
        {
            _deviceCommanderFactory = new Lazy<DeviceCommanderFactory>(() =>
            {
                return new DeviceCommanderFactory(DeviceSelectedOnCreating, CmdStation);
            });
        }
        #endregion
    }
}
