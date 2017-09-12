using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions.Interface
{
    /// <summary>
    /// 可获取实时输出的接口，实现此借口可以实时获取操作输出数据
    /// </summary>
    public interface ICanGetRealTimeOut
    {
        event DataReceivedEventHandler OutputDataReceived;
        event DataReceivedEventHandler ErrorDataReceived;
    }
}
