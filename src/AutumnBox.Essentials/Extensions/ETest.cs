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
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Essentials.Extensions
{
    [ExtName("test")]
    [ExtDeveloperMode]
    class ETest : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI _ui)
        {
            using var ui = _ui;
            ui.Show();
            throw new Exception("test_error");
        }
    }
}
