/* =============================================================================*\
*
* Filename: BundleForCreate
* Description: 
*
* Version: 1.0
* Created: 2017/11/22 19:29:53 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Function.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function.Bundles
{
    public class BundleForCreate : Bundle
    {
        public ModuleArgs Args { get; private set; }
        public BundleForCreate(ModuleArgs args)
        {
            Args = args;
        }
    }
}
