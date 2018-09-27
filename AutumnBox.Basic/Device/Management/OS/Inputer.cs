using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 输入器
    /// </summary>
    public class Inputer : DeviceCommander, IReceiveOutputByTo<Inputer>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public Inputer(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 模拟按键
        /// </summary>
        /// <param name="keycode"></param>
        public void PressKey(int keycode)
        {
            CmdStation.GetShellCommand(Device,
                 $"input keyevent {keycode}")
                 .To(RaiseOutput)
                 .Execute()
                 .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 在目前的焦点文本框输入文本
        /// </summary>
        /// <param name="text"></param>
        public void InputText(string text)
        {
            CmdStation.GetShellCommand(Device,
                $"input text {text}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 模拟在屏幕上滑动
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        public void Swipe(int startX, int startY, int endX, int endY)
        {
            CmdStation.GetShellCommand(Device,
                $"input swipe {startX} {startY} {endX} {endY}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 点击屏幕上的位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Tap(int x, int y)
        {
            Swipe(x, y, x, y);
        }
        /// <summary>
        /// 通过To模式订阅
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Inputer To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
