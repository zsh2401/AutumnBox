namespace AutumnBox.OpenFramework.Wrapper
{
    /// <summary>
    /// 拓展模块进程
    /// </summary>
    public interface IExtensionProcess
    {
        /// <summary>
        /// 推出码
        /// </summary>
        int ExitCode { get; }
        /// <summary>
        /// 开始
        /// </summary>
        void Start();
        /// <summary>
        /// 阻断到其停止
        /// </summary>
        /// <returns></returns>
        int WaitForExit();
        /// <summary>
        /// 杀死进程
        /// </summary>
        void Kill();
    }
}
