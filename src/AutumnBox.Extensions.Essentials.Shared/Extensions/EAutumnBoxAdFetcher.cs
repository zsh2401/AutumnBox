using AutumnBox.Leafx.Container;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxAdFetcher : LeafExtensionBase
    {
        readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };

        [LMain]
        public void EntryPoint(IAppManager appManager, IXCardsManager xCardsManager)
        {
            if (!appManager.EnableAD) return;
            DisplayTextAd(xCardsManager);
        }

        const string TEXTAD = "http://www.atmb.top/client-api/moonboat/textad.json";
        private void DisplayTextAd(IXCardsManager xCardsManager)
        {
            try
            {
                var jsonString = webClient.DownloadString(TEXTAD);
                var card = JsonSerializer.Deserialize<TextADCard>(jsonString);
                xCardsManager.Register(card);
            }
            catch (Exception e)
            {
                SLogger<EAutumnBoxAdFetcher>.Warn(e);
            }
        }
        private class TextADCard : IXCard
        {
            [JsonPropertyName("content")]
            public string Content { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonIgnore]
            public int Priority => 0;

            [JsonIgnore]
            public object View { get; private set; } = "";

            public void Create()
            {
                var grid = new Grid();
                grid.Children.Add(new TextBlock
                {
                    Text = Content,
                    Margin = new System.Windows.Thickness(10, 10, 10, 10),
                });
                grid.MouseDown += (s, e) =>
                {
                    try
                    {
                        LakeProvider.Lake.Get<IAppManager>().OpenUrl(Url);
                    }
                    catch { }
                };
                this.View = grid;
            }

            public void Destory()
            {
                this.View = null;
            }

            public void Update()
            {
                //pass
            }
        }
    }
}
