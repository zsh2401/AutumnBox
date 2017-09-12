/*
 @zsh2401 2017/9/6
 事件的处理方法
 */
using AutumnBox.Basic.Functions.ExecutedResultHandler;
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public class RMFinishEventArgs:EventArgs
    {
        ExecuteResult ExecuteResult;
    }
}