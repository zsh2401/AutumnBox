namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 第二空间伪装版激活器
    /// </summary>
    public class AnzenbokusuFakeActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 第二空间伪装版包名
        /// </summary>
        public const string AppPackageName = "com.hld.anzenbokusufake";
        /// <summary>
        /// 第二空间伪装版包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 接收器类名
        /// </summary>
        protected override string ClassName => ".receiver.DPMReceiver";
    }
}
