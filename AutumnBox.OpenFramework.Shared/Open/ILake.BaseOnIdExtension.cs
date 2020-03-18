/*

* ==============================================================================
*
* Filename: ILake
* Description: 
*
* Version: 1.0
* Created: 2020/3/19 0:11:40
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
	public static class ILakeBaseOnIdExtension
	{
		public static void Register(this ILake lake, string id, Type impl) { }
		public static void Register(string id, Type impl) { }
	}
}
