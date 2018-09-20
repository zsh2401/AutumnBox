/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:07:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class SoundImpl : ISoundPlayer
    {
        private readonly IAutumnBox_GUI sourceApi;
        public SoundImpl( IAutumnBox_GUI sourceApi)
        {
            this.sourceApi = sourceApi;
        }

        public void OK()
        {
            sourceApi.PlayOk();
        }
    }
}
