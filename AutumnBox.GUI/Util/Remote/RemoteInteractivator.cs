/*

* ==============================================================================
*
* Filename: RemoteInteractivator
* Description: 
*
* Version: 1.0
* Created: 2020/3/16 23:20:21
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.Logging;
using System;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Remote
{
    class RemoteInteractivator
    {
        public async Task DoInteractivate()
        {

            try
            {
                throw new NotImplementedException();
                var index = await new IndexReader().Do();
                _ = new MOTDHandler().Do(index);
            }
            catch (Exception e)
            {
                SLogger<RemoteInteractivator>.Warn("Failed", e);
            }
        }
    }
}
