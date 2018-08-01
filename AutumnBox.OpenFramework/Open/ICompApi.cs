using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 兼容性API
    /// </summary>
    public interface ICompApi
    {
        /// <summary>
        /// 隔离执行某个函数
        /// </summary>
        /// <param name="act"></param>
        void IsolatedInvoke(Action act);
        /// <summary>
        /// 根据最低SDK隔离执行函数
        /// </summary>
        /// <param name="minSdk">可以执行的最低SDK</param>
        /// <param name="act">函数</param>
        void IsolatedInvoke(int minSdk, Action act);
        /// <summary>
        /// canRun为true时,隔离执行函数
        /// </summary>
        /// <param name="canRun">执行条件</param>
        /// <param name="act">函数</param>
        void IsolatedInvoke(bool canRun, Action act);
    }
}
