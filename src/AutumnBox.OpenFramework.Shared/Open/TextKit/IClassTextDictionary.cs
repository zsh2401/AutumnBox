namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 类文本加载器
    /// 无法直接通过Lake获得
    /// </summary>
    public interface IClassTextDictionary
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string this[string key] { get; }
    }
}
