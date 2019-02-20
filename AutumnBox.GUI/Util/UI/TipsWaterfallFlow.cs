using AutumnBox.GUI.Model;
using AutumnBox.GUI.View;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.Util.UI
{
    class TipsWaterfallFlow
    {
        private const int SUPPORTED_MAX_VER = 1;
        private const int SUPPORTED_MIN_VER = 1;
        private readonly Panel[] cols;
        public TipsWaterfallFlow(params Panel[] cols)
        {
            this.cols = cols;
        }

        public async void TipsChanged(IEnumerable<Tip> tips)
        {
            await Task.Run(() =>
            {
                HandleNewTips(tips);
            });
        }
        private readonly object _lock = new object();
        private void HandleNewTips(IEnumerable<Tip> tips)
        {
            lock (_lock)
            {
                Clear();
                if (tips == null) return;
                foreach (var tip in tips)
                {
                    AddTipCard(tip);
                    Thread.Sleep(500);
                }
            }
        }
        private bool IsVisible()
        {
            return App.Current.Dispatcher.Invoke(() =>
            {
                return cols.First().IsVisible;
            });
        }
        private void Clear()
        {
            foreach (var col in cols)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    col.Children.Clear();
                });
            }
        }
        private void AddTipCard(Tip tip)
        {
            if (tip.Ver > SUPPORTED_MAX_VER || tip.Ver < SUPPORTED_MIN_VER)
                return;
            var card = App.Current.Dispatcher.Invoke(() =>
            {
                return new TipCard(tip);
            });
            var col = GetTargetCol(tip.Col);
            App.Current.Dispatcher.Invoke(() =>
            {
                col.Children.Add(card);
            });
        }
        private Panel GetTargetCol(uint? id)
        {
            if (id == null || id > cols.Length - 1)
            {
                do
                {
                    Thread.Sleep(100);
                } while (!IsVisible());
                return App.Current.Dispatcher.Invoke(() =>
                {
                    return cols.OrderBy(col => col.Height).First();
                });
            }
            else
            {
                return cols[(uint)id];
            }
        }
    }
}
