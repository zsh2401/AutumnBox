namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 继承此类可以获得默认的信息,提高兼容性
    /// </summary>
    [ExtName("无名拓展", "en-us:Unknown extension")]
    [ExtAuth("佚名", "en-us:Anonymous")]
    [ExtDesc(null)]
    [ExtVersion(0, 0, 1)]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtMinApi(8)]
    [ExtTargetApi]
    [ExtRegions(null)]
    [ExtPriority(ExtPriority.NORMAL)]
    [ExtDeveloperMode(false)]
    [ExtHidden(false)]
    public class EmptyExtension
    {
    }
}
