/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 16:07:37 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.Util.Data
{
    public interface IResource
    {
        string Id { get; }
        ResourceDictionary Resource { get; }
    }
    interface IResourceManager<TResource>
    {
        event EventHandler CurrentChanged;
        TResource Current { get; set; }
        IEnumerable<TResource> Loaded { get; }
        void Apply(TResource resource);
        void ApplyBySetting();
        void ApplyById(string id);
    }
}
