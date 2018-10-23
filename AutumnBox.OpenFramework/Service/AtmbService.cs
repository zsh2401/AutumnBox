/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/23 14:28:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Service
{
    public abstract class AtmbService : Context, IEquatable<AtmbService>
    {
        public abstract int Id { get; }
        public abstract string Name { get; }
        public ServiceState State { get; set; } = ServiceState.Ready;

        public void Start()
        {
            if (State == ServiceState.Running)
            {
                throw new InvalidOperationException("This service is already started");
            }
            State = ServiceState.Running;
            OnStartCommand();
        }

        public void Stop()
        {
            if (State == ServiceState.Stopped)
            {
                throw new InvalidOperationException("This service is already stopped");
            }
            if (State == ServiceState.Ready)
            {
                throw new InvalidOperationException("This service is never been started");
            }
            State = ServiceState.Stopped;
            OnStopCommand();
        }

        public void Destory()
        {
            OnDestory();
        }

        protected abstract void OnStartCommand();
        protected virtual void OnStopCommand() { }
        protected virtual void OnDestory()
        {

        }
        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as AtmbService);
        }

        public bool Equals(AtmbService other)
        {
            return other != null
                && other.Name == this.Name
                && other.Id == this.Id;
        }
    }
}
