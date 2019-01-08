using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafExtension的依赖注入器
    /// </summary>
    internal static class LeafManager
    {
        /// <summary>
        /// 获取新的Context
        /// </summary>
        /// <param name="leafExt"></param>
        /// <returns></returns>
        public static Context NewContext(this LeafExtensionBase leafExt)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// LeafContext
        /// </summary>
        private class LeafContext : Context { }
    }
}
