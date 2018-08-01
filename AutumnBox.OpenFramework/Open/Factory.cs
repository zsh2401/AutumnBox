/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:46:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Open.Impl;
using System;

namespace AutumnBox.OpenFramework.Open
{
    internal class Factory : Object
    {
        private IMd5 _md5 = new Md5Impl();
        private IOSApi _os = new OSApiImpl();
        private ICompApi _comp = new CompImpl();
        public ILogger GetLogger(Context ctx)
        {
            return new LoggerImpl(ctx);
        }
        public IAppManager GetAppManager(Context ctx)
        {
            return new AppManagerImpl(ctx);
        }
        public IOSApi GetOsApi(Context ctx)
        {
            return _os;
        }
        public IEmbeddedFileManager GetEmbeddedFileManager(Context ctx)
        {
            return new EmbeddedFileManagerImpl(ctx);
        }
        public IMd5 GetMd5(Context ctx)
        {
            return _md5;
        }
        public ITemporaryFloder GetTmpManager(Context ctx)
        {
            return new TemporaryFloderImpl(ctx);
        }
        public ICompApi GetCompApi(Context ctx)
        {
            return _comp;
        }
    }
}
