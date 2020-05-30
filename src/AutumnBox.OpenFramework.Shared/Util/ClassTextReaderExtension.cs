using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Open;

#nullable enable
namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// ClassTextReader的拓展方法
    /// </summary>
    public static class ClassTextReaderExtension
    {
        static IAppManager AppManagerCache
        {
            get
            {
                _appManager ??= LakeProvider.Lake.Get<IAppManager>();
                return _appManager;
            }
        }
        static IAppManager? _appManager;

        /// <summary>
        /// 根据OpenFx当前的语言环境获取对应区域的类文本值
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="key"></param>
        /// <returns>应得的文本,发生错误时将返回key值</returns>
        public static string RxGet(this ClassTextReader reader, string key)
        {
            try
            {
                return reader.Get(key, AppManagerCache.CurrentLanguageCode);
            }
            catch
            {
                return key;
            }
        }

        /// <summary>
        /// 获取对应类型上的类文本
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key"></param>
        /// <returns>应得的文本,发生错误时将返回key值</returns>
        public static string RxGetClassText<T>(string key)
        {
            return RxGet(ClassTextReaderCache.Acquire<T>(), key);
        }

        /// <summary>
        /// 根据OpenFx当前的语言环境获取当前对象上加载的对应区域码的类文本值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <returns>应得的文本,发生错误时将返回key值</returns>
        /// <returns>应得的文本,发生错误时将返回key值</returns>
        public static string RxGetClassText(this object obj, string key)
        {
            return RxGet(ClassTextReaderCache.Acquire(obj.GetType()), key);
        }
    }
}
