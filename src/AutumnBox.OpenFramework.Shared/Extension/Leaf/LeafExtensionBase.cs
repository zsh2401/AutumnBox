#nullable enable
using System.Collections.Generic;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    /// <summary>
    /// Leaf模块
    /// </summary>
    public abstract class LeafExtensionBase : EmptyExtension, IClassExtension
    {
        [AutoInject]
        private ILake? lake = null;

        /// <summary>
        /// 入口函数,继承者无需关心
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [LDoNotScan]
        public object Main(Dictionary<string, object> args)
        {
#if !DEBUG
            if (lake == null)
            {
                throw new InvalidOperationException("Lake has not been inject!");
            }
#endif
            var methodProxy = new MethodProxy(this, this.FindEntryPoint(), lake, GetSepLake());
            return methodProxy.Invoke(args ?? new Dictionary<string, object>());
        }

        private ILake GetSepLake()
        {
            SunsetLake s_lake = new SunsetLake();
            s_lake.RegisterSingleton<ILogger>(LoggerFactory.Auto(this.GetType().Name));
            s_lake.RegisterSingleton<ClassTextReader>(() =>
            {
                return ClassTextReader.GetReader(this);
            });
            s_lake.RegisterSingleton<IExtensionInfo>(() =>
            {
                return this.GetExtensionInfo();
            });
            s_lake.RegisterSingleton<ILeafUI>(() =>
            {
                ILeafUI leafUI = this.lake.Get<ILeafUI>();
                IExtensionInfo inf = this.GetExtensionInfo();
                leafUI.Title = inf.Name();
                leafUI.Icon = inf.Icon();
                return leafUI;
            });
            return s_lake;
        }


        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        /// <summary>
        /// 析构一个LeafExtension
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }
        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~LeafExtensionBase()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        /// <summary>
        /// 释放一个LeafExtension
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
