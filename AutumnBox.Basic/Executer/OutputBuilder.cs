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
            try
            {
                outSb.AppendLine(text);
                allSb.AppendLine(text);
                LeastLine = text;
            }
            catch (IndexOutOfRangeException)
            {
                //2017 11 21 01:00的一次调试中
                //我在显示关闭其它助手的提示后,关闭了360手机助手并点击了"我已关闭其它助手"
                //然后出现了RateBox,半秒后便出现了这个奇怪的IndexOutOfRangeException??
                //在StackOverFlow上搜寻后,有人说这是一个奇怪的BUG
                //既然如此...下次就抓住这个BUG吧...
            }
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
        public void Register(IOutSender sender)
        {
            sender.OutputReceived += (s, e) => {
                if (e.IsError)
                {
                    AppendError(e.Text);
                }
                else
                {
                    AppendOut(e.Text);
                }
            };
        }
        public string LeastLine { get; private set; }
        public Output ToOutputData()
        {
            return new Output(allSb.ToString(), outSb.ToString(), errSb.ToString());
        }
    }
}
