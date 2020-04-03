/*

* ==============================================================================
*
* Filename: IObjectMaintainer 
* Description: 
*
* Version: 1.0
* Created: 2020/4/3 22:56:01
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.ObjectManagement
{
    public interface IObjectMatainner
    {
        List<ILake> Lakes { get; }
        Type Type { get; }
        object Instance { get; }
        void BuildInstance(Dictionary<string, object> args);
        void InjectPrototype();
        void InvokeMethod(string methodName,Dictionary<string, object> args);
    }
}
