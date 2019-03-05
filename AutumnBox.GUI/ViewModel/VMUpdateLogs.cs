using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMUpdateLogs : ViewModelBase
    {
        public IEnumerable<VersionInfo> Versions
        {
            get => _vers; set
            {
                _vers = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<VersionInfo> _vers;

        public VMUpdateLogs()
        {
            Versions = data;
        }

        private static readonly IEnumerable<VersionInfo> data = new List<VersionInfo>()
        {
            new VersionInfo("2019.3.5","2019-3-5","秋之盒三月更新来袭,诸多新特性,全新API,界面设计优化"),
            new VersionInfo("2019.1.18","2019-1-18","紧急修复一个臭名昭著的恶性BUG"),
            new VersionInfo("2019.1.11","2019-1-11","强烈建议更新,这个版本将是将来所有拓展模块的基础版本\n优化绿守激活器\n修复大量BUG"),
            new VersionInfo("2019.1.2","2019-1-2","修复了正式版中的网络调试BUG,这是个紧急版本"),

    };
    }
}
