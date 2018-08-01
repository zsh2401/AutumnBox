using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework
{
    /// <summary>
    /// 可重新加载的
    /// </summary>
    public interface IReloadable
    {
        /// <summary>
        /// 重新加载
        /// </summary>
        void Reload();
    }
}
