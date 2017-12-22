/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/22 18:17:15
** filename: RefreshableCtrlFinder.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI
{
    public class CtrlFinder<TResult> where TResult : class
    {
        private readonly List<TResult> result;
        private readonly UIElementCollection source;
        public CtrlFinder(UIElementCollection source)
        {
            result = new List<TResult>();
            this.source = source;
        }
        public List<TResult> Find()
        {
            foreach (var ctrl in source)
            {
                if (ctrl is TResult)
                {
                    result.Add((TResult)ctrl);
                }
            }
            return result;
        }
    }
}
