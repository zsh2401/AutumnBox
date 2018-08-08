/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/9 0:11:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ExampleExtensions
{
    [ExtName("重载测试")]
    public class ReloadExample : AutumnBoxExtension
    {
        public override int Main()
        {
            var fx = new Factory().GetFx(this);
            fx.ReloadLibs();
            App.RunOnUIThread(() =>
            {
                App.RefreshExtensionList();
            });
            return 0;
        }
    }
}
