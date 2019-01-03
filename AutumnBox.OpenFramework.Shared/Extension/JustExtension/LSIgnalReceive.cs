using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.JustExtension
{
    /// <summary>
    /// 可以标记信号接收函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LSignalReceive : Attribute
    {
        public string Pattern { get; }

        public LSignalReceive(string pattern)
        {
            Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        }
        public LSignalReceive() : this("*")
        {
        }
    }
}
