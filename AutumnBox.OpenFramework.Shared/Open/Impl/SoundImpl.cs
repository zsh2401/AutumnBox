using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class SoundImpl : ISoundService
    {
        public void OK()
        {
            OpenFx.BaseApi.PlayOk();
        }
    }
}
