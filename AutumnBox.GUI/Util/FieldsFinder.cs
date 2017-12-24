/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/24 23:43:44
** filename: FieldsFinder.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    public class FieldsFinder<TSource,TTarget>
        where TTarget : class
        where TSource : class
    {
        public IEnumerable<TTarget> FindFrom(TSource source)
        {
            Type stype = typeof(TSource);
            FieldInfo[] fields = stype.GetFields(BindingFlags.Public | BindingFlags.Instance);
            Logger.D($"find {fields.Count()} fields");
            foreach (FieldInfo info in fields) {
                Logger.D($"name {info.Name} type {info.FieldType.Name} value {info.GetValue(source)??"null"}");
            }
            var result = from field in fields
                         where field.FieldType == typeof(TTarget)
                         select (TTarget)field.GetValue(source);
            return result;
        }
    }
}
