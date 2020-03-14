using AutumnBox.GUI.Util.OS.BlurKit;
using System;
using System.Windows;
using System.Windows.Interop;

namespace AutumnBox.GUI.View.Custom
{
    public class NiceWindow : HandyControl.Controls.Window
    {
        static NiceWindow() {
            DefaultStyleKeyProperty.OverrideMetadata(
               typeof(NiceWindow),
               new FrameworkPropertyMetadata(typeof(NiceWindow)));
        }
        public NiceWindow()
        {

        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            //Background = (SolidColorBrush)App.Current.Resources["BlurWindowBackgroundBrush"];
            var ptr = new WindowInteropHelper(this).Handle;
            AcrylicHelper.EnableBlur(ptr);
        }
    }
}
