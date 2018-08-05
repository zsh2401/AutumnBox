/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 20:57:05 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Content
{
    internal class ContextApiProvider
    {
        private static readonly Factory factory = new Factory();
        private readonly Context ctx;
        public ILogger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = factory.GetLogger(ctx);
                }
                return logger;
            }
        }
        private ILogger logger;

        public IOSApi OS
        {
            get
            {
                if (osApi == null)
                {
                    osApi = factory.GetOsApi(ctx);
                }
                return osApi;
            }
        }
        private IOSApi osApi;


        public IMd5 Md5
        {
            get
            {
                if (md5 == null)
                {
                    md5 = factory.GetMd5(ctx);
                }
                return md5;
            }
        }
        private IMd5 md5;

        public ITemporaryFloder Tmp
        {
            get
            {
                if (tmp == null)
                {
                    tmp = factory.GetTmpManager(ctx);
                }
                return tmp;
            }
        }
        private ITemporaryFloder tmp;

        public IAppManager App
        {
            get
            {
                if (app == null)
                {
                    app = factory.GetAppManager(ctx);
                }
                return app;
            }
        }
        private IAppManager app;

        public ICompApi Comp
        {
            get
            {
                if (comp == null)
                {
                    comp = factory.GetCompApi(ctx);
                }
                return comp;
            }
        }
        private ICompApi comp;
        public IEmbeddedFileManager Emb
        {
            get
            {
                if (emb == null)
                {
                    emb = factory.GetEmbeddedFileManager(ctx);
                }
                return emb;
            }
        }
        private IEmbeddedFileManager emb;
        public ContextApiProvider(Context ctx)
        {
            this.ctx = ctx;
        }
    }
}
