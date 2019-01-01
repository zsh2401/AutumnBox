/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    [ExtDeveloperMode(true)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Sideload)]
    //[ExtDesc("wtf")]
    //[ObsoleteImageOperator]
    //[ExtAppProperty("com.miui.calculatorx")]
    [UserAgree("Please be true")]
    internal class EHoldMyHand : SharpExtension
    {
        protected override void Processing(Dictionary<string, object> data)
        {
            Ux.ShowLoadingWindow();
            Ux.Message(Executor.Cmd("ping www.baidu.com").Output.ToString());
            Ux.CloseLoadingWindow();
            throw new Exception();
        }
    }
}
