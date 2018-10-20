/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 20:07:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT一键激活黑洞")]
    //[ExtName("Set Blackhole as DPM without root", Lang = "en-us")]
    [ExtIcon("Icons.blackhole.png")]
    [ExtAppProperty(PKGNAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EBlackHole : DpmSetterExtension
    {
        public const string PKGNAME = "com.hld.apurikakusu";
        public const string CLASSNAME = "receiver.DPMReceiver";
        protected override ComponentName ReceiverName
        {
            get
            {
                return ComponentName
                    .FromSimplifiedClassName(PKGNAME, CLASSNAME);
            }
        }
    }
}
