using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 只要是向设备发出命令实现功能的类，理应该实现此类
    /// </summary>
    public class DeviceCommander : DependOnDeviceObject, ICommandStationObject
    {
        /// <summary>
        /// 命令站点，你可以设置自己的命令站点，或监听此命令站点
        /// </summary>
        public CommandStation CmdStation
        {
            get
            {
                if (_commandStation == null)
                {
                    _commandStation = new CommandStation();
                }
                return _commandStation;
            }
            set
            {
                _commandStation = value;
            }
        }
        private CommandStation _commandStation;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public DeviceCommander(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 当接收到输出时发生
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 输出接收器
        /// </summary>
        /// <param name="e"></param>
        protected virtual void RaiseOutput(OutputReceivedEventArgs e)
        {
            callback?.Invoke(e);
            OutputReceived?.Invoke(this, e);
        }
        /// <summary>
        /// 输出接收器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void RaiseOutput(object sender,OutputReceivedEventArgs e)
        {
            callback?.Invoke(e);
            OutputReceived?.Invoke(this, e);
        }
        private Action<OutputReceivedEventArgs> callback;
        /// <summary>
        /// 通过To模式订阅输出
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        protected void RegisterToCallback(Action<OutputReceivedEventArgs> callback)
        {
            throw new NotImplementedException();
        }
    }
}
