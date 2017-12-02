/* =============================================================================*\
*
* Filename: ChoiceGrid
* Description: 
*
* Version: 1.0
* Created: 2017/12/2 20:09:14 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.UI.Grids
{
    public partial class ChoiceGrid
    {
        public enum ChoiceResult
        {
            Left = 0,
            Right = 1,
            Cancel = -1,
        }
        public class ChoiceData
        {
            public string Title { get; set; } = "Title";
            public string Text { get; set; } = "Text";
            public string TextBtnLeft { get; set; } = "Left";
            public string TextBtnRight { get; set; } = "Right";
            public bool CanCancel { get; set; } = true;
        }
        public class HidedEventArgs : EventArgs {
			public ChoiceResult Result { get; internal set; }
        }
        public delegate void HidedEventHandler(object sender, HidedEventArgs e);
    }
}
