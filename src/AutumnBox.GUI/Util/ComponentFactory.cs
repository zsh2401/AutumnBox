/*

* ==============================================================================
*
* Filename: ComponentFactory
* Description: 
*
* Version: 1.0
* Created: 2020/4/11 1:30:06
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.GUI.Services;
using AutumnBox.GUI.Services.Impl;
using AutumnBox.GUI.Services.Impl.Arcylic;
using AutumnBox.GUI.Util.Loader;
using AutumnBox.Leafx.Container.Support;

namespace AutumnBox.GUI.Util
{
    internal sealed class ComponentFactory
    {
        [Component]
        public IThemeManager GetThemeManager() =>
            throw new ShouldAutoCreateException(typeof(ThemeManager));

        [Component]
        public ILanguageManager GetLanguageManager() =>
                throw new ShouldAutoCreateException(typeof(LanguageManager));

        [Component]
        public GeneralAppLoader GetAppLoader() =>
                throw new ShouldAutoCreateException(typeof(GeneralAppLoader));

        [Component]
        public IAcrylicHelper GetAcrylicHelper() =>
            throw new ShouldAutoCreateException(typeof(AcrylicHelper));

        [Component]
        public IAdbDevicesManager GetAdbDevicesManager() =>
            throw new ShouldAutoCreateException(typeof(AdbDevicesManagerImpl));
    }
}
