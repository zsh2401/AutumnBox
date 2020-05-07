/*

* ==============================================================================
*
* Filename: ObjectExtension
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 17:59:45
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Logging
{
    partial class LoggerExtension
    {
        public static ILogger GetLogger(this object obj)
        {
            throw new NotImplementedException();
            //string categoryName = obj is string ? obj.ToString() : (obj?.GetType()?.Name ?? "Unknown");
            //return ObjectCache<string, ILogger>.Acquire(categoryName, () =>
            //{
            //    return new LoggerImpl
            //});
        }
        public static ILogger GetLogger<T>()
        {
            throw new NotImplementedException();
        }
    }
}
