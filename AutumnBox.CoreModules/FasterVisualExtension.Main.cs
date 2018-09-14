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
            public override void After(AfterArgs args)
            {
                base.After(args);
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
