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

    class ExtensionThreadManager : IExtensionTaskManager
    {
        public static ExtensionThreadManager Instance { get; }
        private readonly List<IExtensionTask> readys = new List<IExtensionTask>();
        private readonly List<IExtensionTask> runnings = new List<IExtensionTask>();

        public IExtensionTask Allocate(IExtensionWrapper wrapper, Type typeOfExtension)
        {
            var thread = new ExtensionThread(this, wrapper.ExtensionType, wrapper)
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

        static ExtensionThreadManager()
        {
            Instance = new ExtensionThreadManager();
        }
        private ExtensionThreadManager() { }
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

        public IEnumerable<IExtensionTask> GetRunning()
        {
            return runnings;
        }
    }
}
