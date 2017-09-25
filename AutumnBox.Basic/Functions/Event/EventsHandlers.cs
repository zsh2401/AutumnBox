/*
 @zsh2401 2017/9/6
 事件的处理方法
 */
namespace AutumnBox.Basic.Functions.Event
{
    public delegate void StartEventHandler(object sender, StartEventArgs e);
    public delegate void FinishEventHandler(object sender,FinishEventArgs e);
    public delegate void SimpleFinishEventHandler();
}
