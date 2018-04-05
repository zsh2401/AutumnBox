/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 22:25:39
** filename: BatInfo.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
namespace AutumnBox.Basic.Flows.MiFlash
{
    /// <summary>
    /// Bat信息
    /// </summary>
    public class BatInfo
    {
        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath { get; private set; }
        /// <summary>
        /// 解释
        /// </summary>
        public string Desc { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="desc"></param>
        public BatInfo(string fullPath, string desc)
        {
            FullPath = fullPath;
            Desc = desc;
        }
    }
}
