/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/12 17:27:39 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    public interface ILanguagesManager
    {
        /// <summary>
        /// 加载某个语言资源词典
        /// </summary>
        /// <param name="regionCode">语言代码</param>
        /// <param name="langResDict">资源词典</param>
        void Load(string regionCode,ResourceDictionary langResDict);
        /// <summary>
        /// 根据目前的语言代码,自动获取语言代码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);
    }
}
