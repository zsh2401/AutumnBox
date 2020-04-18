using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Open;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.Essentials.XCards
{
    [Component(SingletonMode = true, Type = typeof(DevPanel))]
    class DevPanel : IXCard, IDisposable
    {
        public bool Enable { get; set; } = false;

        public int Priority => 10;

        public object View => element;

        private UIElement element;

        private TextBlock tbFreeKitSize;
        private TextBlock tbComponentSize;

        [AutoInject]
        private readonly ILake lake;

        [AutoInject]
        private readonly IAppManager appManager;

        public void Create()
        {
            var stack = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };
            tbFreeKitSize = new TextBlock();
            tbComponentSize = new TextBlock();
            stack.Children.Add(tbFreeKitSize);
            stack.Children.Add(tbComponentSize);
            element = stack;
            Task.Run(UpdateTask);
        }

        private void UpdateTask()
        {
            while (true)
            {
                appManager.RunOnUIThread(() => Update());
                Thread.Sleep(1000);
            }
        }

        public void Destory()
        {
        }

        public void Update()
        {
            tbComponentSize.Text = $"Component Count: {lake.Count}";
            tbFreeKitSize.Text = $"FreeKit Count: {LakeExtension.FreeKitRecordSize}";
        }

        public void Dispose()
        {

        }
    }
}
