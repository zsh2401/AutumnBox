/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:19:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AutumnBox.OpenFramework.V1
{
    public abstract class AutumnBoxExtendModule
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 模块说明
        /// </summary>
        public virtual string Desc { get; } = "...";
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public virtual MailAddress ContactMail { get; }
        /// <summary>
        /// 开发者
        /// </summary>
        public virtual string Auth { get; }
        /// <summary>
        /// 版本号
        /// </summary>
        public virtual Version Version { get; } = new Version(1, 0, 0, 0);
        /// <summary>
        /// 需要的设备状态
        /// </summary>
        public virtual DeviceState RequiredDeviceState { get; } = DeviceState.Poweron;
        /// <summary>
        /// 打LOG
        /// </summary>
        /// <param name="message"></param>
        protected void Log(string message)
        {
            OpenApi.Log.Log(Name, message);
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="args"></param>
        public void Init(InitArgs args)
        {

            OnInit(args);
        }
        /// <summary>
        /// 检查这个模块是否可以运行
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            try
            {
                return OnCheck();
            }
            catch (Exception e)
            {
                Log("Check failed..." + e);
                return false;
            }
        }
        /// <summary>
        /// 运行模块
        /// </summary>
        /// <param name="args"></param>
        public void Run(RunArgs args)
        {
            try
            {
                OnStartCommand(args);
            }
            catch (Exception e)
            {
                Log($"OnStartCommand() Exception: " + e);
            }
        }
        /// <summary>
        /// 销毁模块
        /// </summary>
        /// <param name="args"></param>
        public void Destory(DestoryArgs args)
        {
            try { OnDestory(args); }
            catch (Exception e)
            {
                Log($"Destory Failed" + e);
            }

        }

        /// <summary>
        /// 模块初始化时调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnInit(InitArgs args) { }
        /// <summary>
        /// 当AutumnBox检测此模块是否可用时发生
        /// </summary>
        protected virtual bool OnCheck()
        {
            return true;
        }
        /// <summary>
        /// 当用户启动模块时调用
        /// </summary>
        /// <param name="args"></param>
        protected abstract void OnStartCommand(RunArgs args);
        /// <summary>
        /// 当模块被管理器释放时调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnDestory(DestoryArgs args) { }
    }
}
