/*

* ==============================================================================
*
* Filename: OSVersions
* Description: 
*
* Version: 1.0
* Created: 2020/3/13 23:15:21
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.OS.Acrylic
{
    class OSVersions
    {
        public static Version Windows7 { get { return new Version(6, 1, 7600); } }
        public static Version Windows7_SP1 { get { return new Version(6, 1, 7601); } }

        public static Version Windows8 { get { return new Version(6, 2, 9200); } }
        public static Version Windows8_1 { get { return new Version(6, 3, 9600); } }

        public static Version Windows10 { get { return new Version(10, 0, 10240); } }
        public static Version Windows10_1511 { get { return new Version(10, 0, 10586); } }
        public static Version Windows10_1607 { get { return new Version(10, 0, 14393); } }
        public static Version Windows10_1703 { get { return new Version(10, 0, 15063); } }
        public static Version Windows10_1709 { get { return new Version(10, 0, 16299); } }
        public static Version Windows10_1803 { get { return new Version(10, 0, 17134); } }
        public static Version Windows10_1809 { get { return new Version(10, 0, 17763); } }
        public static Version Windows10_1903 { get { return new Version(10, 0, 18362); } }
    }
}
