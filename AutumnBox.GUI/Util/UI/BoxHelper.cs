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
using AutumnBox.GUI.View.Windows;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.Log;
using System;
using System.Windows;

namespace AutumnBox.GUI.Util.UI
{
    [Obsolete("在新的MD设计中,将逐渐抛弃此类")]
    internal static class BoxHelper
    {
        //public static bool ToBool(this ChoiceResult result)
        //{
        //    return result == ChoiceResult.BtnRight;
        //}
        //private static MainWindow _owner_m
        //{
        //    get
        //    {
        //        MainWindow result = null;
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            result = App.Current.MainWindowAsMainWindow;
        //        });
        //        return result;
        //    }
        //}
        //private static Window _owner
        //{
        //    get
        //    {
        //        Window result = null;
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            result = App.Current.MainWindow;
        //        });
        //        return result;
        //    }
        //}

        //#region ChoiceWindow
        //public static ChoiceResult ShowChoiceDialog(
        //    string keyTitle, string keyText,
        //    string keyTextBtnLeft = null, string keyTextBtnRight = null)
        //{
        //    ChoiceResult result = ChoiceResult.BtnCancel;
        //    App.Current.Dispatcher.Invoke(() =>
        //    {
        //        result = new ChoiceBox()
        //        {
        //            Owner = App.Current.MainWindow.IsActive ? App.Current.MainWindow : null,
        //            Data = new ChoiceBoxData()
        //            {
        //                KeyTitle = keyTitle,
        //                KeyText = keyText,
        //                KeyBtnLeft = keyTextBtnLeft,
        //                KeyBtnRight = keyTextBtnRight,
        //            }
        //        }.ShowDialog();
        //    });
        //    return result;
        //}
        //#endregion

        //public static void ShowMessageDialog(string keyTitle, string keyText)
        //{
        //    var messageBox = new MMessageBox(_owner)
        //    {
        //        Data = new MMessageBoxData()
        //        {
        //            KeyText = keyText,
        //            KeyTitle = keyTitle
        //        }
        //    };
        //    messageBox.ShowDialog();
        //}

        //#region LoadingWindow
        //private static bool _loadingWindowIsAlreadyHaveOne = false;
        //private static Windows.LoadingWindow _loadingWindow;
        //private const string TAG = "BoxHelper";

        //public static void ShowLoadingDialog(ICompletable completable)
        //{
        //    if (_loadingWindowIsAlreadyHaveOne) return;
        //    _loadingWindowIsAlreadyHaveOne = true;
        //    new Windows.LoadingWindow(_owner).ShowDialog(completable);
        //    _loadingWindowIsAlreadyHaveOne = false;
        //}
        //public static void ShowLoadingDialog()
        //{
        //    App.Current.Dispatcher.Invoke(() =>
        //    {
        //        if (_loadingWindowIsAlreadyHaveOne) return;
        //        _loadingWindowIsAlreadyHaveOne = true;
        //        _loadingWindow = new Windows.LoadingWindow(_owner);
        //        _loadingWindow.ShowDialog();
        //    });

        //}
        //public static void CloseLoadingDialog()
        //{
        //    App.Current.Dispatcher.Invoke(() =>
        //    {
        //        try
        //        {
        //            _loadingWindow.Close();
        //            _loadingWindowIsAlreadyHaveOne = false;
        //        }
        //        catch (Exception e)
        //        {
        //            Logger.Warn(TAG, "A exception on close loadingwindow...", e);
        //        }
        //    });

        //}
        //#endregion
    }
}
