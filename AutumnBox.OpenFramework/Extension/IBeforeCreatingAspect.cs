using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    public interface IBeforeCreatingAspect
    {
        void Do(BeforeCreatingAspectArgs args,ref bool canContinue);
    }
}
