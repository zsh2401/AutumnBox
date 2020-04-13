using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.MVVM
{
    public class InjectableObject
    {
        protected virtual bool InjectProperties => true;
        public InjectableObject()
        {
            if ((!IsDesignMode()) && InjectProperties)
            {
                DependenciesInjector.Inject(this, App.Current?.Lake);
            }
        }
        public bool IsDesignMode()
        {
            return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}
