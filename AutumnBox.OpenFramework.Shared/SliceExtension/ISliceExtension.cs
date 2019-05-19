using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.SliceExtension
{
    interface ISliceExtension
    {
        string Title { get; }
        string Icon { get; }
        object View { get; }
        bool PreCheck();
        void Init();
        void Displaying();
        void Pausing();
        void ReceiveData(object data);
        void Destory();
    }
}
