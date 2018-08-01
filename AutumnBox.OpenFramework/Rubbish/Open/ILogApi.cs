using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 日志API
    /// </summary>
    public interface ILogApi
    {
        /// <summary>
        /// 打印一个前缀为DEBUG的日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        void Debug(Context sender, string msg);
        /// <summary>
        /// 打印一个前缀为INFO的日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        void Info(Context sender, string msg);
        /// <summary>
        /// 打印一个警告日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        void Warn(Context sender, string msg);
        /// <summary>
        /// 打印一个错误日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        void Warn(Context sender, string msg,Exception e);
    }
}
