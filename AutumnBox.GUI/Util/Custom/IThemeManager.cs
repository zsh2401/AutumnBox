/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:38:36 (UTC +8:00)
** desc： ...
*************************************************/
using System.Collections.Generic;

namespace AutumnBox.GUI.Util.Custom
{
    interface IThemeManager
    {
        ITheme Current { get; set; }
        IEnumerable<ITheme> Themes { get; }
        void ApplyBySetting();
    }
}
