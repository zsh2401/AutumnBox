/* =============================================================================*\
*
* Filename: Box
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
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Helper
{
    public static class BoxHelper
    {
        public static bool ToBool(this ChoiceResult result)
        {
            return result == ChoiceResult.BtnRight;
        }
        private static readonly MainWindow _owner_m;
        private static readonly Window _owner;
        static BoxHelper()
        {
            _owner = Application.Current.MainWindow;
            _owner_m = (MainWindow)_owner;
        }
        #region ChoiceWindow
        public static ChoiceResult ShowChoiceDialog(string keyTitle, string keyText, string keyTextBtnLeft = null, string keyTextBtnRight = null)
        {
            ChoiceResult result = ChoiceResult.BtnCancel;
            App.Current.Dispatcher.Invoke(()=> {
                result =  new ChoiceBox(_owner)
                {
                    Data = new ChoiceBoxData()
                    {
                        KeyTitle = keyTitle,
                        KeyText = keyText,
                        KeyBtnLeft = keyTextBtnLeft,
                        KeyBtnRight = keyTextBtnRight,
                    }
                }.ShowDialog();
            });
            return result;
        }
        #endregion
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
        #region LoadingWindow
        public static bool _loadingWindowIsAlreadyHaveOne = false;
        public static void ShowLoadingDialog(ICompletable completable)
        {
            if (_loadingWindowIsAlreadyHaveOne) return;
            _loadingWindowIsAlreadyHaveOne = true;
            new LoadingWindow(_owner).ShowDialog(completable);
            _loadingWindowIsAlreadyHaveOne = false;
        }
        private static LoadingWindow _loadingWindow;
        public static void ShowLoadingDialog()
        {
            if (_loadingWindowIsAlreadyHaveOne) return;
            _loadingWindowIsAlreadyHaveOne = true;
            _loadingWindow = new LoadingWindow(_owner);
            _loadingWindow.ShowDialog();
        }
        public static void CloseLoadingDialog()
        {
            try
            {
                _loadingWindow.Close();
                _loadingWindowIsAlreadyHaveOne = false;
            }
            catch (Exception e) { Logger.T("A exception on close loadingwindow...", e); }

        }
        #endregion
    }
}
