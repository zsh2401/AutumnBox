using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules
{
    public abstract partial class FasterVisualExtension : AtmbVisualExtension
    {
        private class SoundPlayAspect : ExtMainAsceptAttribute
        {
            public override void Before(BeforeArgs args)
            {
                base.Before(args);
                args.Extension.Logger.Info("sound,Before");
            }
            public override void After(AfterArgs args)
            {
                base.After(args);
                if (args.IsForceStopped)
                {
                    //args.Extension.SoundPlayer.Err();
                    return;
                }
                switch (args.ReturnCode)
                {
                    case 0:
                        args.Extension.SoundPlayer.OK();
                        break;
                    default:
                        //args.Extension.SoundPlayer.Err();
                        break;
                }
            }
        }

        [SoundPlayAspect]
        public override int Main()
        {
            WriteLineAndSetTip(MsgRunning);
            return base.Main();
        }

        protected override bool VisualStop()
        {
            return false;
        }
    }
}
