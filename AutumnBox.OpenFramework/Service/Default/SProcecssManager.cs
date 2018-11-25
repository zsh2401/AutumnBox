using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutumnBox.OpenFramework.Service.Default
{
    /// <summary>
    /// 进程管理器
    /// </summary>
    [ServiceName(SERV_NAME)]
    public class SProcessManager : AtmbService
    {
        private class ProcessHandler : IEquatable<ProcessHandler>
        {
            public int Id { get; }
            public Thread Thread { get; }
            public IExtension Extension { get; }
            public ProcessHandler(int id, Thread thread, IExtension extension)
            {
                Id = id;
                Thread = thread ?? throw new ArgumentNullException(nameof(thread));
                Extension = extension ?? throw new ArgumentNullException(nameof(extension));
            }
            public override bool Equals(object obj)
            {
                return Equals(obj as ProcessHandler);
            }
            public bool Equals(ProcessHandler other)
            {
                return other != null &&
                       Id == other.Id;
            }
            public override int GetHashCode()
            {
                return 2108858624 + Id.GetHashCode();
            }
        }
        private readonly Random ran = new Random();
        private readonly List<ProcessHandler> procs = new List<ProcessHandler>();
        /// <summary>
        /// 服务名
        /// </summary>
        public const string SERV_NAME = "___procManager";

        /// <summary>
        /// 开始一个进程
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="extractData"></param>
        /// <returns></returns>
        public int Start(IExtension extension, Dictionary<string, object> extractData)
        {
            Thread thread = new Thread(() =>
            {
                extension.Main(extractData);
            });
            int id = AllocatePid();
            procs.Add(new ProcessHandler(id, thread, extension));
            thread.Start();
            return id;
        }
        /// <summary>
        /// 检测进程是否存在
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public bool IsAlive(int proc)
        {
            return FindProcessById(proc)?.Thread.IsAlive ?? throw new Exception("process not found");
        }
        /// <summary>
        /// 杀死一个进程
        /// </summary>
        /// <param name="proc"></param>
        public void Kill(int proc)
        {
            var process = FindProcessById(proc);
            if (process == null)
            {
                throw new Exception("Process not found");
            }
            process.Thread.Abort();
            Free(proc);
        }

        /// <summary>
        /// 释放线程
        /// </summary>
        /// <param name="id"></param>
        private void Free(int id)
        {
            procs.RemoveAll((proc) =>
            {
                return proc.Id == id;
            });
        }
        /// <summary>
        /// 寻找进程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ProcessHandler FindProcessById(int id)
        {
            return procs.Find((proc) =>
            {
                return id == proc.Id;
            });
        }
        /// <summary>
        /// 分配PID
        /// </summary>
        /// <returns></returns>
        private int AllocatePid()
        {
            int next = ran.Next();
            while (FindProcessById(next) != null)
            {
                next = ran.Next();
            }
            return next;
        }
    }
}
