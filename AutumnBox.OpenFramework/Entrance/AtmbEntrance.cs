/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 3:34:12 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.OpenFramework.Entrance
{
    /// <summary>
    /// 当程序集没有入口类时,将使用的默认入口类
    /// </summary>
    public abstract class AtmbEntrance : BaseEntrance
    {
        /// <summary>
        /// 初始化构造器
        /// </summary>
        public AtmbEntrance()
        {
            Init(GetType().Assembly);
        }
        /// <summary>
        /// 根据程序集进行初始化
        /// </summary>
        /// <param name="assembly"></param>
        protected void Init(Assembly assembly)
        {
            ManagedAssembly = GetType().Assembly;
            Reload();
        }
    }
}
