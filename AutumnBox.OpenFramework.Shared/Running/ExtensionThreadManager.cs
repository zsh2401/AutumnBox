using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutumnBox.OpenFramework.Running
{
    [ServiceName(ServicesNames.THREAD_MANAGER)]
    internal class ExtensionThreadManager : AtmbService, IExtensionThreadManager
    {
        private readonly List<IExtensionThread> readys = new List<IExtensionThread>();
        private readonly List<IExtensionThread> runnings = new List<IExtensionThread>();
        public IExtensionThread Allocate(IExtensionWrapper wrapper, Type typeOfExtension)
        {
            var thread = new ExtensionThread(this, typeOfExtension, wrapper)
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

        private int AlllocatePID()
        {
            int nextPid;
            do
            {
                nextPid = ran.Next();
            } while (FindThreadById(nextPid) != null);
            return nextPid;
        }

        public IExtensionThread FindThreadById(int id)
        {
            return runnings.Find((thr) =>
            {
                return thr.Id == id;
            });
        }

        public IEnumerable<IExtensionThread> GetRunning()
        {
            return runnings;
        }


    }
}
