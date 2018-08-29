/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:29:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.DPCommand
{
    public class SocketBasedCommand : IAsyncCommand
    {
        private class Result : ICommandResult
        {
            public Output Output { get; set; }

            public int ExitCode { get; set; }
        }
        public event OutputReceivedEventHandler OutputReceived
        {
            add
            {

            }
            remove
            {

            }
        }
        private string request;
        private AdbClientWarpper adbClient;
        private bool receiveDataBodyAfterOkay = true;

        public void Cancel()
        {
            adbClient.Dispose();
        }

        public void Dispose()
        {
            adbClient?.Dispose();
            adbClient = null;
        }

        public ICommandResult Execute()
        {
            var response = adbClient.SendRequest(request, receiveDataBodyAfterOkay);
            return new Result()
            {
                ExitCode = response.IsOkay ? 0 : 1,
                Output = new Output(response.DataAsString(), response.DataAsString()),
            };
        }

        public Task<ICommandResult> ExecuteAsync()
        {
            return Task.Run(() =>
            {
                return Execute();
            });
        }
    }
}
