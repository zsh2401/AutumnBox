/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/7 1:20:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 设备命令器分发工厂
    /// </summary>
    public sealed class DeviceCommanderFactory
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="device"></param>
        /// <param name="commandStation"></param>
        public DeviceCommanderFactory(IDevice device,CommandStation commandStation) {
            if (commandStation == null)
            {
                throw new ArgumentNullException(nameof(commandStation));
            }

            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.commandStation = commandStation;
        }
        /// <summary>
        /// 获取一个设备命令器对象
        /// </summary>
        /// <typeparam name="TDevCommander"></typeparam>
        /// <returns></returns>
        public TDevCommander GetDeviceCommander<TDevCommander>()
    where TDevCommander : DeviceCommander
        {
            return (TDevCommander)FindOrCreateCommander(typeof(TDevCommander));
        }

        private DeviceCommander FindOrCreateCommander(Type type)
        {
            if (Commanders.TryGetValue(type, out DeviceCommander value))
            {
                return value;
            }
            object instance = Activator.CreateInstance(type, device);
            DeviceCommander devCommander = (DeviceCommander)instance;
            devCommander.CmdStation = commandStation;
            Commanders.Add(type, devCommander);
            return FindOrCreateCommander(type);
        }
        private Dictionary<Type, DeviceCommander> Commanders
        {
            get
            {
                if (_devCmders == null)
                {
                    _devCmders = new Dictionary<Type, DeviceCommander>();
                }
                return _devCmders;
            }
        }
        private Dictionary<Type, DeviceCommander> _devCmders;
        private readonly IDevice device;
        private readonly CommandStation commandStation;
    }
}
