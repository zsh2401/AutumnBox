/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 14:19:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.Net;
using System;
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
            Click = new FlexiableCommand(() =>
            {
                try
                {
                    Process.Start(url);
                }
                catch (Exception e)
                {
                    SLogger.Warn(this, "Click potd is failed", e);
                }
            });
            new PotdGetter().Try((result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (result.Enable == true)
                    {
                        SettingBy(result);
                    }
                });
            });
        }

        private void SettingBy(PotdGetter.Result result)
        {
            url = result.ClickUrl;
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = result.ImageMemoryStream;
            bmp.EndInit();
            Image = bmp;
        }
        private string url;
    }
}
