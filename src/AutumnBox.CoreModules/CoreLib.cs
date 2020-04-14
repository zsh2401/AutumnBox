/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:27:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management.Wrapper;
using AutumnBox.OpenFramework.Open;
using System;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;

namespace AutumnBox.CoreModules
{
    public sealed class CoreLib : ExtensionLibrarian
    {
        public const string STORAGE_ID = "AutumnBox.CoreModules.Storage";
        public static CoreLib Current { get; private set; }

        public override string Name => "AutumnBox Core Modules";

        public override int MinApiLevel => 10;

        public override int TargetApiLevel => 10;
        public CoreLib()
        {
            Current = this;
        }
        public override void Ready()
        {
            base.Ready();
        }

        protected override IExtensionWrapper GetWrapperFor(Type extType)
        {
            return new CustomWrapper(extType);
        }
        public override void Destory()
        {
            base.Destory();
            IStorageManager storageManager = LakeProvider.Lake.Get<IStorageManager>();
            storageManager.Initialize(STORAGE_ID);
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
