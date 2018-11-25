using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 只要实现此接口，均会被视为拓展模块
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// 接收信号
        /// </summary>
        /// <param name="signalName"></param>
        /// <param name="value"></param>
        void ReceiveSignal(string signalName, object value = null);
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        int Main(Dictionary<string, object> args);
    }
}
