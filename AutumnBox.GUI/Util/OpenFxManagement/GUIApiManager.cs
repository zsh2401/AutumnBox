using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal class GUIApiManager
    {
        public static readonly IBaseApi BaseApiInstance;
        static GUIApiManager()
        {
            BaseApiInstance = new AutumnBox_GUI_Caller();
        }
    }
}
