/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:14:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class TemporaryFloderImpl : ITemporaryFloder
    {
        public Context ctx;
        public TemporaryFloderImpl(Context ctx)
        {
#if !SDK
            var floderName = ctx.GetType().Assembly.GetName().Name;
            var path = System.IO.Path.Combine(ExtensionManager.ExtensionPath, floderName);
            DirInfo = new DirectoryInfo(path);
            Create();
#else
            throw new NotImplementedException();
#endif
        }
        public DirectoryInfo DirInfo { get; private set; }
        public string Path => DirInfo.FullName;
        public void Create()
        {
            if (DirInfo.Exists == false)
            {
                DirInfo.Create();
            }
        }
        public void Clean()
        {
            if (DirInfo.Exists == true)
            {
                DirInfo.Delete(true);
            }
        }
    }
}
