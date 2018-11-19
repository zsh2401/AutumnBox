using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Aspect
{
    class ThreeStepWarn : BeforeCreatingAspect
    {
        private readonly string keyOrContnetFirst;
        private readonly string keyOrContnetSec;
        private readonly string keyOrContnetThr;

        public ThreeStepWarn(string keyOrContnetFirst, string keyOrContnetSec, string keyOrContnetThr)
        {
            if (string.IsNullOrWhiteSpace(keyOrContnetFirst))
            {
                throw new ArgumentException("message", nameof(keyOrContnetFirst));
            }

            if (string.IsNullOrWhiteSpace(keyOrContnetSec))
            {
                throw new ArgumentException("message", nameof(keyOrContnetSec));
            }

            if (string.IsNullOrWhiteSpace(keyOrContnetThr))
            {
                throw new ArgumentException("message", nameof(keyOrContnetThr));
            }

            this.keyOrContnetFirst = keyOrContnetFirst;
            this.keyOrContnetSec = keyOrContnetSec;
            this.keyOrContnetThr = keyOrContnetThr;
        }
        public override void BeforeCreating(BeforeCreatingAspectArgs args, ref bool canContinue)
        {
            if (!args.Context.Ux.DoYN(CoreLib.Current.GetTextByKey(keyOrContnetFirst))) return;
            if (!args.Context.Ux.DoYN(CoreLib.Current.GetTextByKey(keyOrContnetSec))) return;
            if (!args.Context.Ux.DoYN(CoreLib.Current.GetTextByKey(keyOrContnetThr))) return;
            throw new NotImplementedException();
        }
    }
}
