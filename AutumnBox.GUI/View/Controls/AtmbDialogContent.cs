/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/21 21:32:33 (UTC +8:00)
** desc： ...
*************************************************/
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Controls
{
    public class AtmbDialogContent : UserControl
    {
        protected DialogSession Session { get; set; }
        public virtual void Show()
        {
            DialogHost.Show(this, OnDialogOpened, OnDialogClosing);
        }
        protected virtual void OnDialogOpened(object sender, DialogOpenedEventArgs e)
        {
            Session = e.Session;
        }
        protected virtual void OnDialogClosing(object sender, DialogClosingEventArgs e)
        {
        }
        protected virtual void Finish()
        {
            Session.Close();
        }
    }
}
