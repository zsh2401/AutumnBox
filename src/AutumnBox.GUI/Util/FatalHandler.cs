﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 19:33:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Views.Windows;
using AutumnBox.Leafx.Container;
using AutumnBox.Logging;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace AutumnBox.GUI.Util
{
    static class FatalHandler
    {
        private readonly static string[] blockListForExceptionSource = {
            "PresentationCore"
        };
        private static bool blocked = false;
        public static void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (blocked) return;
            blocked = true;
            var logger = LoggerFactory.Auto(nameof(FatalHandler));
            string src = e.Exception.Source;
            if (blockListForExceptionSource.Contains(src))
            {
                logger.Warn("PresentationCore Error", e.Exception);
                return;
            }
            string n = Environment.NewLine;
            string exstr =
                $"AutumnBox Exception {DateTime.Now.ToString("MM/dd/yyyy    HH:mm:ss")}{n}{n}" +
                $"Exception:{n}{e.Exception.ToString()}{n}{n}" +
                $"Message:{n}{e.Exception.Message}{n}{n}" +
                $"Source:{n}{e.Exception.Source}{n}{n}" +
                $"Inner:{n}{e.Exception.InnerException?.ToString() ?? "None"}{n}";

            try { logger.Warn(exstr); } catch { }
            ShowErrorToUser(exstr);
            e.Handled = true;
            AppUnloader.Unload();
            App.Current.Shutdown(1);
        }
        private static void ShowErrorToUser(string exstr)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case "zh-CN":
                case "zh-TW":
                case "zh-SG":
                case "zh-HK":
                    MessageBox.Show(
                            $"一个未知的错误的发生了,将logs文件夹压缩并发送给开发者以解决问题{Environment.NewLine}" +
                            $"出问题不发logs,开发者永远不可能解决你遇到的问题{Environment.NewLine}" +
                             $"邮件/QQ: zsh2401@163.com{Environment.NewLine}",
                            "AutumnBox 错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    break;
                default:
                    MessageBox.Show(
                         $"AutumnBox was failed on running{Environment.NewLine}" +
                        $"Please compress the logs folder and send it to zsh2401@163.com",
                        "Unknow Exception",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    break;
            }
        }
    }
}
