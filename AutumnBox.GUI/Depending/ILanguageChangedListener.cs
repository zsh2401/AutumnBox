/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 13:30:37 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.GUI.Depending
{
    public class LangChangedEventArgs : EventArgs
    {
        public string NewLanguageCode { get; set; }
    }
    interface ILanguageChangedListener
    {
        void OnLanguageChanged(LangChangedEventArgs args);
    }
}
