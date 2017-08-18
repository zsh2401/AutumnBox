using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Arg
{
    public interface IArgs {}
    /// <summary>
    /// 一个多用于的Args
    /// </summary>
    public struct Args:IArgs
    {
        public string deviceID;
    }
    public struct FileArgs:IArgs {
        public string deviceID;
        public string[] files;
    }
    public struct RebootArgs : IArgs {
        public string deviceID;
        public RebootOptions rebootOption;
        public DeviceStatus nowStatus; 
    }
}
