using AutumnBox.GUI.Util.OpenFxManagement;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules
{
    static class HideApiManager
    {
        public static readonly IAutumnBoxGUIApi guiHideApi = (IAutumnBoxGUIApi)GUIApiManager.BaseApiInstance;
        public static IVMLeafUIApi ToLeafUIHideApi(this ILeafUI ui) => (IVMLeafUIApi)ui;
    }
}
