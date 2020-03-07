using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    public interface IProxy
    {
        void CreateInstance();
        object CallMethod(params ILake[] lake);
    }
    public interface IProxyManager
    {
        public
    }
}
