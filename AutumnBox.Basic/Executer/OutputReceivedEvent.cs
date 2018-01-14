/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 2:32:40
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public delegate void OutputReceivedEventHandler(object sender, OutputReceivedEventArgs e);
    public class OutputReceivedEventArgs : EventArgs
    {
        public bool IsError { get; private set; }
        public string Text { get; private set; }
        public DataReceivedEventArgs SourceArgs { get; private set; }
        public OutputReceivedEventArgs(string text, DataReceivedEventArgs source, bool isError = false)
        {
            Text = text;
            IsError = isError;
            SourceArgs = source;
        }
    }
    public class Dyc_I_Love_You { }
    public class CaoNa_I_Miss_You { }
    public class But_What_can_i_do { }
}
