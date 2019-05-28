using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.LeafExtension.View
{
    public class DialogClosedEventArgs
    {
        public object Result { get; set; }
        public DialogClosedEventArgs() { }
        public DialogClosedEventArgs(object result)
        {
            Result = result;
        }
    }
}
