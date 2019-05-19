using AutumnBox.GUI.MVVM;
using AutumnBox.OpenFramework.ExtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.GUI.Model
{
    class LibDock : ModelBase
    {
        public ILibrarian Lib
        {
            get => _lib; set
            {
                _lib = value;
                RaisePropertyChanged();
            }
        }
        private ILibrarian _lib;

        public int Count => Lib.GetWrappers().Count();
        public LibDock(ILibrarian lib)
        {
            Lib = lib ?? throw new ArgumentNullException(nameof(lib));
        }
        public static IEnumerable<LibDock> From(IEnumerable<ILibrarian> libs)
        {
            return from lib in libs
                   select new LibDock(lib);
        }
    }
}
