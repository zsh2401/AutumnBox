﻿#nullable enable
/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:19:38 (UTC +8:00)
** desc： ...
*************************************************/
#define WIN32
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 基础秋之盒主程序的API,提供开放框架与秋之盒主程序的交互
    /// </summary>
#if SDK
    internal 
#else
    public
#endif
    interface IBaseApi
    {
        /// <summary>
        /// 已连接的设备
        /// </summary>
        IEnumerable<IDevice> ConnectedDevices { get; }
        /// <summary>
        /// 存储空间
        /// </summary>
        DirectoryInfo StorageDirectory { get; }
        /// <summary>
        /// 临时空间
        /// </summary>
        DirectoryInfo TempDirectory { get; }
        /// <summary>
        /// 语言变更了
        /// </summary>
        event EventHandler LanguageChanged;
        /// <summary>
        /// 调用某些不为人知的特殊API
        /// 不建议任何第三方进行调用
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        object? UnstableInternalApiCall(string message, object? arg = null);
        /// <summary>
        /// 全局湖
        /// </summary>
        IRegisterableLake GlobalLake { get; }
        /// <summary>
        /// 发送一条普通的通知
        /// </summary>
        /// <param name="msg"></param>
        void SendNotificationInfo(string msg);
        /// <summary>
        /// 发送一条成功通知
        /// </summary>
        /// <param name="msg"></param>
        void SendNotificationSuccess(string msg);
        /// <summary>
        /// 发送一条警告通知
        /// </summary>
        /// <param name="msg"></param>
        void SendNotificationWarn(string msg);
        /// <summary>
        /// 发送一条询问通知
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<bool> SendNotificationAsk(string msg);
        /// <summary>
        /// 设置窗口背景模糊
        /// </summary>
        /// <param name="hWnd"></param>
        void SetWindowBlur(IntPtr hWnd);
        /// <summary>
        /// 秋之盒正在结束
        /// </summary>
        event EventHandler Destorying;
        /// <summary>
        /// 获取日志器版本号
        /// </summary>
        Version AutumnBoxLoggingVersion { get; }
        /// <summary>
        /// 获取秋之盒GUI程序版本号
        /// </summary>
        Version AutumnBoxGUIVersion { get; }
        /// <summary>
        /// 获取秋之盒基础版本号
        /// </summary>
        Version AutumnBoxBasicVersion { get; }
        /// <summary>
        /// 添加一个面板
        /// </summary>
        /// <param name="view"></param>
        /// <param name="priority"></param>
        void AppendPanel(object view, int priority);
        /// <summary>
        /// 移除一个被添加过的面板
        /// </summary>
        /// <param name="view"></param>
        void RemovePanel(object view);

        /// <summary>
        /// 刷新拓展模块列表
        /// </summary>
        void RefreshExtensionList();
        /// <summary>
        /// 从秋之盒库中获取已实现的View,通常将此View用于DialogHost
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        object GetNewView(string viewId);
        /// <summary>
        /// 获取新的LeafUI实现
        /// </summary>
        /// <returns></returns>
        ILeafUI NewLeafUI();
        /// <summary>
        /// 是否应该展示CMD窗口
        /// </summary>
        bool ShouldDisplayCmdWindow { get; }
        /// <summary>
        /// 是否是调试模式
        /// </summary>
        bool IsDeveloperMode { get; }
        /// <summary>
        /// 显示异常信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sketch"></param>
        /// <param name="exceptionMessage"></param>
        void ShowException(string title, string sketch, string exceptionMessage);
        /// <summary>
        /// 播放成功音效
        /// </summary>
        void PlayOk();
        /// <summary>
        /// 播放失败音效
        /// </summary>
        void PlayErr();
        /// <summary>
        /// 获取是否窗
        /// </summary>
        /// <returns></returns>
        bool DoYN(string message, string btnYes, string btnNo);
        /// <summary>
        /// 输入数字
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool InputNumber(string hint, int min, int max, out int result);
        /// <summary>
        /// 输入字符串
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool InputString(string hint, out string result);
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <returns></returns>
        dynamic GetMainWindow();
        /// <summary>
        /// 获取调试窗口
        /// </summary>
        /// <returns></returns>
        void ShowDebugUI();
        /// <summary>
        /// 显示消息窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        void ShowMessage(string title, string msg);
        /// <summary>
        /// 创建选择窗口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="btnLeft"></param>
        /// <param name="btnRight"></param>
        /// <param name="btnCancel"></param>
        /// <returns></returns>
        int DoChoice(string msg, string btnLeft = null, string btnRight = null, string btnCancel = null);
        /// <summary>
        /// 获取加载窗口
        /// </summary>
        /// <returns></returns>
        void ShowLoadingUI();
        /// <summary>
        /// 关闭加载窗口
        /// </summary>
        void CloseLoadingUI();
        /// <summary>
        /// 检测秋之盒是否以管理员模式运行
        /// </summary>
        bool IsRunAsAdmin { get; }
        /// <summary>
        /// 秋之盒版本号
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetResouce(string key);
        /// <summary>
        /// 设置资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void SetResource(string key, object value);
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddResource(string key, object value);
        /// <summary>
        /// 重启程序
        /// </summary>
        void Restart();
        /// <summary>
        /// 以管理员方式重启程序
        /// </summary>
        void RestartAsAdmin();
        /// <summary>
        /// 关闭程序
        /// </summary>
        void Shutdown();
        /// <summary>
        /// 在UI线程中运行代码
        /// </summary>
        /// <param name="act"></param>
        void RunOnUIThread(Action act);
        /// <summary>
        /// 获取当前语言代码
        /// </summary>
        /// <returns></returns>
        string GetCurrentLanguageCode();
        /// <summary>
        /// 获取当前的优先设备
        /// </summary>
        IDevice SelectedDevice { get; set; }
        event EventHandler SelectedDeviceChanged;
        event EventHandler ConnectedDeviceChanged;

        //int LeafCreate();
        //void LeafDisplay(int handle);
        //void LeafResize(int handle, int h, int w);
        //void LeafSetTitle(int handle, string title);
        //void LeafSetDescription(int handle, string description);
        //void LeafSetProgress(int handle, double progress);
        //void LeafSetResizable(int handle, bool resizable);
        //void LeafChangeStatus(int handle, int status);
        //void LeafSetStep(int handle, int current, int total, string stepName);
        //void LeafWrite(int handle, char[] buffer);
        //void LeafDialog(int handle, int type, string[] texts, ref string result);
        //void LeafClose(int handle);
    }
}
