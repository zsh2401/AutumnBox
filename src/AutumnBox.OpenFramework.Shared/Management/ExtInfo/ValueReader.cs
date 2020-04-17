#nullable enable
namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 值读取器,设计用于应对随时可能变化的值读取
    /// </summary>
    /// <returns></returns>
    public delegate object? ValueReader();
}
