using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Bridge
{
    public interface IAtmbSlice
    {
        public string Name { get; }
        public string Description { get; }
        object View { get; }
        void Init();
        void Ready();
        void OnDisplay();
        void OnPause();
        void ReceiveData(object data);
        void Destory();
    }
}
