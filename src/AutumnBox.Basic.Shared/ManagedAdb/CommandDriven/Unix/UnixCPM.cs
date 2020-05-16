/*

* ==============================================================================
*
* Filename: CPM
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 21:21:44
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven.Unix
{
    /// <summary>
    /// Unix命令事务管理器,未实现
    /// </summary>
    public class UnixCPM : ICommandProcedureManager
    {
        /// <inheritdoc/>
        public bool DisposedValue => throw new NotImplementedException();
        /// <inheritdoc/>
        public event EventHandler Disposed;
        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc/>
        public ICommandProcedure OpenCommand(string commandName, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
