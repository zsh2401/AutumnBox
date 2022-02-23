﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 0:53:59 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Diagnostics;
using System.Reflection;

namespace AutumnBox.OpenFramework
{
    /// <summary>
    /// 关于SDK的信息
    /// </summary>
    public static class BuildInfo
    {
        /// <summary>
        /// 拓展模块路径
        /// </summary>
        public static string DEFAULT_EXTENSION_PATH = System.AppDomain.CurrentDomain.BaseDirectory + "/../exts";
        /// <summary>
        /// SDK版本,不设计为Const是为了防止编译器优化
        /// </summary>
        public static readonly int API_LEVEL;
        /// <summary>
        /// SDK版本
        /// </summary>
        public static readonly Version SDK_VERSION;
        /// <summary>
        /// AutumnBox.GUI的程序集名称
        /// </summary>
        internal const string AUTUMNBOX_GUI_ASSEMBLY_NAME = "AutumnBox.GUI";
        /// <summary>
        /// AutumnBox.Basic的程序集名称
        /// </summary>
        internal const string AUTUMNBOX_BASIC_ASSEMBLY_NAME = "AutumnBox.Basic";
        /// <summary>
        /// AutumnBox.OpenFramework的程序集名称
        /// </summary>
        internal const string AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME = "AutumnBox.OpenFramework";
        static BuildInfo()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(asm.Location);
            SDK_VERSION = new Version(info.FileMajorPart, info.FileMinorPart, info.FileBuildPart);
            API_LEVEL = SDK_VERSION.Major;
        }
    }
}