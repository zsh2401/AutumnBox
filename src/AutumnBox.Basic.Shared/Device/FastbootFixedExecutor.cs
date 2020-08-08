/*

* ==============================================================================
*
* Filename: FastbootFixedExecutor
* Description: 
*
* Version: 1.0
* Created: 2020/8/9 3:00:55
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    class FastbootFixedExecutor
    {
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value <= 5 ? 5 : value;
            }
        }
        int _timeout = 1000;

        public int MaxTimes
        {
            get
            {
                return _maxTimes;
            }
            set
            {
                _maxTimes = value < 1 ? 1 : value;
            }
        }
        int _maxTimes = 10;
        const int INTERVAL = 50;
        const int WAIT_TIME = 500;
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        private readonly string command;

        public FastbootFixedExecutor(IDevice device, ICommandExecutor executor, string command)
        {
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (executor is null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentException("message", nameof(command));
            }

            this.device = device;
            this.executor = executor;
            this.command = command;
        }
        public CommandResult Execute()
        {
            CommandResult? result = null;
            for (int crtTime = 1; crtTime <= MaxTimes; crtTime++)
            {
                Task.Run(() => result = executor.Fastboot(device, command));
                Thread.Sleep(WAIT_TIME);
                if (result == null)
                {
                    executor.CancelCurrent();
                    Thread.Sleep(INTERVAL);
                }
                else
                {
                    break;
                }
            }
            return result ?? throw new CommandErrorException("Reachec limit of try.");
        }

        public Task<CommandResult> ExecuteAsync() => Task.Run(Execute);
    }
}
