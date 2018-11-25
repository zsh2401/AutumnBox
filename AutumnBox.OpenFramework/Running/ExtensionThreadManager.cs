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
            var thread = new ThreadImpl(typeOfExtension, wrapper)
            {
                Id = AlllocatePID()
            };
            readys.Add(thread);
            thread.Started += (s, e) =>
            {
                readys.Remove(thread);
                runnings.Add(thread);
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

        private class ThreadImpl : IExtensionThread
        {
            private readonly Type extensionType;

            public Thread Thread { get; set; }

            public int ExitCode { get; private set; } = (int)ExtensionExitCodes.Killed;

            public int Id { get; internal set; }

            public bool IsRunning => Thread?.IsAlive == true;

            public IExtensionWrapper Wrapper { get; }

            public event EventHandler<ThreadFinishedEventArgs> Finished;
            public event EventHandler<ThreadStartedEventArgs> Started;

            private IExtension instance;

            public void SendSignal(string signal, object value = null)
            {
                if (string.IsNullOrWhiteSpace(signal))
                {
                    throw new ArgumentException("message", nameof(signal));
                }
                instance.ReceiveSignal(signal, value);
            }

            public void Kill()
            {
                try
                {
                    SendSignal(Signals.STOP_COMMAND);
                }
                catch
                {
                    return;
                }
                try
                {
                    Thread.Abort();
                }
                catch (ThreadAbortException) { }
            }

            private static readonly Dictionary<string, object> defaultData = new Dictionary<string, object>()
            {
                {AtmbVisualExtension.KEY_CLOSE_FINISHED,false}
            };

            public void Start(Dictionary<string, object> extractData = null)
            {
                Thread = new Thread(() =>
                {
                    try
                    {
                        instance = (IExtension)Activator.CreateInstance(extensionType);
                        instance.ReceiveSignal(Signals.ON_CREATED, new ExtensionArgs(this, Wrapper));
                        ExitCode = instance.Main(extractData ?? defaultData);
                    }
                    catch (ThreadAbortException)
                    {
                        ExitCode = (int)ExtensionExitCodes.Killed;
                    }
                    catch (Exception)
                    {
                        ExitCode = (int)ExtensionExitCodes.ErrorUnknown;
                    }
                    finally
                    {
                        Finished?.Invoke(this, new ThreadFinishedEventArgs(this));
                    }
                });
                Thread.Start();
                Started?.Invoke(this, new ThreadStartedEventArgs());
            }

            public ThreadImpl(Type extensionType, IExtensionWrapper wrapper)
            {
                this.extensionType = extensionType;
                Wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
            }
        }
    }
}
