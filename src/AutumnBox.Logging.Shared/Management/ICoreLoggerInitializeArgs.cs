/*

* ==============================================================================
*
* Filename: ICoreLoggerInitializeArgs
* Description: 
*
* Version: 1.0
* Created: 2020/4/26 1:28:26
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
    public interface ICoreLoggerInitializeArgs
    {
        FileInfo LogFile { get; }

        DirectoryInfo DirectoryInfo { get; }

        Action<string> Writer { get; }

        IEnumerable<ILog> PastLogs { get; }
    }
}
