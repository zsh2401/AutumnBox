using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 可输出的
    /// 实现这个接口,将可以使用OutputBuilder.Register()方法来使OutputBuilder自动记录输出
    /// </summary>
    public interface IOutputable : INotifyOutput
    {
    }
}
