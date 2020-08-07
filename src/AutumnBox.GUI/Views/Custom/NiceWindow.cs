using System;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Views.Custom
{
    public class NiceWindow : HandyControl.Controls.Window
    {
        protected override void OnActivated(EventArgs e)
        {
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/icon.ico"));
            base.OnActivated(e);
        }
    }
}
