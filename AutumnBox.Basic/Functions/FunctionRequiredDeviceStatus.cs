namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 方法执行需要的手机的状态?
    /// </summary>
    public enum FunctionRequiredDeviceStatus
    {
        All = 0,
        RunningOrRecovery,
        Running,
        Recovery,
        Fastboot ,
        Sideload,
    }
}
