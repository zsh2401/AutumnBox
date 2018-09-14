using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 模块可用的区域
    /// </summary>
    public class ExtRegionAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="regions"></param>
        public ExtRegionAttribute(params string[] regions) : base(regions)
        {
        }
        /// <summary>
        /// 默认，适应所有区域
        /// </summary>
        public ExtRegionAttribute() : base(null) { }
    }
}
