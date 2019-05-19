using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Bridge
{
    interface IAtmbLibManager
    {
        void Load();
        void Ready();
        IAtmbSlice Slices { get; }
        void Destory();
    }
}
