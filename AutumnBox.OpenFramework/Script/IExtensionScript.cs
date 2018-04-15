using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 标准拓展脚本接口
    /// </summary>
    public interface IExtensionScript:IExtension,IDisposable
    {
        string Name { get; }
        string Desc { get; }
        string Auth { get; }
        Version Version { get; }
        string ContactInfo { get; }
    }
}
