/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 23:45:40
** filename: BuildPropKeys.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// build.prop中常用的key
    /// </summary>
    public static class BuildPropKeys
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public const string Board = "ro.product.board";
        public const string ProductName = "ro.product.name";
        public const string Model = "ro.product.model";
        public const string Brand = "ro.product.brand";
        public const string SdkVersion = "ro.build.version.sdk";
        public const string AndroidVersion = "ro.build.version.release";
        public const string NavigationBar = "qemu.hw.mainkeys";
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
