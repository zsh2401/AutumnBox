/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 18:53:33 (UTC +8:00)
** desc： ...
*************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl
{
    class NpdateInfoGetter : IUpdateInfoGetter
    {
        public IUpdateInfo Get()
        {
            var responseText = new WebClient().DownloadString(UpdaterCore.UPDATE_API);
            Debug.WriteLine(responseText);
            return JsonConvert.DeserializeObject<NpdateInfo>(responseText);
        }
    }
}
