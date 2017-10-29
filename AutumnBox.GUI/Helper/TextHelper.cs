/* =============================================================================*\
*
* Filename: TextHelper
* Description: 
*
* Version: 1.0
* Created: 2017/10/29 16:53:29 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Helper
{
    static class TextHelper
    {
        private static string En(string Key, string content)
        {
            //http://www.jb51.net/article/58442.htm
            char[] data = content.ToCharArray();
            char[] key = Key.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }
            return new string(data);
        }
        private static string De(string Key, string ciphertext)
        {
            char[] data = ciphertext.ToCharArray();
            char[] key = Key.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }
            return new string(data);
        }
    }
}
