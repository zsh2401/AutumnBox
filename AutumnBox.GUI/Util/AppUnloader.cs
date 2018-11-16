/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 22:46:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.GUI.Util
{
    public class AppUnloader
    {
        public void Unload()
        {
            try
            {
                FxLoader.UnloadExtensions();
                FxLoader.UnloadServices();
                Adb.Server.Kill();
            }
            catch { }
        }
    }
}
