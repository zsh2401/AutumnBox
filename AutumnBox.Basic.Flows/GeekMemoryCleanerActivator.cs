/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/19 17:27:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// GeekMemoryCleaner的激活器,实际上就是简单的开启网络调试罢了
    /// </summary>
    public class GeekMemoryCleanerActivator : FunctionFlow
    {
        /// <summary>
        /// GMC的包名
        /// </summary>
        public const string AppPackageName = "com.ifreedomer.fuckmemory";
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            var result = new NetDebuggingOpener().Run(new NetDebuggingOpenerArgs()
            {
                Port = 5555,
                DevBasicInfo = toolKit.Args.DevBasicInfo
            });
            return result.OutputData;
        }
    }
}
