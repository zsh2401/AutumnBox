using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMCst : ViewModelBase
    {
        public const string FAULT = "1";
        public const string SUCCESS = "2";
        public const string LOADING = "3";

        public object Content
        {
            get => _content; set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
        private object _content;

        public ICommand Refresh
        {
            get => _ref; set
            {
                _ref = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _ref;

        public string Status
        {
            get => _status; set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }
        private string _status = LOADING;


        public VMCst()
        {
            RaisePropertyChangedOnDispatcher = true;
            _Refresh();
            Refresh = new FlexiableCommand(_Refresh);
        }

        public void _Refresh()
        {
            Status = LOADING;
            Content = null;
            new CstGetter().DoAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Status = FAULT;
                    SLogger<VMCst>.Warn("Can not refresh cst", task.Exception);
                }
                else
                {
                    Status = SUCCESS;
                    Content = task.Result;
                }
            });
        }
    }
}
