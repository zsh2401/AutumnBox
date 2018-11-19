/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:39:18 (UTC +8:00)
** desc： ...
*************************************************/
using System.Windows;

namespace AutumnBox.GUI.Util.Custom
{
    interface ITheme
    {
        string Name { get; }
        ResourceDictionary Resource { get; }
    }
}
