/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:39:18 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util.Custom
{
    interface ITheme
    {
        string Name { get; }
        ResourceDictionary Resource { get; }
    }
}
