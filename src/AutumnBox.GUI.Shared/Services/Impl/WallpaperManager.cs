/*

* ==============================================================================
*
* Filename: WallpaperManager
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 23:44:34
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.ViewModels;
using AutumnBox.Leafx.Container.Support;
using System.Windows.Media;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IWallpaperManager))]
    class WallpaperManager : IWallpaperManager
    {
        public void Reset()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (App.Current.MainWindow.DataContext is VMMainWindowV3 vm)
                {
                    vm.WallpaperBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                }
            });
        }

        public void SetBrush(Brush brush)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (App.Current.MainWindow.DataContext is VMMainWindowV3 vm)
                {
                    vm.WallpaperBrush = brush;
                }
            });
        }
    }
}
