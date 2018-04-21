namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 可输出的
    /// 实现这个接口,将可以使用OutputBuilder.Register()方法来使OutputBuilder自动记录输出
    /// </summary>
    public interface IOutputable
    {
        /// <summary>
        /// 产生输出时发生的事件
        /// </summary>
        event OutputReceivedEventHandler OutputReceived;
    }
}
