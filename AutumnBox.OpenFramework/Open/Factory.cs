/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:46:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.Impl;
using System;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 秋之盒API工厂类
    /// </summary>
    public class Factory : Object
    {
        // 一些与Context无关的,以单例存在
        #region Context无关
        /// <summary>
        /// 框架管理器
        /// </summary>
        private static IFrameworkManager fx = new FxImpl();
        /// <summary>
        /// MD5
        /// </summary>
        private static IMd5 _md5 = new Md5Impl();
        /// <summary>
        /// 操作系统API
        /// </summary>
        private static IOSApi _os = new OSApiImpl();
        /// <summary>
        /// 兼容性API
        /// </summary>
        private static ICompApi _comp = new CompImpl();
        /// <summary>
        /// 声音API
        /// </summary>
        private static ISoundPlayer _sound = new SoundImpl(AutumnBoxGuiApi.Main);
        #endregion
        /// <summary>
        /// 获取音效播放器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ISoundPlayer GetSoundPlayer(Context ctx)
        {
            return _sound;
        }
        /// <summary>
        /// 获取日志器
        /// </summary>
        /// <param name="ctx">context</param>
        /// <returns>日志器</returns>
        public ILogger GetLogger(Context ctx)
        {
            return new LoggerImpl(ctx);
        }
        /// <summary>
        /// 获取App管理器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IAppManager GetAppManager(Context ctx)
        {
            return new AppManagerImpl(ctx, AutumnBoxGuiApi.Main);
        }
        /// <summary>
        /// 获取操作系统API
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IOSApi GetOsApi(Context ctx)
        {
            return _os;
        }
        /// <summary>
        /// 获取内嵌资源管理器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IEmbeddedFileManager GetEmbeddedFileManager(Context ctx)
        {
            return new EmbeddedFileManagerImpl(ctx);
        }
        /// <summary>
        /// 获取MD5 API
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IMd5 GetMd5(Context ctx)
        {
            return _md5;
        }
        /// <summary>
        /// 获取临时文件管理器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ITemporaryFloder GetTmpManager(Context ctx)
        {
            return new TemporaryFloderImpl(ctx);
        }
        /// <summary>
        /// 获取兼容性API实现
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ICompApi GetCompApi(Context ctx)
        {
            return _comp;
        }
        /// <summary>
        /// 获取扩展框架管理器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IFrameworkManager GetFx(Context ctx)
        {
            return fx;
        }
        /// <summary>
        /// 获取Ux相关API
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IUx GetUx(Context ctx)
        {
            return new UxImpl(ctx, AutumnBoxGuiApi.Main);
        }
    }
}
