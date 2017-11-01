/* =============================================================================*\
*
* Filename: Logger
* Description: 
*
* Version: 1.0
* Created: 2017/10/28 15:35:06 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace AutumnBox.Shared.CstmDebug
{
    [LogProperty(TAG = "Logger Father!", Show = false)]
    public static class Logger
    {
        private static readonly string DEFAULT_LOGFLODER = "logs/";
        private static readonly string DEFAULT_LOGFILE = "default.log";
        private static string NewFloder;
        static Logger()
        {
            if (!Directory.Exists(DEFAULT_LOGFLODER)) Directory.CreateDirectory(DEFAULT_LOGFLODER);
            NewFloder = DateTime.Now.ToString("yy_MM_dd/");
            if (!Directory.Exists(DEFAULT_LOGFLODER + NewFloder)) Directory.CreateDirectory(DEFAULT_LOGFLODER + NewFloder);
        }
        public static void D(string message, bool IsWarning = false)
        {
            var calledMethod = new StackTrace().GetFrames()[1].GetMethod();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(calledMethod);
            if (!attrInfo.Show) return;
            string full = GetFullMessage(attrInfo.TAG, message, IsWarning.ToErrorLevel());
            Debug.WriteLine(full);
            WriteToFile(calledMethod, full);
        }
        public static void D(string message, Exception e)
        {
            var calledMethod = new StackTrace().GetFrames()[1].GetMethod();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(calledMethod);
            if (!attrInfo.Show) return;
            StringBuilder full = new StringBuilder(GetFullMessage(attrInfo.TAG, message, 2));
            full.Append(Environment.NewLine + GetFullMessage(attrInfo.TAG, e.ToString() + e.Message, 2));
            Debug.WriteLine(full.ToString());
            WriteToFile(calledMethod, full.ToString());
        }
        public static void T(string message, bool IsWarning = false)
        {
            var calledMethod = new StackTrace().GetFrames()[1].GetMethod();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(calledMethod);
            if (!attrInfo.Show) return;
            string full = GetFullMessage(attrInfo.TAG, message, IsWarning.ToErrorLevel());
            Trace.WriteLine(full);
            WriteToFile(calledMethod, full);
        }
        public static void T(string message, Exception e)
        {
            var calledMethod = new StackTrace().GetFrames()[1].GetMethod();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(calledMethod);
            if (!attrInfo.Show) return;
            StringBuilder full = new StringBuilder(GetFullMessage(attrInfo.TAG, message, 2));
            full.Append(Environment.NewLine + GetFullMessage(attrInfo.TAG, e.ToString() + e.Message, 2));
            Trace.WriteLine(full.ToString());
            WriteToFile(calledMethod, full.ToString());
        }
        /// <summary>
        /// bool.ToErrorLevel
        /// </summary>
        /// <param name="isError"></param>
        /// <returns></returns>
        private static int ToErrorLevel(this bool isError)
        {
            if (isError) return 1;
            else return 0;
        }
        /// <summary>
        /// 智能化获取log依赖,如果调用者没有Log特性,则返回一个常规的log特性
        /// </summary>
        /// <returns></returns>
        private static LogPropertyAttribute GetLogPropertyAttribute(MethodBase calledMethod)
        {
            LogPropertyAttribute result = new LogPropertyAttribute();
            //Try to Get method TAG
            var methodAttrs = calledMethod.GetCustomAttributes(typeof(LogPropertyAttribute), false);
            foreach (var attr in methodAttrs)
            {
                var a = (attr as LogPropertyAttribute);
                result.TAG = a.TAG;
                result.Show = a.Show;
                break;
            }
            //Try to Get class TAG
            var classAttrs = calledMethod.ReflectedType.GetCustomAttributes(typeof(LogPropertyAttribute), true);
            foreach (var attr in classAttrs)
            {
                var classLogProp = (attr as LogPropertyAttribute);
                //如果方法没有定义logprop或者定了logprop而不定义tag,则使用class的tag
                result.TAG = (result.TAG != LogPropertyAttribute.NOT_LOAD_TAG) ? result.TAG : classLogProp.TAG;
                //如果class决定不显示log,那么方法也别想显示
                if (classLogProp.Show == false && result.Show == true)
                {
                    result.Show = classLogProp.Show;
                }
                break;
            }
            //如果到这里仍然没有得到TAG,则使用默认TAG
            if (result.TAG == LogPropertyAttribute.NOT_LOAD_TAG)
            {
                result.TAG = calledMethod.ReflectedType.Name;
            }
            return result;
        }
        /// <summary>
        /// Get Full message like [17-10-31_03:50:23][LoggerTest/EXCEPTION] : hehe
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="errorLevel"></param>
        /// <returns></returns>
        private static string GetFullMessage(string tag, string message, int errorLevel)
        {
            string t = DateTime.Now.ToString("yy-MM-dd_HH:mm:ss");
            switch (errorLevel)
            {
                case 0:
                    return $"[{t}][{tag}/INFO] : {message}";
                case 1:
                    return $"[{t}][{tag}/WARNING] : {message}";
                default:
                    return $"[{t}][{tag}/EXCEPTION] : {message}";
            }
        }
        /// <summary>
        /// Write log to log File
        /// </summary>
        /// <param name="fullMsg"></param>
        private static void WriteToFile(MethodBase caller, string fullMsg)
        {
            //return;
            string _LogFileName = DEFAULT_LOGFILE;
            var assInfo = Assembly.GetAssembly(caller.ReflectedType);
            var assAttr = assInfo.GetCustomAttributes(typeof(LogFilePropertyAttribute), true);
            if (assAttr.Length != 0)
            {
                _LogFileName = ((LogFilePropertyAttribute)assAttr[0]).FileName;
            }
            try
            {
                StreamWriter sw = new StreamWriter(DEFAULT_LOGFLODER + NewFloder + _LogFileName, true);
                sw.WriteLine(fullMsg);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }
    }
}
