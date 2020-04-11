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

using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.Util.Impl;
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
        public GeneralAppLoader AppLoader() =>
                throw new ShouldAutoCreateException(typeof(GeneralAppLoader));
    }
}
