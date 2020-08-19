using AutumnBox.GUI.MVVM;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.GUI.Models
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

        public int Count => Lib.Extensions.Count();

        public LibDock(ILibrarian lib)
        {
            _lib = lib ?? throw new ArgumentNullException(nameof(lib));
        }
        public static IEnumerable<LibDock> From(IEnumerable<ILibrarian> libs)
        {
            return from lib in libs
                   select new LibDock(lib);
        }
    }
}
