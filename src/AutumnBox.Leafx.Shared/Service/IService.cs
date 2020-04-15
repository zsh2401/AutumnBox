using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Service
{
    public interface IService
    {
        void Initialize();
        void Start();
        void Pause();
        void Handle(IMessage message);
        void Stop();
    }
}
