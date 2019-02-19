using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMTips : ViewModelBase
    {
        public const string FAULT = "1";
        public const string SUCCESS = "2";
        public const string LOADING = "3";

        public IEnumerable<Tip> Tips
        {
            get => _tips; set
            {
                _tips = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<Tip> _tips;

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

        public bool IsNotHidden
        {
            get => _isNotHidden; set
            {
                _isNotHidden = value;
                RaisePropertyChanged();
            }
        }
        private bool _isNotHidden = true;

        public ICommand Hide
        {
            get => _hide; set
            {
                _hide = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _hide;

        public VMTips()
        {
            RaisePropertyChangedOnDispatcher = true;
            _Refresh();
            Refresh = new FlexiableCommand(_Refresh);
            Hide = new FlexiableCommand(() =>
            {
                IsNotHidden = false;
            });
        }

        public void _Refresh()
        {
            Status = LOADING;
            Tips = null;
            new TipsGetter().Advance().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Status = FAULT;
                    Tips = new List<Tip>();
                    SLogger<VMTips>.Warn("Can not refresh tips", task.Exception);
                }
                else
                {
                    Status = SUCCESS;
                    IsNotHidden = task.Result.Enable;
                    Tips = task.Result.Tips;
                }
            });
        }
    }
}
