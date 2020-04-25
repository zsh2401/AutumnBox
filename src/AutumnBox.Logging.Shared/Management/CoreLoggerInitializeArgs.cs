#nullable enable
/*

* ==============================================================================
*
* Filename: CoreLoggerInitializeArgs
* Description: 
*
* Version: 1.0
* Created: 2020/4/25 18:28:56
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

namespace AutumnBox.Logging.Management
{
    public class CoreLoggerInitializeArgs : ICoreLoggerInitializeArgs
    {
        public CoreLoggerInitializeArgs(FileInfo logFile, DirectoryInfo directoryInfo, Action<string> writer, IEnumerable<ILog> pastLogs)
        {
            LogFile = logFile ?? throw new ArgumentNullException(nameof(logFile));
            DirectoryInfo = directoryInfo ?? throw new ArgumentNullException(nameof(directoryInfo));
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
            PastLogs = pastLogs ?? throw new ArgumentNullException(nameof(pastLogs));
        }

        public FileInfo LogFile { get; }

        public DirectoryInfo DirectoryInfo { get; }

        public Action<string> Writer { get; }

        public IEnumerable<ILog> PastLogs { get; }
    }
}
