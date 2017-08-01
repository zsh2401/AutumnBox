using System;
using System.Diagnostics;

namespace AutumnBox.Basic.DebugTools
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    internal class Log
    {
        /// <summary>
        /// 打印信息并且存储到log文件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="message">信息</param>
        public static void d(string tag,string message) {
            m(tag,message);
            WriteToLogFile($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] { tag} : {message}");
        }
        /// <summary>
        /// 打印信息,但不存储到log文件
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void m(string tag, string message) {
            Debug.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] { tag} : {message}");
        }
        /// <summary>
        /// 将传入的信息增添到log文件
        /// </summary>
        /// <param name="content"></param>
        static void WriteToLogFile(string content) {
            
        }
    }
}
