/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 22:45:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.UI.Model.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.ViewModel.Panel
{
    class VMExtensions : ViewModelBase
    {
        public ICommand RunExtension { get; set; }
        public ModelExtensions Model { get; set; }
        public VMExtensions(DeviceState targetState)
        {
            Model = new ModelExtensions(targetState);
            RunExtension = new MVVMCommand((args) =>
             {
                 Model.Selected.RunAsync(Model.CurrentDevcie);
             });
        }
    }
}
