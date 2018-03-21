using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 设计中
    /// </summary>
    public interface IAutumnBoxExtension : IDisposable
    {
        string Name { get; }
        string Infomation { get; }
        object Icon { get; }
        bool RunCheck(RunCheckArgs args);
        bool Init(InitArgs args);
        void Run(StartArgs args);
        bool Stop(StopArgs args);
        void Destory(DestoryArgs args);
    }
}
