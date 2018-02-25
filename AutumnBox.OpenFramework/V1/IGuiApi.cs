using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.V1
{
    public interface IGuiApi
    {
        bool? ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null);
        void ShowMessageBox(string title,string msg);
        void ShowLoadingWindow(ICompletable completable);
        void ShowWindow(Window window);
    }
}
