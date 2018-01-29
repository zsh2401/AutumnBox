/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/29 8:39:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    public class ACPCommand
    {
        private string baseCommand = null;
        private string[] args = new string[0];
        public override string ToString()
        {
            if (baseCommand == null) throw new NullReferenceException();
            StringBuilder args_str = new StringBuilder();
            foreach (String arg in args) {
                args_str.Append(arg + " ");
            }
            return baseCommand + " " + args_str.ToString();
        }
        public byte[] ToBytes() {
            Logger.D("ToBytes(): the command string: " + ToString());
            return Encoding.UTF8.GetBytes(ToString());
        }
        public class Builder {
            public String BaseCommand { get; set; }
            public string[] Args { get; set; } = new string[0];
            public void SetArgs(params string[] args) {
                Args = args;
            }

            public ACPCommand ToCommand() {
                if (BaseCommand == null) throw new NullReferenceException();
                if (Args == null) throw new NullReferenceException();
                return new ACPCommand()
                {
                    baseCommand = BaseCommand,
                    args = Args
                };
            }
            public override string ToString()
            {
                return ToCommand().ToString();
            }
        }
    }
}
