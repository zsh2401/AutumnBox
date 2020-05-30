/*

* ==============================================================================
*
* Filename: XCardManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/17 0:16:03
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;

#nullable enable
namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(IXCardsManager))]
    internal sealed class XCardsManager : IXCardsManager
    {
        private readonly IBaseApi baseApi;

        public XCardsManager(IBaseApi baseApi)
        {
            this.baseApi = baseApi;
            baseApi.RunOnUIThread(() =>
            {
                baseApi.Destorying += AutumnBoxDestorying;
                baseApi.LanguageChanged += (s, e) => UpdateCards();
            });
        }

        private void AutumnBoxDestorying(object? sender, EventArgs e)
        {
            cards.ForEach(card => Unregister(card));
        }

        private readonly List<IXCard> cards = new List<IXCard>();

        private void UpdateCards()
        {
            cards.ForEach(card =>
            {
                baseApi.RunOnUIThread(() =>
                {
                    card.Update();
                });
            });
        }
        public void Register(IXCard card)
        {
            baseApi.RunOnUIThread(() =>
            {
                card.Create();
                card.Update();
                baseApi.AppendPanel(card.View, card.Priority);
                cards.Add(card);
            });
        }

        public void Unregister(IXCard card)
        {
            baseApi.RunOnUIThread(() =>
            {
                baseApi.RemovePanel(card.View);
                card.Destory();
            });
        }
    }
}
