/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 19:48:30 (UTC +8:00)
** desc： ...
*************************************************/
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.Windows
{
    internal sealed class TBLoadingEffect
    {
        private static readonly string[] marks = { "|", "/", "--", "\\" };
        private static int _max { get { return marks.Length; } }
        private int _current = 0;
        public int Interval { get; set; } = 500;
        private TextBlock textBlock;
        private bool _continue;
        public TBLoadingEffect(TextBlock tb)
        {
            textBlock = tb;
        }
        public async void Start()
        {
            _continue = true;
            await Task.Run(() =>
            {
                Run();
            });
        }
        private void Run()
        {
            while (_continue)
            {
                Change();
                Thread.Sleep(Interval);
            }
        }
        private void Change()
        {
            if (_current == _max)
            {
                _current = 0;
            }
            try
            {
                textBlock.Dispatcher.Invoke(() =>
                {
                    textBlock.Text = marks[_current];
                });
            }
            catch (TaskCanceledException) {
                Stop();
            }
            _current++;
        }
        public void Stop()
        {
            _continue = false;
        }
    }
}
