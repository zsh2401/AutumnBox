using System;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 输入器
    /// </summary>
    public class Inputer : DeviceCommander
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="Exceptions.CommandNotFoundException"></exception>
        public Inputer(IDevice device) : base(device)
        {
            //ShellCommandHelper.CommandExistsCheck(device, "input");
        }
        /// <summary>
        /// 模拟按键
        /// </summary>
        /// <param name="keyCode">安卓键值,如果秋之盒中没有你需要的键值定义,可以进行强转: (AndroidKeyCode)233</param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void PressKey(AndroidKeyCode keyCode)
        {
            Executor.AdbShell(Device,
                  $"input keyevent {(int)keyCode}")
                  .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 模拟按键
        /// </summary>
        /// <param name="keycode"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void PressKey(int keycode)
        {
            Executor.AdbShell(Device,
                 $"input keyevent {keycode}")
                 .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 在目前的焦点文本框输入文本
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void InputText(string text)
        {
            Executor.AdbShell(Device,
                $"input text {text}")
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 模拟在屏幕上滑动
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void Swipe(int startX, int startY, int endX, int endY)
        {
            Executor.AdbShell(Device,
                $"input swipe {startX} {startY} {endX} {endY}")
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 点击屏幕上的位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void Tap(int x, int y)
        {
            Executor.AdbShell(Device,
                $"input tap {x} {y}")
                .ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}
