/* =============================================================================*\
*
* Filename: BlockHelper
* Description: 
*
* Version: 1.0
* Created: 2017/12/3 18:38:48 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.UI.Cstm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static AutumnBox.GUI.Helper.UIHelper;
namespace AutumnBox.GUI.Helper
{
    public static class BlockHelper
    {
        private static MainWindow _mainWindow { get { return (MainWindow)App.Current.MainWindow; } }
        private static Grid _mainGrid
        {
            get
            {
                return null;
            }
        }
        #region ChoiceBlock
        private static object _blockLock = new object();
        private static ChoiceBlock _choiceBlock;

        public static void CloseChoiceBlock()
        {
            _choiceBlock.Hide();
        }
        public static ChoiceResult ShowChoiceBlock(string keyTitle, string keyMessage, string keyTextLeftBtn = null, string keyTextRightBtn = null)
        {
            lock (_blockLock)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    _choiceBlock = new ChoiceBlock(_mainGrid, Make(keyTitle, keyMessage, keyTextLeftBtn, keyTextRightBtn));
                    _choiceBlock.Show();
                });
            }
            _choiceBlock.WaitToClosed();
            return _choiceBlock.Result ?? ChoiceResult.Cancel;
        }
        public static bool BShowChoiceBlock(string keyTitle, string keyMessage, string keyTextLeftBtn = null, string keyTextRightBtn = null)
        {
            return ShowChoiceBlock(keyTitle, keyMessage, keyTextLeftBtn, keyTextRightBtn) == ChoiceResult.Right;
        }
        private static ChoiceData Make(string keyTitle, string keyMessage, string keyTextLeftBtn = null, string keyTextRightBtn = null)
        {
            return new ChoiceData()
            {
                Title = GetString(keyTitle),
                Text = GetString(keyMessage),
                TextBtnLeft = (keyTextLeftBtn == null) ? UIHelper.GetString("btnCancel") : UIHelper.GetString(keyTextLeftBtn),
                TextBtnRight = (keyTextRightBtn == null) ? UIHelper.GetString("btnOK") : UIHelper.GetString(keyTextRightBtn),
            };
        }
        #endregion
        public static void ShowMessageBlock(string keyTitle, string keyMessage)
        {
            lock (_blockLock)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    new MessageBlock(_mainGrid, UIHelper.GetString(keyTitle), UIHelper.GetString(keyMessage)).Show();
                });
            }
        }
    }
}
