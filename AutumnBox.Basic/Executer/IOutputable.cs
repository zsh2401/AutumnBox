namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 可输出的
    /// 实现这个接口,将可以使用OutputBuilder.Register()方法来使OutputBuilder自动记录输出
    /// </summary>
    public interface IOutputable
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        event OutputReceivedEventHandler OutputReceived;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
