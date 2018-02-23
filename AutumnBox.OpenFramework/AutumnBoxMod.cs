using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework
{
    public abstract class AutumnBoxMod
    {
        #region 模块信息
        /// <summary>
        /// 模块名称
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 模块说明
        /// </summary>
        public virtual string Desc { get; } = "...";
        /// <summary>
        /// 模块需要的设备状态
        /// </summary>
        public virtual DeviceState RequiredDeviceState { get; } = DeviceState.Poweron;
        /// <summary>
        /// 模块版本
        /// </summary>
        public virtual Version Version { get; } = new Version(1,0,0,0);
        /// <summary>
        /// 模块发布日期
        /// </summary>
        public virtual DateTime DevelopmentDate { get; } = new DateTime();
        /// <summary>
        /// 目标SDK版本
        /// </summary>
        public virtual int TARGET_SDK => BuildInfo.SDK_VERSION;
        #endregion

        #region 供AutumnBox主程序调用的方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(InitArgs args)
        {
            OnInit(args);
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        public void Run(StartArgs args)
        {
            OnStartCommand(args);
        }
        /// <summary>
        /// 摧毁
        /// </summary>
        public void Destory(DestoryArgs args)
        {
            OnDestory(args);
        }
        #endregion

        /// <summary>
        /// 打印Log
        /// </summary>
        /// <param name="message"></param>
        protected virtual void Log(string message)
        {
            Logger.Log(Name, message);
            Console.WriteLine(Name,message);
        }
        #region 由子类覆写或实现的方法
        /// <summary>
        /// 初始化时执行此事件
        /// </summary>
        protected virtual void OnInit(InitArgs args) { }
        /// <summary>
        /// 当用户启动该模块时发生
        /// </summary>
        /// <param name="args"></param>
        protected abstract void OnStartCommand(StartArgs args);
        /// <summary>
        /// 当模块被停用时调用
        /// </summary>
        protected virtual void OnDestory(DestoryArgs args) { }
        #endregion
    }
}
