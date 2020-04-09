/*

* ==============================================================================
*
* Filename: DependencyInjector
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 18:01:18
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;

namespace AutumnBox.OpenFramework.Leafx
{
    public sealed class DependencyInjector
    {
        private readonly ILake lake;

        public DependencyInjector(ILake lake, object instance) {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }
            this.lake = lake;
        }
        public void Inject() { 
        
        }
    }
}
