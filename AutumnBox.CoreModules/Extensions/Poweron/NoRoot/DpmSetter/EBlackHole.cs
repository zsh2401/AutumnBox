/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 20:07:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT一键激活黑洞")]
    [ExtName("Set Blackhole as DPM without root", Lang = "en-us")]
    [ExtIcon("Icons.blackhole.png")]
    [ExtAppProperty(PKG_NAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EBlackHole : DpmSetterExtension
    {
        public const string PKG_NAME = "com.hld.apurikakusu";
        public override string ReceiverClassName => throw new NotImplementedException();

        public override string DpmAppPackageName => throw new NotImplementedException();
    }
}
