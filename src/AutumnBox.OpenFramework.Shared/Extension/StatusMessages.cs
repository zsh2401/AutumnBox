/*

* ==============================================================================
*
* Filename: StatusMessages
* Description: 
*
* Version: 1.0
* Created: 2020/5/6 14:47:58
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
#nullable enable
using AutumnBox.Leafx.Enhancement.ClassTextKit;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 提供一些预定义的状态信息
    /// </summary>
    public static class StatusMessages
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static string Success => carrier.RxGetClassText(KEY_SUCCESS);

        /// <summary>
        /// 失败
        /// </summary>
        public static string Failed => carrier.RxGetClassText(KEY_FAILED);

        /// <summary>
        /// 被用户取消
        /// </summary>
        public static string CancelledByUser => carrier.RxGetClassText(KEY_CANCELLED_BY_USER);

        /// <summary>
        /// 表示致命错误
        /// </summary>
        public static string Fatal => carrier.RxGetClassText(KEY_FATAL);

        /// <summary>
        /// 文本载体
        /// </summary>
        private static readonly TextCarrier carrier = new TextCarrier();

        private const string KEY_SUCCESS = "success";
        private const string KEY_FAILED = "failed";
        private const string KEY_FATAL = "fatal";
        private const string KEY_CANCELLED_BY_USER = "failed";

        /// <summary>
        /// 载体
        /// </summary>
        [ClassText(KEY_SUCCESS, "Success", "zh-cn:成功")]
        [ClassText(KEY_FAILED, "Failed", "zh-cn:失败")]
        [ClassText(KEY_CANCELLED_BY_USER, "Cancelled by user", "zh-cn:被用户取消")]
        [ClassText(KEY_FATAL, "Fatal Error", "zh-cn:致命问题")]
        private sealed class TextCarrier { }
    }
}
