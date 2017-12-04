/* =============================================================================*\
*
* Filename: BoxHelper
* Description: 
*
* Version: 1.0
* Created: 2017/12/5 0:15:18 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Helper
{
    public static class Box
    {
        private static readonly MainWindow _owner_m;
        private static readonly Window _owner;
        static Box()
        {
            _owner = Application.Current.MainWindow;
            _owner_m = (MainWindow)_owner;
        }
        public static bool BShowChoiceDialog(string keyTitle, string keyText, string keyTextBtnLeft = null, string keyTextBtnRight = null)
        {
            return ShowChoiceDialog(keyTitle, keyText, keyTextBtnLeft, keyTextBtnRight) == ChoiceResult.BtnRight;
        }
        public static ChoiceResult ShowChoiceDialog(string keyTitle, string keyText, string keyTextBtnLeft = null, string keyTextBtnRight = null)
        {
            var choiceBox = new ChoiceBox(_owner)
            {
                Data = new ChoiceBoxData()
                {
                    KeyTitle = keyTitle,
                    KeyText = keyText,
                    KeyBtnLeft = keyTextBtnLeft,
                    KeyBtnRight = keyTextBtnRight,
                }
            };
            return choiceBox.ShowDialog();
        }
        public static void ShowMessageDialog(string keyTitle, string keyText)
        {
            var messageBox = new MMessageBox(_owner)
            {
                Data = new MMessageBoxData()
                {
                    KeyText = keyText,
                    KeyTitle = keyTitle
                }
            };
            messageBox.ShowDialog();
        }
        public static void ShowLoadingDialog(IForceStoppable stoppable)
        {

        }
    }
}
