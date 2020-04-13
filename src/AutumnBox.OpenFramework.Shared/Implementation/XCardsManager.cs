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
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Implementation
{
    internal sealed class XCardsManager : IXCardsManager
    {
        private readonly IBaseApi baseApi;

        public XCardsManager(IBaseApi baseApi)
        {
            this.baseApi = baseApi;
            baseApi.RunOnUIThread(() =>
            {
                baseApi.Destorying += AutumnBoxDestorying;
            });
        }

        private void AutumnBoxDestorying(object sender, EventArgs e)
        {
            cards.ForEach(card => Unregister(card));
        }

        private readonly List<IXCard> cards = new List<IXCard>();

        public void Register(IXCard card)
        {
            baseApi.RunOnUIThread(() =>
            {
                card.Create();
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
