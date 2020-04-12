using AutumnBox.Leafx.Container.Support;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(INotificationManager))]
    sealed class NotificationManager : INotificationManager
    {
        public Task SendInfo(string msg)
        {
            throw new System.NotImplementedException();
        }

        public Task SendWarn(string msg)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool?> SendYN(string msg, string btnYes = null, string btnNo = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
