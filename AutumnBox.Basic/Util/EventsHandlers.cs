/*
 @zsh2401 2017/9/6
 事件的处理方法
 */
namespace AutumnBox.Basic.Util
{
    public delegate void FinishEventHandler(object sender,FinishEventArgs e=null);
    public delegate void SimpleFinishEventHandler();
}
