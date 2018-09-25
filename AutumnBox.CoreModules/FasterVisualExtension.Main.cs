using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules
{
    public abstract partial class FasterVisualExtension : AtmbVisualExtension
    {
        public sealed override int Main()
        {
            WriteLineAndSetTip(MsgRunning);
            int exitCode = base.Main();
            switch (exitCode)
            {
                case 0:
                    SoundPlayer.OK();
                    break;
                default:
                    break;
            }
            return exitCode;
        }

        protected override bool VisualStop()
        {
            return false;
        }
    }
}
