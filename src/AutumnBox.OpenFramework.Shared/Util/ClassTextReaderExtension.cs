using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// ClassTextReader的拓展方法
    /// </summary>
    public static class ClassTextReaderExtension
    {
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
                var appManager = LakeProvider.Lake.Get<IAppManager>();
                return reader.Get(key, appManager.CurrentLanguageCode);
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
            return RxGet(ClassTextReader.GetReader<T>(), key);
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
            return RxGet(ClassTextReader.GetReader(obj), key);
        }
    }
}
