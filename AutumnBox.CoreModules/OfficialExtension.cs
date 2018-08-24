/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules
{
    [ExtAuth("秋之盒-zsh2401")]
    [ExtAuth("AutumnBox official", Lang = "en-us")]
    [ExtOfficial(true)]
    [ContextPermission(CtxPer.High)]
    public abstract class OfficialExtension : AtmbVisualExtension
    {
        protected string Res(string key)
        {
            return App.GetPublicResouce(key)?.ToString() ?? key;
        }
    }
}
