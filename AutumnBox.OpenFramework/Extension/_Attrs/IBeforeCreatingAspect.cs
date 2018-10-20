using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 创建实例前的切面,特性实现此类将可实现简单的AOP编程
    /// </summary>
    public interface IBeforeCreatingAspect
    {
        /// <summary>
        /// 具体实现
        /// </summary>
        /// <param name="args"></param>
        /// <param name="canContinue"></param>
        void BeforeCreating(BeforeCreatingAspectArgs args,ref bool canContinue);
    }
}
