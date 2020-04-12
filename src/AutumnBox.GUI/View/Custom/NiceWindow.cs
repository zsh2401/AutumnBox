using System;
using System.Windows;

namespace AutumnBox.GUI.View.Custom
{
    public class NiceWindow : HandyControl.Controls.Window
    {
        static NiceWindow()
        {
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
        }
    }
}
