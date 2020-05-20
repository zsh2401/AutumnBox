/*

* ==============================================================================
*
* Filename: ObjectCache
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 18:04:23
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.ObjectManagement
{
	internal static class ObjectCache<TValue>
	{
		private static TValue value;
		public static TValue Acquire(Func<TValue> factory)
		{
			value ??= factory();
			return value;
		}
	}
}
