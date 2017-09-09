/*
 @zsh2401 2017/9/9
 事件的处理方法
 */
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public class StartEventArgs:EventArgs
    {
        public IArgs Args { get; set; }
    }
}