/*

* ==============================================================================
*
* Filename: MethodReplacer
* Description: 
*
* Version: 1.0
* Created: 2020/8/19 17:09:11
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.Leafx.Enhancement.Runtime
{
    class MethodReplacer
    {
        public MethodReplacer(MethodInfo target, MethodInfo toReplace)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            ToReplace = toReplace ?? throw new ArgumentNullException(nameof(toReplace));
        }

        public MethodInfo Target { get; }
        public MethodInfo ToReplace { get; }
        private bool applied = false;
        public void Restore()
        {
            if (applied) throw new InvalidOperationException("Already applied.");
            applied = false;
        }
        public void Apply()
        {
            applied = true;
        }
    }
}
