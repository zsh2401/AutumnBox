using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.Running;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 锋利可控的拓展模块
    /// </summary>
    public abstract class SharpExtension : AutumnBoxExtension
    {
        /// <summary>
        /// 执行器
        /// </summary>
        protected CommandExecutor Executor { get; private set; }
        /// <summary>
        /// 返回码
        /// </summary>
        protected int ExitCode { get; set; } = (int)ExtensionExitCodes.Ok;
        /// <summary>
        /// 当创建时使用,SharpExtension将会在此构建Executor
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCreate(ExtensionArgs args)
        {
            Executor = new CommandExecutor();
            base.OnCreate(args);
        }
        /// <summary>
        /// 呵呵哒的main方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public sealed override int Main(Dictionary<string, object> data)
        {
            Processing(data);
            return ExitCode;
        }
        /// <summary>
        /// SharpExtension的主要方法,最大的特点是无需在意返回码
        /// </summary>
        /// <param name="data"></param>
        protected abstract void Processing(Dictionary<string, object> data);
        /// <summary>
        /// 当摧毁时调用,SharpExtension将会在此摧毁Executor
        /// </summary>
        /// <param name="args"></param>
        protected override void OnDestory(object args)
        {
            base.OnDestory(args);
            try
            {
                Executor.Dispose();
                Executor = null;
            }
            catch (Exception e)
            {
                Logger.Warn("Can not dispose CommandExecutor", e);
            }
        }
    }
}
