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
    public class ExtRegionsAttribute : SingleInfoAttribute
    {
        public override string Key => ExtensionInformationKeys.REGIONS;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="regions"></param>
        public ExtRegionsAttribute(params string[] regions) : base(regions)
        {
        }
        /// <summary>
        /// 默认，适应所有区域
        /// </summary>
        public ExtRegionsAttribute() : base(null) { }
    }
}
