using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("一键激活第二空间")]
    [ExtIcon("Icons.Anzenbokusu.png")]
    [ExtRegions("zh-CN")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtAppProperty(PKG_NAME)]
    [DpmReceiver(RECEIVER_NAME)]
    internal class EAnzenboksuActivator : EDpmSetterBase
    {
        public const string PKG_NAME = "com.hl.danzenbokusu";
        public const string RECEIVER_NAME = "com.hl.danzenbokusu/.receiver.DPMReceiver";
    }
}
