/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:27:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Open.Impl;
using System;
using System.Windows;

namespace AutumnBox.CoreModules
{
    [ContextPermission(CtxPer.High)]
    public sealed class CoreLib : ExtensionLibrarin
    {
        public static Context Context { get; private set; }
        public static CoreLib Current { get; private set; }
        public LanguageManager Languages { get; private set; }

        public override string Name => "AutumnBox.Core Modules";

        public override int MinApiLevel => 8;

        public override int TargetApiLevel => 8;

        public CoreLib()
        {
            Context = this;
            Current = this;
        }
        public override void Ready()
        {
            base.Ready();
            Languages = new LanguageManager(this);
            var en_us = new ResourceDictionary() { Source = new Uri("pack://application:,,,/AutumnBox.CoreModules;component/Res/I18N/en-US.xaml") };
            var zh_cn = new ResourceDictionary() { Source = new Uri("pack://application:,,,/AutumnBox.CoreModules;component/Res/I18N/zh-CN.xaml") }; ;
            Languages.Load("en-US", en_us);
            Languages.Load("zh-CN", zh_cn);
            Languages.Load("zh-TW", zh_cn);
            Languages.Load("zh-SG", zh_cn);
            Languages.Load("zh-HK", zh_cn);
        }
    }
}
