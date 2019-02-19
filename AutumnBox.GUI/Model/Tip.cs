using AutumnBox.GUI.MVVM;
using AutumnBox.Logging;
using Newtonsoft.Json;
using System.Net;

namespace AutumnBox.GUI.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    class Tip : ModelBase
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("pic")]
        public string ImageUrl
        {
            get => _imgUrl; set
            {
                _imgUrl = value;
                if (_imgUrl != null)
                {
                    DownloadImage();
                }
                RaisePropertyChanged();
            }
        }
        private string _imgUrl;

        [JsonProperty("target")]
        public string Target { get; set; }

        public byte[] Image
        {
            get => _img; set
            {
                _img = value;
                RaisePropertyChanged();
            }
        }
        private byte[] _img;

        [JsonProperty("imgtarget")]
        public string ImageTarget
        {
            get => _imgTarget; set
            {
                _imgTarget = value;
                RaisePropertyChanged();
            }
        }
        private string _imgTarget;

        private void DownloadImage()
        {
            new WebClient().DownloadDataTaskAsync(ImageUrl).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Image = task.Result;
                }
                else
                {
                    SLogger.Warn(this, $"can not download img from {ImageUrl}", task.Exception);
                }
            });
        }
    }
}
