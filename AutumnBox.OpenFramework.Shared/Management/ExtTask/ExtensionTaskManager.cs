using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.Wrapper;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
#if SDK
    internal 
#else
    public
#endif

    class ExtensionTaskManager : IExtensionTaskManager
    {
        public static ExtensionTaskManager Instance { get; }
        private readonly List<IExtensionTask> readys = new List<IExtensionTask>();
        private readonly List<IExtensionTask> runnings = new List<IExtensionTask>();

        public IExtensionTask Allocate(Type extType)
        {
            var thread = new ExtensionTask(extType)
            {
                Id = AlllocatePID()
            };
            readys.Add(thread);
            thread.Started += (s, e) =>
            {
                readys.Remove(thread);
                runnings.Add(thread);
            };
            thread.Finished += (s, e) =>
            {
                runnings.Remove(thread);
            };
            return thread;
        }

        private readonly Random ran = new Random();

        static ExtensionTaskManager()
        {
            Instance = new ExtensionTaskManager();
        }
        private ExtensionTaskManager() { }
        private int AlllocatePID()
        {
            int nextPid;
            do
            {
                nextPid = ran.Next();
            } while (FindTaskById(nextPid) != null);
            return nextPid;
        }

        public IExtensionTask FindTaskById(int id)
        {
            return runnings.Find((thr) =>
            {
                return thr.Id == id;
            });
        }

        public IEnumerable<IExtensionTask> RunningTasks => runnings.ToArray();
    }
}
