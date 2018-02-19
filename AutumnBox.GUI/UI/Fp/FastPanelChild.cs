/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/19 8:20:46 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.Fp
{
    public class FastPanelChild:UserControl
    {
        public event EventHandler Finished;
        public virtual void OnPanelDisplayed() { }
        public virtual void OnPanelClosed() { }
        public void Finish() {
            Finished?.Invoke(this, new EventArgs());
        }
    }
}
