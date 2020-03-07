/*

* ==============================================================================
*
* Filename: IMethodProxy
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 14:04:09
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    public interface IMethodProxy
    {
        object CallMethod(object _this, string name, params ILake[] lakes);
        TClass CreateClass<TClass>();
        Func<TClass> GetClassBuilder<TClass>();
        Func<object> GetClassBuilder(Type classType);
        Func<object> GetMethodCaller(object owner, string methodName);
    }
}
