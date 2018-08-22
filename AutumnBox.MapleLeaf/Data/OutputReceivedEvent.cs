/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 2:26:38 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Data
{
    public delegate void OutputReceivedEventHandler(object sender, OutputReceivedEventArgs e);

    public class OutputReceivedEventArgs : EventArgs
    {
        public string Content { get; private set; }
        public bool IsError { get; private set; }
        public OutputReceivedEventArgs(string content, bool isErr = false)
        {
            this.Content = content;
            this.IsError = isErr;
        }
    }
}
