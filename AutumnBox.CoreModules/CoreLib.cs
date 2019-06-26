/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:27:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.CoreModules
{
    [ContextPermission(CtxPer.High)]
    public sealed class CoreLib : ExtensionLibrarin
    {
        public static Context Context { get; private set; }
        public static CoreLib Current { get; private set; }

        public override string Name => "AutumnBox Core Modules";

        public override int MinApiLevel => 10;

        public override int TargetApiLevel => 10;
        public CoreLib()
        {
            Context = this;
            Current = this;
        }
        protected override IExtensionWrapper GetWrapperFor(Type extType)
        {
            return new CustomWrapper(extType);
        }
        public override void Destory()
        {
            base.Destory();
            IStorageManager storageManager = ApiFactory.Get<IStorageManager>(this);
            storageManager.ClearCache();
        }
        private class CustomWrapper : ClassExtensionWrapper
        {
            protected internal CustomWrapper(Type t) : base(t)
            {
            }

            public override void Ready()
            {

            }
        }
    }
}
