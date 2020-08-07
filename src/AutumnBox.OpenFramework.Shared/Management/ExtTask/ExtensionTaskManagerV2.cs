#nullable enable
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutumnBox.Leafx.Container;
using System.Threading;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ADBKit;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Exceptions;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    [Component(Type = typeof(IExtensionTaskManager))]
    internal sealed class ExtensionTaskManagerV2 : IExtensionTaskManager
    {
        public IEnumerable<Task<object?>> RunningTasks
        {
            get
            {
                return from taskInfos in taskInfos.Values
                       where taskInfos.Task.Status == TaskStatus.Running
                       select taskInfos.Task;
            }
        }

        private readonly Dictionary<string, TaskInfo> taskInfos = new Dictionary<string, TaskInfo>();

        [AutoInject] ILibsManager? libsManager;

        [AutoInject] ILake? lake;

        [AutoInject] IDeviceManager? deviceManager;

        public IExtensionInfo GetExtensionByTask(Task<object?> task)
        {
            var findResult = taskInfos.Where(kv => kv.Value.Task == task);
            if (findResult.Any())
            {
                return findResult.FirstOrDefault().Value.Extension;
            }
            else
            {
                throw new Exception("Task not found!");
            }
        }

        public Task<object?> Start(string id, Dictionary<string, object>? args = null)
        {
            return Start(FindExtensionTypeById(id), args);
        }

        public Task<object?> Start(IExtensionInfo inf, Dictionary<string, object>? args = null)
        {
            StateCheck(inf);
            string id = GenerateId();
            var task = ExtensionTaskFactory.CreateTask(inf, args ?? new Dictionary<string, object>(), (thread) =>
            {
                taskInfos[id].Thread = thread;
            }, lake!);
            taskInfos[id] = new TaskInfo(task, inf);
            task.Start();
            return task;
        }

        private IExtensionInfo FindExtensionTypeById(string id)
        {
            if (libsManager == null)
            {
                throw new InvalidOperationException("Librarian has not been inject to here!");
            }
            var types = (from rext in libsManager.Registry
                         where rext.ExtensionInfo.Id == id
                         select rext.ExtensionInfo);
            if (types.Any())
            {
                return types.First();
            }
            else
            {
                throw new Exception("Extension not found!");
            }
        }

        public void Terminate(Task<object?> task)
        {
            var findResult = taskInfos.Where(kv => kv.Value.Task == task);
            if (findResult.Any() && findResult.FirstOrDefault().Value.Task.Status == TaskStatus.Running)
            {
                findResult?.FirstOrDefault().Value?.Thread?.Abort();
            }
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        private void StateCheck(IExtensionInfo inf)
        {
            IDevice? currentDevice = deviceManager!.Selected;
            if (!inf.IsRunnableCheck(currentDevice))
            {
                throw new DeviceStateIsNotCorrectException(inf.RequiredDeviceState(), currentDevice?.State);
            }
        }

        private class TaskInfo
        {
            public Task<object?> Task { get; }
            public Thread? Thread { get; set; }
            public IExtensionInfo Extension { get; }
            public TaskInfo(Task<object?> task, IExtensionInfo extension)
            {
                if (task is null)
                {
                    throw new ArgumentNullException(nameof(task));
                }
                Task = task;
                Extension = extension;
            }
        }
    }
}
