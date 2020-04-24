/*

* ==============================================================================
*
* Filename: ELogMonitor
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 16:49:32
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.Essentials.XCards;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Essentials.Extensions
{
    [ExtName("Logs Monitor", "zh-CN:日志监视器")]
    class ELogMonitor : LeafExtensionBase
    {
        private static LogMonitorView viewInstance;
        [LMain]
        public void EntryPoint(LogMonitorView view, IXCardsManager xCardsManager)
        {
            if (viewInstance == null)
            {
                viewInstance = view;
                xCardsManager.Register(view);
            }
            else
            {
                xCardsManager.Unregister(viewInstance);
                viewInstance = null;
            }
        }
    }
}
