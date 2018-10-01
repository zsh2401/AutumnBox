/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/29 22:38:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 安卓键值
    /// </summary>
    public enum AndroidKeyCode
    {
        /// <summary>
        /// HOME 键 
        /// </summary>
        Home = 3,
        /// <summary>
        /// 返回键  
        /// </summary>
        Back = 4,
        /// <summary>
        /// 打开拨号应用 
        /// </summary>
        Call = 5,
        /// <summary>
        /// 挂断电话  
        /// </summary>
        Hangup = 6,
        /// <summary>
        /// 增加音量
        /// </summary>
        VolUp = 24,
        /// <summary>
        /// 降低音量
        /// </summary>
        VolDown = 25,
        /// <summary>
        /// 电源键  
        /// </summary>
        Power = 26,
        /// <summary>
        /// 拍照（需要在相机应用里）  
        /// </summary>
        Camera = 27,
        /// <summary>
        /// 打开浏览器 
        /// </summary>
        Browser = 64,
        /// <summary>
        /// 菜单键
        /// </summary>
        Menu = 82,
        /// <summary>
        /// 播放/暂停
        /// </summary>
        MediaPlayPause = 85,
        /// <summary>
        /// 停止播放 
        /// </summary>
        MediaStop = 86,
        /// <summary>
        /// 播放下一首 
        /// </summary>
        MediaNext = 87,
        /// <summary>
        /// 播放上一首
        /// </summary>
        MediaPrevious = 88,
        /// <summary>
        /// 移动光标到行首或列表顶部
        /// </summary>
        MoveHome = 122,
        /// <summary>
        /// 移动光标到行末或列表底部
        /// </summary>
        MoveEnd = 123,
        /// <summary>
        /// 恢复播放
        /// </summary>
        MediaPlay = 126,
        /// <summary>
        /// 暂停播放
        /// </summary>
        MediaPause = 127,
        /// <summary>
        /// 静音
        /// </summary>
        Mute = 164,
        /// <summary>
        /// 打开系统设置
        /// </summary>
        Settings = 176,
        /// <summary>
        /// 切换应用
        /// </summary>
        AppSwitch = 187,
        /// <summary>
        /// 打开联系人
        /// </summary>
        Contacts = 207,
        /// <summary>
        /// 打开日历
        /// </summary>
        Calendar = 208,
        /// <summary>
        /// 打开音乐
        /// </summary>
        Music = 209,
        /// <summary>
        /// 打开计算器
        /// </summary>
        Calculator = 210,
        /// <summary>
        /// 降低屏幕亮度
        /// </summary>
        BrightnessDown = 220,
        /// <summary>
        /// 提高屏幕亮度
        /// </summary>
        BrightnessUp = 221,
        /// <summary>
        /// 系统休眠
        /// </summary>
        Sleep = 223,
        /// <summary>
        /// 点亮屏幕
        /// </summary>
        WakeUp = 224,
        /// <summary>
        /// 打开语音助手
        /// </summary>
        VoiceAssistant = 231,
        /// <summary>
        /// 如果没有 wakelock 则让系统休眠
        /// </summary>
        SoftSleep = 276,
    }
}
