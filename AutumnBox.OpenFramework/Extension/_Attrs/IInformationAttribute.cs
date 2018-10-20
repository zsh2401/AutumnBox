using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 特性实现此接口,将会对拓展模块进行信息标记
    /// </summary>
    public interface IInformationAttribute
    {
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
        /// <summary>
        /// Value
        /// </summary>
        object Value { get; }
    }
}
