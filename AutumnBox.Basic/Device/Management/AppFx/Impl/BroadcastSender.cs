/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:35:07 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    public class BroadcastSender : DependOnDeviceObject, IBroadcastSender
    {
        public BroadcastSender(IDevice device) : base(device)
        {
        }

        public void Send(string broadcast)
        {

            new AdbCommandBuilder().Device(Device)
                .Shell()
                .Arg("am")
                .Arg("broadcast")
                .Arg("-a")
                .Arg(broadcast).ToCommand().Execute().ThrowIfExitCodeNotEqualsZero(); ;
        }
    }
}
