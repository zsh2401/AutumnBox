using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.Container
{
    public static class LakeSourcesHelper
    {
        public static object Get(this IEnumerable<ILake> sources, string id)
        {
            for (int i = sources.Count(); i >= 0; i--)
            {
                if (sources.ElementAt(i).TryGet(id, out object value))
                {
                    return value;
                }
            }
            return null;
        }
        public static object Get(this IEnumerable<ILake> sources, Type t)
        {
            for (int i = sources.Count(); i >= 0; i--)
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
