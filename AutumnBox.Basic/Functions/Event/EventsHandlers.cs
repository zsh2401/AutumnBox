/*
 @zsh2401 2017/9/6
 事件的处理方法
 */
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public delegate void StartEventHandler(object sender, StartEventArgs e = null);
    public delegate void FinishEventHandler(object sender,FinishEventArgs e=null);
    public delegate void SimpleFinishEventHandler();
    [Obsolete("将在未来版本移除")]
    public delegate void RunningManagerFinishHandler(object sender,RMFinishEventArgs e);
}
