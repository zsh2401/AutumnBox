#nullable enable
/*

* ==============================================================================
*
* Filename: ILake
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:17:27
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// 标准的湖接口,湖是一种IoC中的容器概念,不可动态修改
    /// </summary>
    public interface ILake
    {
        /// <summary>
        /// 组件数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 根据ID获取组件
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="IdNotFoundException">当ID不存在时触发</exception>
        /// <returns>被注册的值</returns>
        object GetComponent(string id);
    }
}
