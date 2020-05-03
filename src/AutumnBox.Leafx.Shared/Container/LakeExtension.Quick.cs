#nullable enable
/*

* ==============================================================================
*
* Filename: LakeExtension
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 3:30:22
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Container
{
	partial class LakeExtension
	{
		/// <summary>
		/// 联合多个Lake
		/// </summary>
		/// <param name="first"></param>
		/// <param name="others"></param>
		/// <returns></returns>
		public static ILake UniteWith(this ILake first, params ILake[] others)
		{
			List<ILake> tmp = new List<ILake>
			{
				[0] = first
			};
			tmp.AddRange(others);
			return new MergedLake(tmp.ToArray());
		}
	}
}
