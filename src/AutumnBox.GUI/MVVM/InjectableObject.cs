using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.MVVM
{
    public class InjectableObject
    {
        protected virtual bool InjectProperties => true;
        public InjectableObject()
        {
            if (InjectProperties)
            {
                DependenciesInjector.Inject(this, App.Current.Lake);
            }
        }
    }
}
