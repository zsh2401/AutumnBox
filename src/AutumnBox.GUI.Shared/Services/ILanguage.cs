/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:15:33 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Windows;

namespace AutumnBox.GUI.Services
{
    interface ILanguage : IEquatable<ILanguage>
    {
        string LanCode { get; }
        string LangName { get; }
        ResourceDictionary Resource { get; }
    }
}
