/*

* ==============================================================================
*
* Filename: AsyncFileLogger
* Description: 
*
* Version: 1.0
* Created: 2020/5/6 1:23:27
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
#nullable enable
namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 表示异步日志文件记录器
    /// </summary>
    public class AsyncFileLogger : CoreLoggerBase
    {
        private readonly FileStream fs;
        private readonly StreamWriter sw;

        /// <summary>
        /// 指示日志文件的存放文件夹
        /// </summary>
        public static DirectoryInfo LogsDirectory
        {
            get
            {
                _logsDirectory ??= new DirectoryInfo("logs");
                if (!_logsDirectory.Exists)
                {
                    _logsDirectory.Create();
                }
                return _logsDirectory;
            }
        }
        private static DirectoryInfo? _logsDirectory;

        /// <summary>
        /// 初始化新的异步日志文件记录器
        /// </summary>
        public AsyncFileLogger(FileStream _fs)
        {
            fs = _fs;
            sw = new StreamWriter(fs)
            {
                AutoFlush = true
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="log"></param>
        public override void Log(ILog log)
        {
            Task.Run(() =>
            {
                lock (sw)
                {
                    sw.WriteLine(log);
                }
            });
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sw.Flush();
                fs.Flush();
                sw.Dispose();
                fs.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
