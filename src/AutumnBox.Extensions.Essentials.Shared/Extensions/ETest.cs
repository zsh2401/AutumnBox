/*

* ==============================================================================
*
* Filename: ETest
* Description: 
*
* Version: 1.0
* Created: 2020/5/19 20:33:29
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Essentials.Extensions
{
    [ExtName("test")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtDeveloperMode]
    class ETest : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui)
        {
            ui.Show();
            ui.StatusInfo = "少女祈祷中...";
            ui.ShowMessage("你好!秋之盒");
            ui.Println("测试信息");
            ui.Finish();
        }
    }
}
