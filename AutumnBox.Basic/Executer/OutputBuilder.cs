/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 17:44:42 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public class OutputBuilder
    {
        private StringBuilder outSb;
        private StringBuilder errSb;
        private StringBuilder allSb;
        public OutputBuilder()
        {
            outSb = new StringBuilder();
            errSb = new StringBuilder();
            allSb = new StringBuilder();
        }
        public void AppendOut(string text)
        {
            outSb.AppendLine(text);
            allSb.AppendLine(text);
            LeastLine = text;
        }
        public void AppendError(string text)
        {
            errSb.AppendLine(text);
            allSb.AppendLine(text);
            LeastLine = text;
        }
        public void Clear()
        {
            outSb.Clear();
            errSb.Clear();
            allSb.Clear();
        }
        public void Register(IOutputable sender)
        {
            sender.OutputReceived += Sender_OutputReceived;
        }
        public void Unregister(IOutputable sender)
        {
            sender.OutputReceived -= Sender_OutputReceived;
        }
        private void Sender_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (e.IsError)
            {
                AppendError(e.Text);
            }
            else
            {
                AppendOut(e.Text);
            }
        }
        public string LeastLine { get; private set; }
        public Output ToOutput()
        {
            return new Output(allSb.ToString(), outSb.ToString(), errSb.ToString());
        }
        public Output Result
        {
            get
            {
                return ToOutput();
            }
        }
    }
}
