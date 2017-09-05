using System;
using System.Diagnostics;
using System.IO;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    internal static class Logger
    {
        public static readonly string LOG_FILE = "basic.log";
        public static void D(string tag, string message) {
#if DEBUG
            T(tag,message);
#endif
        }
        public static void T(string tag, string message) {
            string m= $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] { tag} : {message}";
            WriteToLogFile(m);
        }
        [Obsolete]
        /// <summary>
        /// 打印信息并且存储到log文件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="message">信息</param>
        public static void d(string tag,string message) {
            m(tag,message);
            WriteToLogFile($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] { tag} : {message}");
        }
        [Obsolete]
        /// <summary>
        /// 打印信息,但不存储到log文件
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void m(string tag, string message) {
            Debug.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] { tag} : {message}");
        }
        /// <summary>
        /// 将传入的信息写到log文件(增添的方式)
        /// </summary>
        /// <param name="content"></param>
        static void WriteToLogFile(string content) {
            try
            {
                StreamWriter sw = new StreamWriter(LOG_FILE, true);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch {

            }
        }
    }
}
