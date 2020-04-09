using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.Container
{
    /// <summary>
    /// LakeSources的帮助类
    /// </summary>
    public static class LakeSources
    {
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Get(this IEnumerable<ILake> sources, string id)
        {
            for (int i = sources.Count() - 1; i >= 0; i--)
            {
                if (sources.ElementAt(i).TryGet(id, out object value))
                {
                    return value;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据type获取
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object Get(this IEnumerable<ILake> sources, Type t)
        {
            for (int i = sources.Count() - 1; i >= 0; i--)
            {
                if (sources.ElementAt(i).TryGet(t, out object value))
                {
                    return value;
                }
            }
            return null;
        }
    }
}
