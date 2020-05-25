/*

* ==============================================================================
*
* Filename: FakeOpenFxInitializer
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 16:53:33
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX
{
    internal static class FakeOpenFx
    {
        public static FakeBaseApi FakeApi { get; } = new FakeBaseApi();
        public static void Initialize()
        {
            OpenFx.Load(FakeApi);
            OpenFx.RefreshExtensionsList();
        }
    }
}
