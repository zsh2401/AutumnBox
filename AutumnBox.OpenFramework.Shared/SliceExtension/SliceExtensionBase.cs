using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open;
using System;
namespace AutumnBox.OpenFramework.SliceExtension
{
    abstract class SliceExtensionBase : ISliceExtension
    {
        public abstract string Title { get; }
        public virtual string Icon { get; }
        public virtual object View { get; }
        protected ILogger Logger { get; private set; }
        public virtual bool PreCheck()
        {
            return true;
        }
        public virtual void Init()
        {
            Logger = LoggerFactory.Auto(GetType().Name);
        }
        public virtual void Displaying() { }
        public virtual void Pausing() { }
        public virtual void Destory() { }
        public virtual void ReceiveData(object data)
        {
        }
    }
}
