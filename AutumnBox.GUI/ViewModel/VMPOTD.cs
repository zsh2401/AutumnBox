/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 14:19:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.ViewModel
{
    class VMPOTD : ViewModelBase
    {
        public ICommand Click
        {
            get
            {
                return _click;
            }
            set
            {
                _click = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _click;

        public ImageSource Image
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
                RaisePropertyChanged();
            }
        }
        private ImageSource _img = null;

        public VMPOTD()
        {
            new PotdGetter().Try((result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    SettingBy(result);
                });
            });
        }
        private void SettingBy(PotdGetter.Result result)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = result.ImageMemoryStream;
            bmp.EndInit();
            Image = bmp;
            url = result.ClickUrl;
        }
        private string url;
        public void OpenUrl()
        {
            try
            {
                Process.Start(url);
            }
            catch { }
        }
    }
}
