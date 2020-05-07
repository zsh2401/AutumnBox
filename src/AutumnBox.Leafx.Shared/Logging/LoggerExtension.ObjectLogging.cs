/*

* ==============================================================================
*
* Filename: LoggerExtension
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 20:08:53
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Leafx.Logging
{
	partial class LoggerExtension
	{
		[Conditional("DEBUG")]
		public static void Debug(this object sender, object content)
		{
			throw new NotImplementedException();
		}

		[Conditional("DEBUG")]
		public static void Debug(this object sender, object content, Exception e)
		{
			throw new NotImplementedException();
		}

		public static void Info(this object sender, object content)
		{
			throw new NotImplementedException();
		}
		public static void Warn(this object sender, object content)
		{
			throw new NotImplementedException();
		}
		public static void Warn(this object sender, object content, Exception e)
		{
			throw new NotImplementedException();
		}
		public static void Fatal(this object sender, object content, Exception e)
		{
			throw new NotImplementedException();
		}
		public static void Exception(this object sender, object content, Exception e)
		{
			throw new NotImplementedException();
		}
	}
}
