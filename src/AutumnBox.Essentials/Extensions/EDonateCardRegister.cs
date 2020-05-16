/*

* ==============================================================================
*
* Filename: EDonateCard
* Description: 
*
* Version: 1.0
* Created: 2020/5/3 15:38:24
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EDonateCardRegister : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IXCardsManager xCardsManager, IBaseApi baseApi)
        {
            xCardsManager.Register(new DonateCard(() => baseApi.UnstableInternalApiCall("show_donate_window")));
        }

        [ClassText("desc", "You make a living by what you get, but you make a life by what you give.", "zh-cn:赠人玫瑰，手留余香")]
        [ClassText("btn", "donate us!", "zh-cn:捐赠秋之盒")]
        private class DonateCard : IXCard
        {
            private readonly Action showDonate;

            public DonateCard(Action showDonate)
            {
                this.showDonate = showDonate ?? throw new ArgumentNullException(nameof(showDonate));
            }

            public int Priority => int.MinValue;

            public object View { get; private set; }
            private TextBlock donateDescription;
            private Button donateBtn;
            public void Create()
            {
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
                donateDescription = new TextBlock()
                {
                    Margin = new System.Windows.Thickness(0,0,0,10),
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    TextAlignment = System.Windows.TextAlignment.Center
                };
                donateBtn = new Button();
                donateBtn.Click += (s, e) => showDonate();
                stackPanel.Children.Add(donateDescription);
                stackPanel.Children.Add(donateBtn);
                View = stackPanel;
            }

            public void Destory()
            {
                donateDescription = null;
                donateBtn = null;
                View = null;
            }

            public void Update()
            {
                donateDescription.Text = this.RxGetClassText("desc");
                donateBtn.Content = this.RxGetClassText("btn");
            }
        }
    }
}
