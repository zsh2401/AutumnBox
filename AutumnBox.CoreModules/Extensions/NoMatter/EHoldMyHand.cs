/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Impl;
using System.Collections.Generic;
using System.IO;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Example extension")]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(LeafConstants.NoMatter)]
    [ExtDeveloperMode]
    [ExtText("fuck", "Hello", "zh-cn:你好!")]
    //[ExtMinAndroidVersion(9, 0, 0)]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LProperty]
        private IUx Ux { get; set; }

        [LProperty]
        private IDevice Device { get; set; }

        [LMain]
        public void Main(IDevice device, IAppManager app, ILogger<string> logger, Context context, ILeafUI ui, IUx ux, Dictionary<string, object> data, TextAttrManager manager)
        {
            using (ui)
            {
                ui.Show();
                var storageManager = new StorageManagerImpl(context);
                using (var fs = storageManager.OpenFile("wtf"))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write("hello!");
                    }
                }
                List<string> list = new List<string>() {
                    "1","2"
                };
                storageManager.SaveJsonObject("main",list);
                ui.Finish();
            }
        }
    }
}
