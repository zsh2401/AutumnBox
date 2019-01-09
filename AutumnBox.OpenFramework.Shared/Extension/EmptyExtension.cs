namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 继承此类可以获得默认的信息,提高兼容性
    /// </summary>
    [ExtName("无名拓展", "en-us:Unknown extension")]
    [ExtAuth("佚名", "en-us:Anonymous")]
    [ExtDesc(null)]
    [ExtVersion()]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtMinApi(8)]
    [ExtTargetApi()]
    [ExtRunAsAdmin(false)]
    [ExtRequireRoot(false)]
    [ExtOfficial(false)]
    [ExtRegions(null)]
    [ExtPriority(ExtPriority.NORMAL)]
    //[ExtDeveloperMode]
    //[ExtAppProperty("com.miui.fm")]
    //[ExtMinAndroidVersion(7,0,0)]
    public class EmptyExtension
    {
    }
}
