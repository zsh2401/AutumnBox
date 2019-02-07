using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafUI相关拓展函数
    /// </summary>
    public static class LeafUIHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="LeafTerminatedException"></exception>
        /// <param name="ui"></param>
        /// <param name="exitCode"></param>
        public static void EFinish(this ILeafUI ui, int exitCode = 0)
        {
            ui.Finish(exitCode);
            throw new LeafTerminatedException(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="LeafTerminatedException"></exception>
        /// <param name="ui"></param>
        /// <param name="tip"></param>
        public static void EFinish(this ILeafUI ui, string tip)
        {
            ui.Finish(tip);
            throw new LeafTerminatedException(0);
        }
        /// <summary>
        /// Shutdown
        /// </summary>
        /// <param name="ui"></param>
        public static void EShutdown(this ILeafUI ui)
        {
            ui.Shutdown();
            throw new LeafTerminatedException(0);
        }
    }
}
