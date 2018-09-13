/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules
{
    [ExtAuth("AutumnBox")]
    [ExtAuth("AutumnBox official", Lang = "en-us")]
    [ExtOfficial(true)]
    [ContextPermission(CtxPer.High)]
    public abstract partial class OfficialVisualExtension : AtmbVisualExtension
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
