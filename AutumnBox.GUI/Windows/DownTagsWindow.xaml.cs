using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutumnBox.OpenFramework.Open;
using System.Windows.Controls;
using AutumnBox.OpenFramework.Internal;
using LeanCloud;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// DownTagsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownTagsWindow : Window
    {
        public DownTagsWindow()
        {
            //LeanCloud配置
            AVClient.Initialize("mXkNor8O49P9WEbfHe5BV4xL-gzGzoHsz", "Rl8bw4b1NyaqninEhiO7kp2L");
            InitializeComponent();
            GetAllDataAsync();
        }

        public void ListBox(List<TagsData> t)
        {
            ListBox1.ItemsSource = t;
        }

        //这里先暂时这样写吧，紧耦合，等有时间再来解耦改变量什么的
        public async Task GetAllDataAsync()
        {
            var query = new AVQuery<AVObject>("AutumnBoxTags");
            var data = new List<AVObject>();
            await query.FindAsync().ContinueWith(t =>
            {
                data = t.Result.ToList();
            });

            var a = data;
            var b = new List<TagsData>();

            foreach (var d in a)
            {
                var c = new TagsData
                {
                    tagName = d.Get<string>("tagName"),
                    author = d.Get<string>("author"),
                    introduce = d.Get<string>("introduce"),
                    lastVersion = d.Get<string>("lastVersion"),
                    url = d.Get<string>("url") + "\'" + d.Get<string>("dllName")
                };

                try
                {
                    c.md5 = d.Get<string>("md5");
                }
                catch (Exception)
                {
                    c.md5 = "";
                }
                
                b.Add(c);
            }

            ListBox(b);
        }


        private void ButtonDownLoad_Click(object sender, RoutedEventArgs e)
        {
            var a = ((Button)sender).Tag.ToString();

            var b = a.Split('\'');
            var d = new DownLoad();
            var p = ExtensionManager.ExtensionsPath;

            Task.Run(() => d.DownloadFile(b[0], p + @"\" + b[1]));
        }

        private void ButtonRe_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }
    }

    /// <summary>
    /// TagData数据结构
    /// </summary>
    public class TagsData
    {
        public string tagName { get; set; }
        public string lastVersion { get; set; }
        public string author { get; set; }
        public string introduce { get; set; }
        public string md5 { get; set; }
        public string url { get; set; }
    }

}
