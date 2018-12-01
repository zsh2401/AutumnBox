using AutumnBox.Reporter.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Reporter.Model
{
    class ReportHeader : ModelBase
    {
        public string UUID
        {
            get => _uuid; set
            {
                _uuid = value;
                RaisePropertyChanged();
            }
        }
        private string _uuid;

        public string UserName
        {
            get => _userName; set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }
        private string _userName;

        public string UserMail
        {
            get => _userMail; set
            {
                _userMail = value;
                RaisePropertyChanged();
            }
        }
        private string _userMail;

        public string Remark
        {
            get => _remark; set
            {
                _remark = value;
                RaisePropertyChanged();
            }
        }
        private string _remark;

        public ReportHeader()
        {
            UUID = Guid.NewGuid().ToString();
        }
    }
}
