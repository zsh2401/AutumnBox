using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutumnBox.Leafx.Container;
using System.Threading;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    [Component(Type = typeof(IExtensionTaskManager))]
    class ExtensionTaskManagerV2 : IExtensionTaskManager
    {
        public IEnumerable<Task<object>> RunningTasks
        {
            get
            {
                return from taskInfos in taskInfos.Values
                       where taskInfos.Task.Status == TaskStatus.Running
                       select taskInfos.Task;
            }
        }

        [AutoInject]
        private readonly ILibsManager libsManager;

        [AutoInject]
        private readonly ILake lake;

        public Type ExtensionOfTask(Task<object> task)
        {
            var findResult = taskInfos.Where(kv => kv.Value.Task == task);
            if (findResult.Any())
            {
                return findResult.FirstOrDefault().Value.ExtensionType;
            }
            else
            {
                throw new Exception("Task not found!");
            }
        }

        public Task<object> Start(string extensionClassName, Dictionary<string, object> extralArgs = null)
        {
            return Start(FindExtensionTypeByName(extensionClassName), extralArgs);
        }

        private readonly Dictionary<string, TaskInfo> taskInfos = new Dictionary<string, TaskInfo>();

        private class TaskInfo
        {
            public Task<object> Task { get; }
            public Thread Thread { get; set; }
            public Type ExtensionType { get; }
            public TaskInfo(Task<object> task, Type extensionType)
            {
                if (task is null)
                {
                    throw new ArgumentNullException(nameof(task));
                }
                Task = task;
                ExtensionType = extensionType;
            }
        }
        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public Task<object> Start(Type t, Dictionary<string, object> extralArgs = null)
        {
            string id = GenerateId();
            var task = ExtensionTaskFactory.CreateTask(t, extralArgs, (thread) =>
            {
                taskInfos[id].Thread = thread;
            }, lake);
            taskInfos[id] = new TaskInfo(task, t);
            task.Start();
            return task;
        }

        private Type FindExtensionTypeByName(string className)
        {
            var types = (from wrapper in libsManager.Wrappers()
                         where wrapper.ExtensionType.Name == className
                         select wrapper.ExtensionType);
            if (types.Any())
            {
                return types.First();
            }
            else
            {
                throw new Exception("Extension not found!");
            }
        }

        public void Terminate(Task<object> task)
        {
            var findResult = taskInfos.Where(kv => kv.Value.Task == task);
            if (findResult.Any() && findResult.FirstOrDefault().Value.Task.Status == TaskStatus.Running)
            {
                findResult.FirstOrDefault().Value.Thread.Abort();
            }
        }

        public Task<object> Start<TClassExtension>(Dictionary<string, object> extralArgs = null) where TClassExtension : IClassExtension
        {
            return Start(typeof(TClassExtension), extralArgs);
        }
    }
}
