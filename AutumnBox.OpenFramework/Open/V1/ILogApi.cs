using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.V1
{
    /// <summary>
    /// 日志API
    /// </summary>
    public interface ILogApi
    {
        /// <summary>
        /// 打印一个日志
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="msg">内容</param>
        void Log(string tag, string msg);
    }
}
