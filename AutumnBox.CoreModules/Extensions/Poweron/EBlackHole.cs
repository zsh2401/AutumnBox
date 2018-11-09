/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 20:07:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("免ROOT一键激活黑洞", "en-us:Set Blackhole as DPM without root")]
    [ExtIcon("Icons.blackhole.png")]
    [ExtAppProperty(PKGNAME)]
    [DpmReceiver(RECEIVER_NAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EBlackHole : EDpmSetterBase
    {
        public const string PKGNAME = "com.hld.apurikakusu";
        public const string RECEIVER_NAME = "com.hld.apurikakusu/.receiver.DPMReceiver";
    }
}
