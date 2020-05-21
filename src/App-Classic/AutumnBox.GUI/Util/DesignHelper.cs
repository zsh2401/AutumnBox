using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util
{
    public static class DesignHelper
    {
        private static bool? _isDesignModeCache = null;
        public static bool IsDesignMode
        {
            get
            {
                if (_isDesignModeCache == null)
                {
                    _isDesignModeCache = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
                }
                return _isDesignModeCache == true;
            }
        }
    }
}
