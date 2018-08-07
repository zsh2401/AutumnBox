/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/7 21:55:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.ExtLibrary;

namespace AutumnBox.ExampleExtensions
{
    public class Lib : ExtensionLibrarin
    {
        public override string Name => "Mother fucker";

        public override int MinApiLevel => 8;

        public override int TargetApiLevel => 8;
        public override void Ready()
        {
            base.Ready();
            App.RunOnUIThread(() =>
            {
                App.CreateDebuggingWindow().Show();
            });
        }
    }
}
