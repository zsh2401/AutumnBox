/* =============================================================================*\
*
* Filename: SideloadFlasher.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 19:53:39(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Bundles;

namespace AutumnBox.Basic.Function.Modules
{
    public class SideloadFlasher : FunctionModule
    {
        protected override CheckResult Check(ModuleArgs args)
        {
            return CheckResult.Unfinish;
        }
        protected override OutputData MainMethod(BundleForTools bundle)
        {
            throw new NotImplementedException();
        }
    }
}
