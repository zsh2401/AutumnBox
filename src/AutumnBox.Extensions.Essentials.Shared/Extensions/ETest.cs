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
        public void EntryPoint(ILeafUI _ui, IDevice device,ISoundService soundService)
        {
            soundService.OK();
        }
    }
}
