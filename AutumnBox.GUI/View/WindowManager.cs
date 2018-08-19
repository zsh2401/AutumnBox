/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 18:53:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.GUI.View.Windows;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.View
{
    static class WindowManager
    {
        public static bool ToBool(this ChoiceResult result)
        {
            return result == ChoiceResult.BtnRight;
        }
        private static MainWindow _owner_m
        {
            get
            {
                MainWindow result = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    result = App.Current.MainWindowAsMainWindow;
                });
                return result;
            }
        }
        private static Window _owner
        {
            get
            {
                Window result = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    result = App.Current.MainWindow;
                });
                return result;
            }
        }

        #region ChoiceWindow
        public static ChoiceResult ShowChoiceDialog(
            string keyTitle, string keyText,
            string keyTextBtnLeft = null, string keyTextBtnRight = null)
        {
            ChoiceResult result = ChoiceResult.BtnCancel;
            App.Current.Dispatcher.Invoke(() =>
            {
                result = new ChoiceBox()
                {
                    Owner = App.Current.MainWindow.IsActive ? App.Current.MainWindow : null,
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
        private static bool _loadingWindowIsAlreadyHaveOne = false;
        private static LoadingWindow _loadingWindow;
        private const string TAG = "BoxHelper";

        public static void ShowLoadingDialog(ICompletable completable)
        {
            if (_loadingWindowIsAlreadyHaveOne) return;
            _loadingWindowIsAlreadyHaveOne = true;
            new LoadingWindow(_owner).ShowDialog(completable);
            _loadingWindowIsAlreadyHaveOne = false;
        }
        public static void ShowLoadingDialog()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (_loadingWindowIsAlreadyHaveOne) return;
                _loadingWindowIsAlreadyHaveOne = true;
                _loadingWindow = new LoadingWindow(_owner);
                _loadingWindow.ShowDialog();
            });

        }
        public static void CloseLoadingDialog()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    _loadingWindow.Close();
                    _loadingWindowIsAlreadyHaveOne = false;
                }
                catch (Exception e)
                {
                    Logger.Warn(TAG, "A exception on close loadingwindow...", e);
                }
            });

        }
        #endregion
    }
}
