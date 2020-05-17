/*

* ==============================================================================
*
* Filename: VMMainWindowV3
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 23:46:49
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindowV3 : ViewModelBase
    {
        public Brush WallpaperBrush
        {
            get => _wallpaperBrush; set
            {
                _wallpaperBrush = value;
                RaisePropertyChanged();
            }
        }
        private Brush _wallpaperBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        [AutoInject]
        IWallpaperManager wallpaperManager;
        public VMMainWindowV3()
        {
            RaisePropertyChangedOnUIThread = true;
        }
    }
}
